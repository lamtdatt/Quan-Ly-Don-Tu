using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DANGCAPNE.Data;
using DANGCAPNE.ViewModels;

namespace DANGCAPNE.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Services.IFileService _fileService;

        public AccountController(ApplicationDbContext context, Services.IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin đăng nhập.";
                return View(model);
            }

            var passwordHash = HashPassword(model.Password);
            var user = await _context.Users
                .Include(u => u.Department)
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordHash == passwordHash && u.Status == "Active");

            if (user == null)
            {
                ViewBag.Error = "Email hoặc mật khẩu không chính xác.";
                return View(model);
            }

            // Set session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetInt32("TenantId", user.TenantId);
            HttpContext.Session.SetString("FullName", user.FullName);
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("Avatar", user.AvatarUrl ?? "");
            HttpContext.Session.SetString("EmployeeCode", user.EmployeeCode);
            HttpContext.Session.SetString("Department", user.Department?.Name ?? "");

            var roles = user.UserRoles.Select(ur => ur.Role?.Name ?? "").ToList();
            HttpContext.Session.SetString("Roles", string.Join(",", roles));
            HttpContext.Session.SetString("PrimaryRole", roles.FirstOrDefault() ?? "Employee");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var user = _context.Users
                .Include(u => u.Department)
                .Include(u => u.Branch)
                .Include(u => u.JobTitle)
                .Include(u => u.Position)
                .FirstOrDefault(u => u.Id == userId);

            var leaveBalances = _context.LeaveBalances
                .Include(lb => lb.LeaveType)
                .Where(lb => lb.UserId == userId && lb.Year == DateTime.Now.Year)
                .ToList();

            var attendanceHistory = _context.Timesheets
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .Take(10)
                .ToList();

            var model = new ProfileViewModel
            {
                User = user,
                LeaveBalances = leaveBalances,
                AttendanceHistory = attendanceHistory,
                TwoFactorEnabled = user?.TwoFactorEnabled ?? false
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return RedirectToAction("Login");

            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                if (model.NewPassword != model.ConfirmPassword)
                {
                    TempData["Error"] = "Mật khẩu xác nhận không khớp.";
                    return RedirectToAction("Profile");
                }
                user.PasswordHash = HashPassword(model.NewPassword);
            }

            user.Phone = model.User?.Phone ?? user.Phone;
            user.TwoFactorEnabled = model.TwoFactorEnabled;
            user.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật thông tin thành công!";
            HttpContext.Session.SetString("FullName", user.FullName);

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Json(new { success = false, message = "Phiên làm việc hết hạn." });

            if (file == null || file.Length == 0) return Json(new { success = false, message = "Vui lòng chọn ảnh." });

            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null) return Json(new { success = false, message = "Người dùng không tồn tại." });

                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(user.AvatarUrl))
                {
                    _fileService.DeleteFile(user.AvatarUrl);
                }

                // Lưu ảnh mới
                var fileUrl = await _fileService.SaveFileAsync(file, "avatars");
                user.AvatarUrl = fileUrl;
                user.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                
                // Cập nhật session
                HttpContext.Session.SetString("Avatar", fileUrl);

                return Json(new { success = true, url = fileUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi tải ảnh: " + ex.Message });
            }
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + "DANGCAPNE_SALT"));
            return Convert.ToBase64String(bytes);
        }
    }
}
