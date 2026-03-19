using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DANGCAPNE.Data;
using DANGCAPNE.Models.Requests;
using DANGCAPNE.ViewModels;
using Newtonsoft.Json;

namespace DANGCAPNE.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly DANGCAPNE.Services.GeminiAIService _aiService;

        public RequestsController(ApplicationDbContext context, IWebHostEnvironment env, DANGCAPNE.Services.GeminiAIService aiService)
        {
            _context = context;
            _env = env;
            _aiService = aiService;
        }

        public async Task<IActionResult> Index(string? status, string? type, string? search, int page = 1)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;
            var roles = (HttpContext.Session.GetString("Roles") ?? "").Split(",");
            var isAdminOrHR = roles.Contains("Admin") || roles.Contains("HR");

            var query = _context.Requests
                .Include(r => r.Requester)
                .Include(r => r.FormTemplate)
                .Where(r => r.TenantId == tenantId);

            if (!isAdminOrHR)
                query = query.Where(r => r.RequesterId == userId);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(r => r.Status == status);
            if (!string.IsNullOrEmpty(type))
                query = query.Where(r => r.FormTemplate!.Category == type);
            if (!string.IsNullOrEmpty(search))
                query = query.Where(r => r.Title.Contains(search) || r.RequestCode.Contains(search));

            var pageSize = 10;
            var total = await query.CountAsync();

            var model = new RequestListViewModel
            {
                Requests = await query.OrderByDescending(r => r.CreatedAt)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                StatusFilter = status,
                TypeFilter = type,
                SearchQuery = search,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                PageSize = pageSize,
                FormTemplates = await _context.FormTemplates.Where(f => f.TenantId == tenantId && f.IsActive).ToListAsync()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int templateId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var template = await _context.FormTemplates
                .Include(f => f.Fields.OrderBy(ff => ff.DisplayOrder))
                    .ThenInclude(f => f.Options.OrderBy(o => o.DisplayOrder))
                .FirstOrDefaultAsync(f => f.Id == templateId);

            if (template == null) return NotFound();

            // Check for auto-saved draft
            var draft = await _context.DraftRequests
                .FirstOrDefaultAsync(d => d.UserId == userId.Value && d.FormTemplateId == templateId);

            var model = new RequestCreateViewModel
            {
                FormTemplate = template,
                Fields = template.Fields.ToList(),
                FormData = draft != null ? JsonConvert.DeserializeObject<Dictionary<string, string>>(draft.FormDataJson) ?? new() : new()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int templateId, IFormCollection form)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var template = await _context.FormTemplates
                .Include(f => f.Workflow).ThenInclude(w => w!.Steps.OrderBy(s => s.StepOrder))
                .Include(f => f.Fields)
                .FirstOrDefaultAsync(f => f.Id == templateId);

            if (template == null) return NotFound();

            // Generate request code
            var today = DateTime.Now;
            var countToday = await _context.Requests.CountAsync(r => r.TenantId == tenantId && r.CreatedAt.Date == today.Date);
            var requestCode = $"REQ-{today:yyyyMMdd}-{(countToday + 1):D3}";

            var title = form["Title"].ToString();
            if (string.IsNullOrEmpty(title))
                title = $"{template.Name} - {HttpContext.Session.GetString("FullName")}";

            var request = new Request
            {
                TenantId = tenantId,
                RequestCode = requestCode,
                FormTemplateId = templateId,
                RequesterId = userId.Value,
                Title = title,
                Status = "Pending",
                CurrentStepOrder = 1,
                Priority = form["Priority"].ToString() ?? "Normal",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            // Save form data
            foreach (var field in template.Fields)
            {
                var val = form[field.FieldName].ToString();
                if (!string.IsNullOrEmpty(val))
                {
                    _context.RequestData.Add(new RequestData
                    {
                        RequestId = request.Id,
                        FieldName = field.FieldName,
                        FieldValue = val
                    });
                }
            }

            // Handle file uploads
            var aiTasks = new List<Task<string>>();
            foreach (var file in form.Files)
            {
                if (file.Length > 0)
                {
                    var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", tenantId.ToString());
                    Directory.CreateDirectory(uploadsDir);
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(uploadsDir, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);

                    _context.RequestAttachments.Add(new RequestAttachment
                    {
                        RequestId = request.Id,
                        FileName = file.FileName,
                        FilePath = $"/uploads/{tenantId}/{fileName}",
                        ContentType = file.ContentType,
                        FileSize = file.Length,
                        UploadedById = userId.Value
                    });
                    
                    // Trigger AI Document Analysis for images
                    if (file.ContentType.StartsWith("image/"))
                    {
                        var task = _aiService.AnalyzeDocumentAsync(filePath, file.ContentType);
                        aiTasks.Add(task);
                    }
                }
            }

            // Create approval steps
            if (template.Workflow != null)
            {
                foreach (var step in template.Workflow.Steps.OrderBy(s => s.StepOrder))
                {
                    int? approverId = null;

                    // Determine approver
                    if (step.ApproverType == "DirectManager")
                    {
                        var mgr = await _context.UserManagers
                            .Where(um => um.UserId == userId && um.IsPrimary && (um.EndDate == null || um.EndDate > DateTime.Now))
                            .FirstOrDefaultAsync();
                        approverId = mgr?.ManagerId;

                        // Check delegation
                        if (approverId != null)
                        {
                            var delegation = await _context.Delegations
                                .Where(d => d.DelegatorId == approverId && d.IsActive && d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)
                                .FirstOrDefaultAsync();
                            if (delegation != null)
                                approverId = delegation.DelegateId;
                        }
                    }
                    else if (step.ApproverType == "SpecificUser")
                    {
                        approverId = step.ApproverUserId;
                    }
                    else if (step.ApproverType == "Role")
                    {
                        var roleUser = await _context.UserRoles
                            .Where(ur => ur.RoleId == step.ApproverRoleId)
                            .Select(ur => ur.UserId)
                            .FirstOrDefaultAsync();
                        approverId = roleUser > 0 ? roleUser : null;
                    }

                    // Skip step if applicant holds the role
                    var skipStatus = "Pending";
                    if (step.CanSkipIfApplicant && approverId == userId)
                    {
                        skipStatus = "Skipped";
                    }

                    _context.RequestApprovals.Add(new RequestApproval
                    {
                        RequestId = request.Id,
                        StepOrder = step.StepOrder,
                        StepName = step.Name,
                        ApproverId = approverId,
                        Status = skipStatus,
                        CreatedAt = DateTime.Now
                    });
                }
            }

            // Audit log
            _context.RequestAuditLogs.Add(new RequestAuditLog
            {
                RequestId = request.Id,
                UserId = userId.Value,
                Action = "Created",
                NewStatus = "Pending",
                Details = $"Tạo {template.Name}",
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                CreatedAt = DateTime.Now
            });

            // Notification for approver
            var firstApproval = await _context.RequestApprovals
                .Where(ra => ra.RequestId == request.Id && ra.Status == "Pending")
                .OrderBy(ra => ra.StepOrder)
                .FirstOrDefaultAsync();

            if (firstApproval?.ApproverId != null)
            {
                _context.Notifications.Add(new Models.SystemModels.Notification
                {
                    TenantId = tenantId,
                    UserId = firstApproval.ApproverId.Value,
                    Title = "Đơn mới cần duyệt",
                    Message = $"{HttpContext.Session.GetString("FullName")} đã tạo {template.Name}: {title}",
                    Type = "Approval",
                    ActionUrl = $"/Requests/Detail/{request.Id}",
                    RelatedRequestId = request.Id
                });
            }

            // Delete draft if exists
            var draft = await _context.DraftRequests
                .FirstOrDefaultAsync(d => d.UserId == userId.Value && d.FormTemplateId == templateId);
            if (draft != null) _context.DraftRequests.Remove(draft);

            // Delay AI Processing minimally to not block the main request, though for demo we await
            if (aiTasks.Any())
            {
                var aiResults = await Task.WhenAll(aiTasks);
                foreach (var res in aiResults)
                {
                    if (!string.IsNullOrEmpty(res))
                    {
                        _context.RequestComments.Add(new RequestComment
                        {
                            RequestId = request.Id,
                            UserId = userId.Value, // AI comment runs as the requester impersonation or system user
                            Content = res,
                            CreatedAt = DateTime.Now
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = $"Đơn {requestCode} đã được tạo thành công!";
            return RedirectToAction("Detail", new { id = request.Id });
        }

        public async Task<IActionResult> Detail(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var request = await _context.Requests
                .Include(r => r.Requester).ThenInclude(u => u!.Department)
                .Include(r => r.FormTemplate).ThenInclude(f => f!.Fields.OrderBy(ff => ff.DisplayOrder))
                    .ThenInclude(ff => ff.Options)
                .Include(r => r.Attachments)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null) return NotFound();

            var approvals = await _context.RequestApprovals
                .Include(a => a.Approver)
                .Where(a => a.RequestId == id)
                .OrderBy(a => a.StepOrder)
                .ToListAsync();

            var comments = await _context.RequestComments
                .Include(c => c.User)
                .Where(c => c.RequestId == id)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();

            var auditLogs = await _context.RequestAuditLogs
                .Include(a => a.User)
                .Where(a => a.RequestId == id)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            var formData = await _context.RequestData
                .Where(rd => rd.RequestId == id)
                .ToDictionaryAsync(rd => rd.FieldName, rd => rd.FieldValue ?? "");

            var currentApproval = approvals.FirstOrDefault(a => a.Status == "Pending" && a.ApproverId == userId);
            var roles = (HttpContext.Session.GetString("Roles") ?? "").Split(",");

            var model = new RequestDetailViewModel
            {
                Request = request,
                ApprovalHistory = approvals,
                Comments = comments,
                AuditLogs = auditLogs,
                FormData = formData,
                FormFields = request.FormTemplate?.Fields.ToList() ?? new(),
                CanApprove = currentApproval != null,
                CanReject = currentApproval != null,
                CanEdit = request.RequesterId == userId && (request.Status == "Draft" || request.Status == "RequestEdit"),
                CanCancel = request.RequesterId == userId && request.Status == "Pending",
                RequiresPin = request.FormTemplate?.RequiresFinancialApproval ?? false,
                CurrentApprovalId = currentApproval?.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int requestId, string content)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            _context.RequestComments.Add(new RequestComment
            {
                RequestId = requestId,
                UserId = userId.Value,
                Content = content,
                CreatedAt = DateTime.Now
            });

            _context.RequestAuditLogs.Add(new RequestAuditLog
            {
                RequestId = requestId,
                UserId = userId.Value,
                Action = "Commented",
                Details = content.Length > 200 ? content.Substring(0, 200) + "..." : content,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Detail", new { id = requestId });
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var request = await _context.Requests.FindAsync(id);
            if (request == null || request.RequesterId != userId) return NotFound();

            request.Status = "Cancelled";
            request.UpdatedAt = DateTime.Now;

            _context.RequestAuditLogs.Add(new RequestAuditLog
            {
                RequestId = id,
                UserId = userId.Value,
                Action = "Cancelled",
                OldStatus = request.Status,
                NewStatus = "Cancelled",
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
            });

            await _context.SaveChangesAsync();
            TempData["Success"] = "Đơn đã được hủy.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SaveDraft([FromBody] dynamic data)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();

            int templateId = (int)data.templateId;
            string formDataJson = JsonConvert.SerializeObject(data.formData);

            var existing = await _context.DraftRequests
                .FirstOrDefaultAsync(d => d.UserId == userId.Value && d.FormTemplateId == templateId);

            if (existing != null)
            {
                existing.FormDataJson = formDataJson;
                existing.LastSavedAt = DateTime.Now;
            }
            else
            {
                _context.DraftRequests.Add(new DraftRequest
                {
                    TenantId = HttpContext.Session.GetInt32("TenantId") ?? 1,
                    UserId = userId.Value,
                    FormTemplateId = templateId,
                    FormDataJson = formDataJson
                });
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Đã lưu nháp" });
        }

        public async Task<IActionResult> SelectTemplate()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var templates = await _context.FormTemplates
                .Where(f => f.TenantId == tenantId && f.IsActive)
                .ToListAsync();

            return View(templates);
        }
        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] AIChatRequest payload)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized(new { text = "Vui lòng đăng nhập trước tiên." });

            var history = payload?.history;
            if (history == null || history.Count == 0) return BadRequest(new { text = "History is empty." });

            var aiResult = await _aiService.GeneralChatAsync(history);

            return Json(new { text = aiResult });
        }
        
        public class AIIntentRequest
        {
            public string? prompt { get; set; }
        }

        public class AIChatRequest
        {
            public List<DANGCAPNE.Services.ChatMessage> history { get; set; } = new();
        }

        [HttpPost]
        public async Task<IActionResult> AnalyzeIntent([FromBody] AIIntentRequest payload)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();

            string? prompt = payload?.prompt;
            if (string.IsNullOrEmpty(prompt)) return BadRequest();

            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;
            
            // Get simplified template info for AI
            var templatesInfo = await _context.FormTemplates
                .Include(f => f.Fields)
                .Where(f => f.TenantId == tenantId && f.IsActive)
                .Select(f => new 
                {
                    Id = f.Id,
                    Name = f.Name,
                    Fields = f.Fields.Select(ff => new { ff.FieldName, ff.Label, ff.FieldType })
                })
                .ToListAsync();

            var templatesJson = JsonConvert.SerializeObject(templatesInfo);
            var aiResult = await _aiService.ParseRequestIntentAsync(prompt, templatesJson);

            return Content(aiResult, "application/json");
        }
    }
}
