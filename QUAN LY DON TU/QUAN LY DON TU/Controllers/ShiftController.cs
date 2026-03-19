using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DANGCAPNE.Data;
using DANGCAPNE.Models.Timekeeping;

namespace DANGCAPNE.Controllers
{
    public class ShiftController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShiftController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSwapRequest(int targetUserId, int requesterShiftId, DateTime requesterDate, int targetShiftId, DateTime targetDate, string reason)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Json(new { success = false, message = "Vui lòng đăng nhập" });
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var request = new ShiftSwapRequest
            {
                TenantId = tenantId,
                RequesterId = userId.Value,
                TargetUserId = targetUserId,
                RequesterShiftId = requesterShiftId,
                RequesterDate = requesterDate,
                TargetShiftId = targetShiftId,
                TargetDate = targetDate,
                Reason = reason,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            _context.ShiftSwapRequests.Add(request);
            await _context.SaveChangesAsync();

            // Gửi thông báo cho đồng nghiệp (TargetUserId) - Logic Notification đã có sẵn trong project
            // TODO: Call Notification Service

            return Json(new { success = true, message = "Đã gửi yêu cầu đổi ca cho đồng nghiệp" });
        }

        [HttpPost]
        public async Task<IActionResult> RespondToSwap(int requestId, bool accept)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var request = await _context.ShiftSwapRequests.FindAsync(requestId);

            if (request == null || request.TargetUserId != userId)
                return Json(new { success = false, message = "Yêu cầu không hợp lệ" });

            if (!accept)
            {
                request.Status = "Rejected";
            }
            else
            {
                request.Status = "ApprovedByTarget"; // Đã đồng nghiệp đồng ý, chờ Quản lý duyệt
                // Tùy theo chính sách, có thể chuyển thẳng sang Approved nếu không cần Manager
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = accept ? "Bạn đã đồng ý đổi ca" : "Bạn đã từ chối đổi ca" });
        }

        [HttpPost]
        public async Task<IActionResult> ManagerApproveSwap(int requestId, bool approve)
        {
            var roles = (HttpContext.Session.GetString("Roles") ?? "").Split(",");
            if (!roles.Contains("Manager") && !roles.Contains("Admin"))
                return Json(new { success = false, message = "Không có quyền duyệt" });

            var request = await _context.ShiftSwapRequests.FindAsync(requestId);
            if (request == null) return Json(new { success = false, message = "Không tìm thấy đơn" });

            if (approve)
            {
                request.Status = "Approved";
                request.ApprovedByManagerId = HttpContext.Session.GetInt32("UserId");

                // Thực hiện hoán đổi thực tế trong bảng UserShift (Nếu dự án lưu UserShift theo ngày)
                // Logic cập nhật UserShift ở đây...
            }
            else
            {
                request.Status = "RejectedByManager";
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Đã xử lý đơn đổi ca" });
        }
    }
}
