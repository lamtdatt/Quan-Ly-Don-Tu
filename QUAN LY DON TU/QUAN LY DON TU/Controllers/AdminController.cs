using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DANGCAPNE.Data;
using DANGCAPNE.Models.Organization;
using DANGCAPNE.Models.Workflow;
using DANGCAPNE.ViewModels;

namespace DANGCAPNE.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string tab = "users")
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var roles = (HttpContext.Session.GetString("Roles") ?? "").Split(",");
            if (!roles.Contains("Admin")) return RedirectToAction("AccessDenied", "Account");
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var model = new AdminViewModel
            {
                Users = await _context.Users.Include(u => u.Department).Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                    .Where(u => u.TenantId == tenantId).ToListAsync(),
                Departments = await _context.Departments.Where(d => d.TenantId == tenantId).ToListAsync(),
                Roles = await _context.Roles.Where(r => r.TenantId == tenantId).ToListAsync(),
                FormTemplates = await _context.FormTemplates.Include(f => f.Workflow).Where(f => f.TenantId == tenantId).ToListAsync(),
                Workflows = await _context.Workflows.Include(w => w.Steps).Where(w => w.TenantId == tenantId).ToListAsync(),
                Branches = await _context.Branches.Where(b => b.TenantId == tenantId).ToListAsync(),
                ActiveTab = tab
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == id);

            var manager = await _context.UserManagers
                .Where(um => um.UserId == id && um.IsPrimary && (um.EndDate == null || um.EndDate > DateTime.Now))
                .FirstOrDefaultAsync();

            var model = new UserEditViewModel
            {
                User = user,
                AllRoles = await _context.Roles.Where(r => r.TenantId == tenantId).ToListAsync(),
                SelectedRoleIds = user?.UserRoles.Select(ur => ur.RoleId).ToList() ?? new(),
                Departments = await _context.Departments.Where(d => d.TenantId == tenantId).ToListAsync(),
                Branches = await _context.Branches.Where(b => b.TenantId == tenantId).ToListAsync(),
                JobTitles = await _context.JobTitles.Where(j => j.TenantId == tenantId).ToListAsync(),
                Positions = await _context.Positions.Where(p => p.TenantId == tenantId).ToListAsync(),
                ManagerId = manager?.ManagerId,
                PotentialManagers = await _context.Users.Where(u => u.TenantId == tenantId && u.Id != id && u.Status == "Active").ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserEditViewModel model, int[] roleIds)
        {
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            User user;
            if (model.User?.Id > 0)
            {
                user = await _context.Users.FindAsync(model.User.Id) ?? new();
                user.FullName = model.User.FullName;
                user.Email = model.User.Email;
                user.Phone = model.User.Phone;
                user.EmployeeCode = model.User.EmployeeCode;
                user.DepartmentId = model.User.DepartmentId;
                user.BranchId = model.User.BranchId;
                user.JobTitleId = model.User.JobTitleId;
                user.PositionId = model.User.PositionId;
                user.Status = model.User.Status;
                user.UpdatedAt = DateTime.Now;
            }
            else
            {
                user = new User
                {
                    TenantId = tenantId,
                    FullName = model.User?.FullName ?? "",
                    Email = model.User?.Email ?? "",
                    Phone = model.User?.Phone ?? "",
                    EmployeeCode = model.User?.EmployeeCode ?? "",
                    DepartmentId = model.User?.DepartmentId,
                    BranchId = model.User?.BranchId,
                    JobTitleId = model.User?.JobTitleId,
                    PositionId = model.User?.PositionId,
                    PasswordHash = HashPassword("Default@123"),
                    Status = "Active"
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            // Update roles
            var existingRoles = await _context.UserRoles.Where(ur => ur.UserId == user.Id).ToListAsync();
            _context.UserRoles.RemoveRange(existingRoles);
            foreach (var roleId in roleIds)
            {
                _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = roleId });
            }

            // Update manager
            if (model.ManagerId.HasValue)
            {
                var existingMgr = await _context.UserManagers.Where(um => um.UserId == user.Id && um.IsPrimary).ToListAsync();
                _context.UserManagers.RemoveRange(existingMgr);
                _context.UserManagers.Add(new UserManager { UserId = user.Id, ManagerId = model.ManagerId.Value, IsPrimary = true });
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Lưu thông tin nhân viên thành công!";
            return RedirectToAction("Index", new { tab = "users" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(string name, string code, int? managerId)
        {
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;
            _context.Departments.Add(new Department
            {
                TenantId = tenantId,
                Name = name,
                Code = code,
                ManagerId = managerId
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Tạo phòng ban thành công!";
            return RedirectToAction("Index", new { tab = "departments" });
        }

        [HttpGet]
        public async Task<IActionResult> FormBuilder(int? id)
        {
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var model = new FormBuilderViewModel
            {
                FormTemplate = id.HasValue
                    ? await _context.FormTemplates
                        .Include(f => f.Fields.OrderBy(ff => ff.DisplayOrder))
                            .ThenInclude(ff => ff.Options)
                        .FirstOrDefaultAsync(f => f.Id == id)
                    : new FormTemplate { TenantId = tenantId },
                Workflows = await _context.Workflows.Where(w => w.TenantId == tenantId).ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveFormTemplate(FormTemplate template, string fieldsJson)
        {
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;
            template.TenantId = tenantId;

            if (template.Id > 0)
            {
                var existing = await _context.FormTemplates.FindAsync(template.Id);
                if (existing != null)
                {
                    existing.Name = template.Name;
                    existing.Description = template.Description;
                    existing.Category = template.Category;
                    existing.Icon = template.Icon;
                    existing.WorkflowId = template.WorkflowId;
                    existing.RequiresFinancialApproval = template.RequiresFinancialApproval;
                    existing.IsActive = template.IsActive;
                }
            }
            else
            {
                _context.FormTemplates.Add(template);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Lưu biểu mẫu thành công!";
            return RedirectToAction("Index", new { tab = "forms" });
        }

        [HttpGet]
        public async Task<IActionResult> Delegation()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var model = new DelegationViewModel
            {
                ActiveDelegations = await _context.Delegations
                    .Include(d => d.Delegator).Include(d => d.Delegate)
                    .Where(d => d.TenantId == tenantId && d.IsActive)
                    .ToListAsync(),
                PotentialDelegates = await _context.Users
                    .Where(u => u.TenantId == tenantId && u.Id != userId && u.Status == "Active")
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDelegation(DelegationViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            if (model.DelegateId.HasValue && model.StartDate.HasValue && model.EndDate.HasValue)
            {
                _context.Delegations.Add(new Delegation
                {
                    TenantId = tenantId,
                    DelegatorId = userId.Value,
                    DelegateId = model.DelegateId.Value,
                    StartDate = model.StartDate.Value,
                    EndDate = model.EndDate.Value,
                    Reason = model.Reason ?? "",
                    IsActive = true
                });
                await _context.SaveChangesAsync();
                TempData["Success"] = "Ủy quyền thành công!";
            }

            return RedirectToAction("Delegation");
        }

        [HttpGet]
        public async Task<IActionResult> AuditLogs(int? requestId, int page = 1)
        {
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var query = _context.RequestAuditLogs
                .Include(a => a.User)
                .Include(a => a.Request)
                .Where(a => a.Request!.TenantId == tenantId);

            if (requestId.HasValue)
                query = query.Where(a => a.RequestId == requestId);

            var logs = await query.OrderByDescending(a => a.CreatedAt)
                .Skip((page - 1) * 50).Take(50)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.RequestId = requestId;
            return View(logs);
        }

        [HttpGet]
        public async Task<IActionResult> SystemErrors(int page = 1)
        {
            var errors = await _context.SystemErrors
                .OrderByDescending(e => e.OccurredAt)
                .Skip((page - 1) * 50).Take(50)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            return View(errors);
        }

        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + "DANGCAPNE_SALT"));
            return Convert.ToBase64String(bytes);
        }
    }
}
