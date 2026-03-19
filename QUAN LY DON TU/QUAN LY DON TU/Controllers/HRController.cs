using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DANGCAPNE.Data;
using DANGCAPNE.ViewModels;

namespace DANGCAPNE.Controllers
{
    public class HRController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HRController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;
            var roles = (HttpContext.Session.GetString("Roles") ?? "").Split(",");
            if (!roles.Contains("HR") && !roles.Contains("Admin"))
                return RedirectToAction("AccessDenied", "Account");

            var allRequests = await _context.Requests
                .Include(r => r.Requester).ThenInclude(u => u!.Department)
                .Include(r => r.FormTemplate)
                .Where(r => r.TenantId == tenantId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            var model = new HRDashboardViewModel
            {
                AllRequests = allRequests,
                PendingRequests = allRequests.Where(r => r.Status == "Pending").ToList(),
                InProgressRequests = allRequests.Where(r => r.Status == "InProgress" || r.Status == "RequestEdit").ToList(),
                ApprovedRequests = allRequests.Where(r => r.Status == "Approved").ToList(),
                RejectedRequests = allRequests.Where(r => r.Status == "Rejected" || r.Status == "Cancelled").ToList(),
                Employees = await _context.Users.Include(u => u.Department).Where(u => u.TenantId == tenantId && u.Status == "Active").ToListAsync(),
                LeaveBalances = await _context.LeaveBalances.Include(lb => lb.User).Include(lb => lb.LeaveType)
                    .Where(lb => lb.TenantId == tenantId && lb.Year == DateTime.Now.Year).ToListAsync(),
            };

            // Anomaly detection
            model.Anomalies = await DetectAnomalies(tenantId);

            // Dept stats
            var deptStats = allRequests
                .Where(r => r.Requester?.Department != null)
                .GroupBy(r => r.Requester!.Department!.Name)
                .ToDictionary(g => g.Key, g => g.Count());
            model.DepartmentStats = deptStats;

            return View(model);
        }

        public async Task<IActionResult> LeaveManagement()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var balances = await _context.LeaveBalances
                .Include(lb => lb.User).ThenInclude(u => u!.Department)
                .Include(lb => lb.LeaveType)
                .Where(lb => lb.TenantId == tenantId && lb.Year == DateTime.Now.Year)
                .ToListAsync();

            return View(balances);
        }

        public async Task<IActionResult> Timekeeping(DateTime? startDate, DateTime? endDate, int? employeeId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var start = startDate ?? DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            var end = endDate ?? DateTime.Now;

            var query = _context.Timesheets
                .Include(t => t.User).ThenInclude(u => u!.Department)
                .Where(t => t.TenantId == tenantId && t.Date >= start && t.Date <= end);

            if (employeeId.HasValue)
                query = query.Where(t => t.UserId == employeeId);

            var model = new TimekeepingViewModel
            {
                Timesheets = await query.OrderByDescending(t => t.Date).ToListAsync(),
                Shifts = await _context.Shifts.Where(s => s.TenantId == tenantId).ToListAsync(),
                StartDate = start,
                EndDate = end,
                UserId = employeeId,
                Employees = await _context.Users.Where(u => u.TenantId == tenantId && u.Status == "Active").ToListAsync()
            };

            return View(model);
        }

        private async Task<List<AnomalyAlert>> DetectAnomalies(int tenantId)
        {
            var anomalies = new List<AnomalyAlert>();
            var threeMonthsAgo = DateTime.Now.AddMonths(-3);

            // Detect Monday sick leave pattern
            var mondaySickLeaves = _context.Requests
                .Include(r => r.Requester)
                .Include(r => r.FormTemplate)
                .Where(r => r.TenantId == tenantId &&
                    r.FormTemplate!.Category == "Leave" &&
                    r.CreatedAt >= threeMonthsAgo)
                .AsEnumerable()
                .Where(r => r.CreatedAt.DayOfWeek == DayOfWeek.Monday)
                .GroupBy(r => r.RequesterId)
                .Where(g => g.Count() >= 3)
                .Select(g => new { UserId = g.Key, Count = g.Count(), Name = g.First().Requester!.FullName })
                .ToList();

            foreach (var item in mondaySickLeaves)
            {
                anomalies.Add(new AnomalyAlert
                {
                    EmployeeName = item.Name,
                    AlertType = "Nghỉ thứ Hai",
                    Description = $"Đã xin nghỉ {item.Count} lần vào ngày thứ Hai trong 3 tháng qua",
                    Severity = "Warning"
                });
            }

            // Detect high OT department
            var otByDept = await _context.Requests
                .Include(r => r.Requester).ThenInclude(u => u!.Department)
                .Where(r => r.TenantId == tenantId &&
                    r.FormTemplate!.Category == "OT" &&
                    r.CreatedAt >= DateTime.Now.AddMonths(-1))
                .GroupBy(r => r.Requester!.Department!.Name)
                .Select(g => new { Dept = g.Key, Count = g.Count() })
                .ToListAsync();

            foreach (var item in otByDept.Where(x => x.Count > 10))
            {
                anomalies.Add(new AnomalyAlert
                {
                    EmployeeName = item.Dept,
                    AlertType = "OT cao bất thường",
                    Description = $"Phòng {item.Dept} có {item.Count} đơn OT trong tháng này",
                    Severity = "Critical"
                });
            }

            return anomalies;
        }
    }
}
