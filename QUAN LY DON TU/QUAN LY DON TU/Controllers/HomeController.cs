using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DANGCAPNE.Data;
using DANGCAPNE.ViewModels;

namespace DANGCAPNE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;
            var roles = (HttpContext.Session.GetString("Roles") ?? "").Split(",");

            var user = await _context.Users
                .Include(u => u.Department)
                .Include(u => u.JobTitle)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var isAdmin = roles.Contains("Admin");
            var isHR = roles.Contains("HR");
            var isManager = roles.Contains("Manager");

            // Base query for tenant
            var requestsQuery = _context.Requests.Where(r => r.TenantId == tenantId);

            var todaySheet = await _context.Timesheets
                .FirstOrDefaultAsync(t => t.UserId == userId && t.Date == DateTime.Today);

            var model = new DashboardViewModel
            {
                CurrentUser = user,
                RoleName = HttpContext.Session.GetString("PrimaryRole") ?? "Employee",
                UnreadNotifications = await _context.Notifications
                    .CountAsync(n => n.UserId == userId && !n.IsRead),
                // Đã hoàn tất = có cả Check-in và Check-out
                IsAttendanceDone = todaySheet?.CheckOut != null,
                // Chỉ mới Check-in, chưa Check-out
                IsCheckInDone = todaySheet?.CheckIn != null && todaySheet?.CheckOut == null,
                CheckInTime = todaySheet?.CheckIn?.ToString("HH:mm")
            };

            // Role-based stats
            if (isAdmin || isHR)
            {
                model.TotalPendingRequests = await requestsQuery.CountAsync(r => r.Status == "Pending" || r.Status == "InProgress");
                model.TotalApprovedRequests = await requestsQuery.CountAsync(r => r.Status == "Approved");
                model.TotalRejectedRequests = await requestsQuery.CountAsync(r => r.Status == "Rejected");
                model.TotalMyRequests = await requestsQuery.CountAsync();
                model.TotalEmployees = await _context.Users.CountAsync(u => u.TenantId == tenantId && u.Status == "Active");
            }
            else
            {
                model.TotalPendingRequests = await requestsQuery.CountAsync(r => r.RequesterId == userId && (r.Status == "Pending" || r.Status == "InProgress"));
                model.TotalApprovedRequests = await requestsQuery.CountAsync(r => r.RequesterId == userId && r.Status == "Approved");
                model.TotalRejectedRequests = await requestsQuery.CountAsync(r => r.RequesterId == userId && r.Status == "Rejected");
                model.TotalMyRequests = await requestsQuery.CountAsync(r => r.RequesterId == userId);
            }

            // Leave balance
            var leaveBalance = await _context.LeaveBalances
                .Where(lb => lb.UserId == userId && lb.LeaveTypeId == 1 && lb.Year == DateTime.Now.Year)
                .FirstOrDefaultAsync();
            model.LeaveBalance = leaveBalance?.Remaining ?? 12;

            // Recent requests
            if (isAdmin || isHR)
            {
                model.RecentRequests = await requestsQuery
                    .Include(r => r.Requester)
                    .Include(r => r.FormTemplate)
                    .OrderByDescending(r => r.CreatedAt)
                    .Take(10)
                    .ToListAsync();
            }
            else
            {
                model.RecentRequests = await requestsQuery
                    .Where(r => r.RequesterId == userId)
                    .Include(r => r.FormTemplate)
                    .Include(r => r.Requester)
                    .OrderByDescending(r => r.CreatedAt)
                    .Take(5)
                    .ToListAsync();
            }

            // Pending approvals for managers
            if (isManager || isAdmin || isHR)
            {
                model.PendingApprovals = await _context.Requests
                    .Include(r => r.Requester)
                    .Include(r => r.FormTemplate)
                    .Where(r => r.TenantId == tenantId &&
                        r.Approvals.Any(a => a.ApproverId == userId && a.Status == "Pending"))
                    .OrderByDescending(r => r.CreatedAt)
                    .Take(10)
                    .ToListAsync();
            }

            // Recent notifications
            model.RecentNotifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Take(5)
                .ToListAsync();

            // Chart: Requests by month (last 6 months)
            var sixMonthsAgo = DateTime.Now.AddMonths(-6);
            var monthlyData = await requestsQuery
                .Where(r => r.CreatedAt >= sixMonthsAgo)
                .GroupBy(r => new { r.CreatedAt.Year, r.CreatedAt.Month })
                .Select(g => new { Key = g.Key.Year + "-" + g.Key.Month.ToString("D2"), Count = g.Count() })
                .ToListAsync();
            foreach (var m in monthlyData)
                model.RequestsByMonth[m.Key] = m.Count;

            // Chart: Requests by department
            var deptData = await requestsQuery
                .Include(r => r.Requester).ThenInclude(u => u!.Department)
                .Where(r => r.Requester != null && r.Requester.Department != null)
                .GroupBy(r => r.Requester!.Department!.Name)
                .Select(g => new { Dept = g.Key, Count = g.Count() })
                .ToListAsync();
            foreach (var d in deptData)
                model.RequestsByDepartment[d.Dept] = d.Count;

            // Chart: Requests by status
            var statusData = await requestsQuery
                .GroupBy(r => r.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();
            foreach (var s in statusData)
                model.RequestsByStatus[s.Status] = s.Count;

            // Chart: Requests by type
            var typeData = await requestsQuery
                .Include(r => r.FormTemplate)
                .GroupBy(r => r.FormTemplate!.Name)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToListAsync();
            foreach (var t in typeData)
                model.RequestsByType[t.Type] = t.Count;

            // Team leave balances (for managers)
            if (isManager || isHR || isAdmin)
            {
                var teamUserIds = await _context.UserManagers
                    .Where(um => um.ManagerId == userId && (um.EndDate == null || um.EndDate > DateTime.Now))
                    .Select(um => um.UserId)
                    .ToListAsync();

                var teamBalances = await _context.LeaveBalances
                    .Include(lb => lb.User)
                    .Where(lb => teamUserIds.Contains(lb.UserId) && lb.LeaveTypeId == 1 && lb.Year == DateTime.Now.Year)
                    .ToListAsync();

                model.TeamLeaveBalances = teamBalances.Select(lb => new LeaveBalanceSummary
                {
                    EmployeeName = lb.User?.FullName ?? "",
                    TotalEntitled = lb.TotalEntitled,
                    Used = lb.Used,
                    Remaining = lb.Remaining
                }).ToList();
            }

            return View(model);
        }
    }
}
