using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DANGCAPNE.Data;
using DANGCAPNE.Models.Timekeeping;
using System;

namespace DANGCAPNE.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            
            var user = await _context.Users.FindAsync(userId);
            ViewBag.HasFaceRegistered = !string.IsNullOrEmpty(user?.FaceDescriptor);
            ViewBag.FaceDescriptor = user?.FaceDescriptor;
            ViewBag.AvatarUrl = user?.AvatarUrl;
            
            var today = DateTime.Today;
            var timesheet = await _context.Timesheets
                .FirstOrDefaultAsync(t => t.UserId == userId && t.Date == today);
            
            // Đã có cả Check-in và Check-out = Hoàn thành
            ViewBag.IsTodayCompleted = timesheet?.CheckOut != null;
            // Đã Check-in nhưng chưa Check-out
            ViewBag.IsCheckInDone = timesheet?.CheckIn != null && timesheet?.CheckOut == null;
            ViewBag.CheckInTime = timesheet?.CheckIn?.ToString("HH:mm:ss");
            
            return View();
        }

        public IActionResult RegisterFace()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            return View();
        }

        public async Task<IActionResult> AdminReport(DateTime? fromDate, DateTime? toDate, int? deptId, int? searchUserId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var roles = (HttpContext.Session.GetString("Roles") ?? "").Split(",");
            bool isAdmin = roles.Contains("Admin");
            bool isHR = roles.Contains("HR");
            if (!isAdmin && !isHR) return RedirectToAction("Index"); // Chỉ Admin/HR

            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            // Mặc định xem tháng hiện tại
            var from = fromDate ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var to = toDate ?? DateTime.Today;

            var query = _context.Timesheets
                .Include(t => t.User).ThenInclude(u => u!.Department)
                .Where(t => t.User!.TenantId == tenantId && t.Date >= from && t.Date <= to);

            if (deptId.HasValue)
                query = query.Where(t => t.User!.DepartmentId == deptId);

            if (searchUserId.HasValue)
                query = query.Where(t => t.UserId == searchUserId);

            var timesheets = await query
                .OrderByDescending(t => t.Date)
                .ThenBy(t => t.User!.FullName)
                .ToListAsync();

            var departments = await _context.Departments
                .Where(d => d.TenantId == tenantId)
                .ToListAsync();

            var employees = await _context.Users
                .Where(u => u.TenantId == tenantId && u.Status == "Active")
                .OrderBy(u => u.FullName)
                .ToListAsync();

            ViewBag.Timesheets = timesheets;
            ViewBag.Departments = departments;
            ViewBag.Employees = employees;
            ViewBag.FromDate = from.ToString("yyyy-MM-dd");
            ViewBag.ToDate = to.ToString("yyyy-MM-dd");
            ViewBag.SelectedDeptId = deptId;
            ViewBag.SelectedUserId = searchUserId;
            ViewBag.TotalPresent = timesheets.Count;
            ViewBag.TotalWorkHours = timesheets.Sum(t => t.WorkHours);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveFaceDescriptor(string descriptor)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Json(new { success = false, message = "Phiên hết hạn" });

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return Json(new { success = false, message = "User không tồn tại" });

            user.FaceDescriptor = descriptor;
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đăng ký khuôn mặt thành công" });
        }

        [HttpPost]
        public async Task<IActionResult> CheckIn(double? lat, double? lon, string? wifiName, string? wifiBssid, string? qrCode, string? photoBase64, bool? faceMatched)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Json(new { success = false, message = "Phiên đăng nhập hết hạn" });
            
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return Json(new { success = false, message = "Không tìm thấy người dùng" });

            // Kiểm tra xác thực khuôn mặt nếu user đã đăng ký
            if (!string.IsNullOrEmpty(user.FaceDescriptor))
            {
                if (faceMatched != true)
                    return Json(new { success = false, message = "Xác thực khuôn mặt thất bại. Vui lòng quét lại đúng gương mặt chủ tài khoản." });
            }

            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var config = await _context.AttendanceLocationConfigs
                .FirstOrDefaultAsync(c => c.BranchId == user.BranchId && c.IsActive);

            string source = "FaceRecognition";

            // Kiểm tra QRCode, Wifi, GPS nếu có cấu hình...
            if (config != null && !string.IsNullOrEmpty(config.QrCodeKey))
            {
                if (qrCode != config.QrCodeKey)
                    return Json(new { success = false, message = "Mã QR không hợp lệ cho chi nhánh này" });
                source = "QRCode";
            }
            if (config != null && !string.IsNullOrEmpty(config.WifiBssid))
            {
                if (wifiBssid != config.WifiBssid)
                    return Json(new { success = false, message = "Vui lòng kết nối đúng Wifi văn phòng" });
                source = "Wifi";
            }
            if (config != null && config.AllowedLatitude.HasValue && lat.HasValue)
            {
                double distance = CalculateDistance(lat.Value, lon!.Value, config.AllowedLatitude.Value, config.AllowedLongitude!.Value);
                if (distance > config.AllowedRadiusMeters)
                    return Json(new { success = false, message = $"Bạn đang ở quá xa văn phòng ({Math.Round(distance)}m)" });
                source = "GPS";
            }

            var today = DateTime.Today;
            var timesheet = await _context.Timesheets
                .FirstOrDefaultAsync(t => t.UserId == userId && t.Date == today);

            // ===== LƯỢT 1: CHECK-IN =====
            if (timesheet == null)
            {
                timesheet = new Timesheet
                {
                    TenantId = tenantId,
                    UserId = userId.Value,
                    Date = today,
                    CheckIn = DateTime.Now,
                    Source = source,
                    GpsLatitude = lat,
                    GpsLongitude = lon,
                    WifiName = wifiName,
                    WifiBssid = wifiBssid,
                    QrCodeKey = qrCode,
                    PhotoUrl = photoBase64,
                    Status = "Present"
                };
                _context.Timesheets.Add(timesheet);
                await _context.SaveChangesAsync();
                return Json(new { success = true, type = "checkin", message = "Chấm công VÀO thành công", time = DateTime.Now.ToString("HH:mm:ss") });
            }

            // ===== LƯỢT 2: CHECK-OUT =====
            if (timesheet.CheckOut == null)
            {
                timesheet.CheckOut = DateTime.Now;
                // Tính giờ làm (đảm bảo >= 0)
                var workHours = (timesheet.CheckOut.Value - timesheet.CheckIn!.Value).TotalHours;
                timesheet.WorkHours = Math.Round(Math.Max(workHours, 0), 2);
                // Trạng thái: 8 tiếng là đủ giờ
                timesheet.Status = timesheet.WorkHours >= 8 ? "Present" : "EarlyLeave";
                timesheet.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();

                var statusMsg = timesheet.WorkHours >= 8
                    ? $"✅ Đủ giờ ({timesheet.WorkHours.ToString("0.##")}h)"
                    : $"⚠️ Thiếu giờ ({timesheet.WorkHours.ToString("0.##")}h / 8h)";

                return Json(new { success = true, type = "checkout", message = "Chấm công RA thành công. " + statusMsg, time = DateTime.Now.ToString("HH:mm:ss") });
            }

            // Cả 2 lượt đã đủ
            return Json(new { success = false, message = "Bạn đã hoàn thành đủ 2 lần chấm công cho hôm nay rồi!" });
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371e3; // metres
            var p1 = lat1 * Math.PI / 180;
            var p2 = lat2 * Math.PI / 180;
            var dp = (lat2 - lat1) * Math.PI / 180;
            var dl = (lon2 - lon1) * Math.PI / 180;

            var a = Math.Sin(dp / 2) * Math.Sin(dp / 2) +
                    Math.Cos(p1) * Math.Cos(p2) *
                    Math.Sin(dl / 2) * Math.Sin(dl / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c; // in metres
        }
    }
}
