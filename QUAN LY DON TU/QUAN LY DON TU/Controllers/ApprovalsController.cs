using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using DANGCAPNE.Data;
using DANGCAPNE.Hubs;
using DANGCAPNE.Models.Requests;
using DANGCAPNE.ViewModels;

namespace DANGCAPNE.Controllers
{
    public class ApprovalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ApprovalsController(ApplicationDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index(string? status)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var pending = await _context.RequestApprovals
                .Include(a => a.Request).ThenInclude(r => r!.Requester)
                .Include(a => a.Request).ThenInclude(r => r!.FormTemplate)
                .Where(a => a.ApproverId == userId && a.Status == "Pending")
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            var processed = await _context.RequestApprovals
                .Include(a => a.Request).ThenInclude(r => r!.Requester)
                .Include(a => a.Request).ThenInclude(r => r!.FormTemplate)
                .Where(a => a.ApproverId == userId && a.Status != "Pending")
                .OrderByDescending(a => a.ActionDate)
                .Take(50)
                .ToListAsync();

            var model = new ApprovalListViewModel
            {
                PendingApprovals = pending,
                ProcessedApprovals = processed,
                StatusFilter = status,
                TotalPending = pending.Count
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessApproval(ApprovalActionViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var approval = await _context.RequestApprovals
                .Include(a => a.Request).ThenInclude(r => r!.FormTemplate)
                .FirstOrDefaultAsync(a => a.Id == model.ApprovalId && a.ApproverId == userId);

            if (approval == null) return NotFound();

            // PIN verification for financial approvals
            if (approval.Request?.FormTemplate?.RequiresFinancialApproval == true && model.Action == "Approve")
            {
                if (string.IsNullOrEmpty(model.Pin) || model.Pin != "1234") // In production, verify against user's PIN hash
                {
                    TempData["Error"] = "Mã PIN không chính xác. Vui lòng thử lại.";
                    return RedirectToAction("Detail", "Requests", new { id = approval.RequestId });
                }
                approval.VerifiedByPin = true;
            }

            var request = approval.Request!;
            var oldStatus = request.Status;

            if (model.Action == "Approve")
            {
                approval.Status = "Approved";
                approval.ActionDate = DateTime.Now;
                approval.Comments = model.Comments;
                approval.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

                // Check if there are more steps
                var nextApproval = await _context.RequestApprovals
                    .Where(a => a.RequestId == request.Id && a.StepOrder > approval.StepOrder && a.Status == "Pending")
                    .OrderBy(a => a.StepOrder)
                    .FirstOrDefaultAsync();

                // Skip already-skipped steps
                while (nextApproval != null && nextApproval.Status == "Skipped")
                {
                    nextApproval = await _context.RequestApprovals
                        .Where(a => a.RequestId == request.Id && a.StepOrder > nextApproval.StepOrder && a.Status == "Pending")
                        .OrderBy(a => a.StepOrder)
                        .FirstOrDefaultAsync();
                }

                if (nextApproval != null)
                {
                    request.Status = "InProgress";
                    request.CurrentStepOrder = nextApproval.StepOrder;

                    // Notify next approver
                    if (nextApproval.ApproverId != null)
                    {
                        _context.Notifications.Add(new Models.SystemModels.Notification
                        {
                            TenantId = tenantId,
                            UserId = nextApproval.ApproverId.Value,
                            Title = "Đơn cần duyệt",
                            Message = $"Đơn {request.RequestCode} đã được duyệt bước trước và chuyển đến bạn",
                            Type = "Approval",
                            ActionUrl = $"/Requests/Detail/{request.Id}",
                            RelatedRequestId = request.Id
                        });

                        // SignalR real-time notification
                        await _hubContext.Clients.Group($"user_{nextApproval.ApproverId}")
                            .SendAsync("ReceiveNotification", new
                            {
                                title = "Đơn cần duyệt",
                                message = $"Đơn {request.RequestCode} cần bạn xử lý",
                                type = "Approval",
                                actionUrl = $"/Requests/Detail/{request.Id}"
                            });
                    }
                }
                else
                {
                    // All steps approved
                    request.Status = "Approved";
                    request.CompletedAt = DateTime.Now;

                    // Notify requester
                    _context.Notifications.Add(new Models.SystemModels.Notification
                    {
                        TenantId = tenantId,
                        UserId = request.RequesterId,
                        Title = "Đơn đã được duyệt ✓",
                        Message = $"Đơn {request.RequestCode} - {request.Title} đã được phê duyệt hoàn tất",
                        Type = "Info",
                        ActionUrl = $"/Requests/Detail/{request.Id}",
                        RelatedRequestId = request.Id
                    });

                    // SignalR
                    await _hubContext.Clients.Group($"user_{request.RequesterId}")
                        .SendAsync("RequestStatusChanged", new
                        {
                            requestCode = request.RequestCode,
                            newStatus = "Approved"
                        });
                }
            }
            else if (model.Action == "Reject")
            {
                approval.Status = "Rejected";
                approval.ActionDate = DateTime.Now;
                approval.Comments = model.Comments;
                approval.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

                request.Status = "Rejected";
                request.CompletedAt = DateTime.Now;

                _context.Notifications.Add(new Models.SystemModels.Notification
                {
                    TenantId = tenantId,
                    UserId = request.RequesterId,
                    Title = "Đơn bị từ chối ✗",
                    Message = $"Đơn {request.RequestCode} bị từ chối: {model.Comments}",
                    Type = "Info",
                    ActionUrl = $"/Requests/Detail/{request.Id}",
                    RelatedRequestId = request.Id
                });

                await _hubContext.Clients.Group($"user_{request.RequesterId}")
                    .SendAsync("RequestStatusChanged", new
                    {
                        requestCode = request.RequestCode,
                        newStatus = "Rejected"
                    });
            }
            else if (model.Action == "RequestEdit")
            {
                approval.Status = "Pending"; // Keep pending
                request.Status = "RequestEdit";

                _context.Notifications.Add(new Models.SystemModels.Notification
                {
                    TenantId = tenantId,
                    UserId = request.RequesterId,
                    Title = "Yêu cầu chỉnh sửa đơn",
                    Message = $"Đơn {request.RequestCode} cần chỉnh sửa: {model.Comments}",
                    Type = "Info",
                    ActionUrl = $"/Requests/Detail/{request.Id}",
                    RelatedRequestId = request.Id
                });
            }

            request.UpdatedAt = DateTime.Now;

            // Audit log
            _context.RequestAuditLogs.Add(new RequestAuditLog
            {
                RequestId = request.Id,
                UserId = userId.Value,
                Action = model.Action,
                OldStatus = oldStatus,
                NewStatus = request.Status,
                Details = model.Comments,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
            });

            await _context.SaveChangesAsync();

            TempData["Success"] = model.Action switch
            {
                "Approve" => "Đơn đã được phê duyệt thành công!",
                "Reject" => "Đơn đã bị từ chối.",
                "RequestEdit" => "Đã yêu cầu chỉnh sửa đơn.",
                _ => "Thao tác thành công."
            };

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> BulkApproval(BulkApprovalViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            int processed = 0;
            foreach (var approvalId in model.ApprovalIds)
            {
                var approval = await _context.RequestApprovals
                    .Include(a => a.Request)
                    .FirstOrDefaultAsync(a => a.Id == approvalId && a.ApproverId == userId && a.Status == "Pending");

                if (approval == null) continue;

                approval.Status = model.Action == "Approve" ? "Approved" : "Rejected";
                approval.ActionDate = DateTime.Now;
                approval.Comments = model.Comments;
                approval.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

                var request = approval.Request!;

                if (model.Action == "Approve")
                {
                    var hasMoreSteps = await _context.RequestApprovals
                        .AnyAsync(a => a.RequestId == request.Id && a.StepOrder > approval.StepOrder && a.Status == "Pending");

                    request.Status = hasMoreSteps ? "InProgress" : "Approved";
                    if (!hasMoreSteps) request.CompletedAt = DateTime.Now;
                }
                else
                {
                    request.Status = "Rejected";
                    request.CompletedAt = DateTime.Now;
                }

                request.UpdatedAt = DateTime.Now;

                _context.RequestAuditLogs.Add(new RequestAuditLog
                {
                    RequestId = request.Id,
                    UserId = userId.Value,
                    Action = $"Bulk{model.Action}",
                    NewStatus = request.Status,
                    Details = model.Comments,
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
                });

                processed++;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"Đã xử lý {processed} đơn thành công!";
            return RedirectToAction("Index");
        }
    }
}
