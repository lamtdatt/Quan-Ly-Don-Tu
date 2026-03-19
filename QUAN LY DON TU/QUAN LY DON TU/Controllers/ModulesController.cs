using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DANGCAPNE.Data;
using DANGCAPNE.Filters;
using DANGCAPNE.ViewModels;
using DANGCAPNE.Models.Security;
using DANGCAPNE.Models.HR;
using DANGCAPNE.Models.Training;
using DANGCAPNE.Models.AdminOps;
using DANGCAPNE.Models.Compliance;
using DANGCAPNE.Models.Timekeeping;
using DANGCAPNE.Models.Organization;
using DANGCAPNE.Models.Workflow;
using DANGCAPNE.Models.Requests;

namespace DANGCAPNE.Controllers
{
    [RoleAuthorize("Admin", "HR", "Manager")]
    public class ModulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var modules = GetModulesForCurrentUser();
            var model = new ModulesIndexViewModel
            {
                Modules = modules.Select(m => new ModuleLinkViewModel
                {
                    Key = m.Key,
                    Title = m.Title,
                    Description = m.Description
                }).ToList()
            };
            ViewData["Title"] = "Modules";
            return View(model);
        }

        public IActionResult Rbac()
        {
            var keys = new[] { "permissions", "rolepermissions", "userpermissions" };
            ViewData["Title"] = "RBAC";
            return View("Group", BuildGroupViewModel("RBAC", keys));
        }

        public IActionResult Recruitment()
        {
            var keys = new[]
            {
                "jobrequisitions", "jobrequisitionapprovals", "candidates", "candidateapplications",
                "interviewschedules", "offerletters", "onboardingtasktemplates", "onboardingtasks",
                "offboardingtasktemplates", "offboardingtasks"
            };
            ViewData["Title"] = "Recruitment & Onboarding";
            return View("Group", BuildGroupViewModel("Recruitment & Onboarding", keys));
        }

        public IActionResult TrainingCompliance()
        {
            var keys = new[]
            {
                "trainingcourses", "trainingenrollments", "certifications", "certificationrenewals",
                "policydocuments", "policyacknowledgements"
            };
            ViewData["Title"] = "Training & Compliance";
            return View("Group", BuildGroupViewModel("Training & Compliance", keys));
        }

        public IActionResult AdminOps()
        {
            var keys = new[] { "assetassignments", "assetincidents", "carbookings", "mealregistrations", "uniformrequests" };
            ViewData["Title"] = "Admin Ops";
            return View("Group", BuildGroupViewModel("Admin Ops", keys));
        }

        [HttpGet]
        public async Task<IActionResult> List(string key)
        {
            var module = GetModuleOrDeny(key, out var denyResult);
            if (denyResult != null) return denyResult;

            var entityType = module!.EntityType;
            var query = GetQueryable(entityType);
            query = ApplyAsNoTracking(entityType, query);
            var rows = await ToListAsync(entityType, query);

            var columns = GetScalarProperties(entityType).Select(p => p.Name).ToList();
            var rowData = rows.Select(r => ToRowDict(r, columns)).ToList();

            var selectLookups = BuildSelectLookups(module.Key);
            if (selectLookups.Count > 0)
            {
                foreach (var row in rowData)
                {
                    foreach (var lookup in selectLookups)
                    {
                        if (row.TryGetValue(lookup.Key, out var value) && value != null)
                        {
                            var keyStr = Convert.ToString(value, CultureInfo.InvariantCulture);
                            if (keyStr != null && lookup.Value.TryGetValue(keyStr, out var label))
                            {
                                row[lookup.Key] = label;
                            }
                        }
                    }
                }
            }

            var model = new DynamicCrudListViewModel
            {
                EntityKey = module.Key,
                Title = module.Title,
                Columns = columns,
                Rows = rowData
            };

            ViewData["Title"] = module.Title;
            return View(model);
        }

        [HttpGet]
        public IActionResult Create(string key)
        {
            var module = GetModuleOrDeny(key, out var denyResult);
            if (denyResult != null) return denyResult;

            var model = BuildEditModel(module!, null);
            ViewData["Title"] = "Create " + module!.Title;
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string key, DynamicCrudEditPostModel post)
        {
            var module = GetModuleOrDeny(key, out var denyResult);
            if (denyResult != null) return denyResult;

            var entityType = module!.EntityType;
            var entity = Activator.CreateInstance(entityType);
            if (entity == null) return BadRequest("Failed to create entity.");

            ApplyPostToEntity(entity, post);
            ApplySystemDefaults(entity);

            _context.Add(entity);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Created successfully.";
            return RedirectToAction("List", new { key = module.Key });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string key, int id)
        {
            var module = GetModuleOrDeny(key, out var denyResult);
            if (denyResult != null) return denyResult;

            var entity = await _context.FindAsync(module!.EntityType, id);
            if (entity == null) return NotFound();

            var model = BuildEditModel(module, entity);
            ViewData["Title"] = "Edit " + module.Title;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string key, int id, DynamicCrudEditPostModel post)
        {
            var module = GetModuleOrDeny(key, out var denyResult);
            if (denyResult != null) return denyResult;

            var entity = await _context.FindAsync(module!.EntityType, id);
            if (entity == null) return NotFound();

            ApplyPostToEntity(entity, post, skipKey: true);
            ApplySystemDefaults(entity, isUpdate: true);

            await _context.SaveChangesAsync();
            TempData["Success"] = "Updated successfully.";
            return RedirectToAction("List", new { key = module.Key });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string key, int id)
        {
            var module = GetModuleOrDeny(key, out var denyResult);
            if (denyResult != null) return denyResult;

            var entity = await _context.FindAsync(module!.EntityType, id);
            if (entity == null) return NotFound();

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Deleted successfully.";
            return RedirectToAction("List", new { key = module.Key });
        }

        private DynamicCrudEditViewModel BuildEditModel(ModuleConfig module, object? entity)
        {
            var props = GetScalarProperties(module.EntityType);
            var fields = new List<DynamicField>();
            var fieldConfigs = GetFieldConfigs(module.Key);
            foreach (var p in props)
            {
                var isKey = string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase);
                if (entity == null && isKey) continue;

                var value = entity == null ? null : p.GetValue(entity);
                var fieldConfig = fieldConfigs.TryGetValue(p.Name, out var fc) ? fc : null;
                var selectOptions = fieldConfig?.Select != null ? LoadSelectOptions(fieldConfig.Select) : new List<SelectOption>();
                var inputType = fieldConfig?.Select != null ? "select" : GetInputType(p.PropertyType);

                var readOnly = isKey && entity != null;
                if (ReadOnlyFields.Contains(p.Name))
                {
                    readOnly = true;
                }

                fields.Add(new DynamicField
                {
                    Name = p.Name,
                    DisplayName = fieldConfig?.DisplayName ?? p.Name,
                    Type = inputType,
                    Value = FormatValue(value, p.PropertyType),
                    IsKey = isKey,
                    ReadOnly = readOnly,
                    Options = selectOptions
                });
            }

            return new DynamicCrudEditViewModel
            {
                EntityKey = module.Key,
                Title = module.Title,
                Fields = fields,
                IsEdit = entity != null
            };
        }

        private static IEnumerable<System.Reflection.PropertyInfo> GetScalarProperties(Type type)
        {
            return type.GetProperties()
                .Where(p => p.CanRead && p.CanWrite)
                .Where(p => IsScalarType(p.PropertyType));
        }

        private IQueryable GetQueryable(Type entityType)
        {
            var setMethod = typeof(DbContext).GetMethod("Set", Type.EmptyTypes);
            if (setMethod == null)
                throw new InvalidOperationException("DbContext.Set<T>() not found.");
            var generic = setMethod.MakeGenericMethod(entityType);
            var query = (IQueryable?)generic.Invoke(_context, null);
            if (query == null)
                throw new InvalidOperationException("Failed to create queryable for entity.");
            return query;
        }

        private static IQueryable ApplyAsNoTracking(Type entityType, IQueryable query)
        {
            var method = typeof(EntityFrameworkQueryableExtensions).GetMethods()
                .FirstOrDefault(m => m.Name == "AsNoTracking" && m.GetParameters().Length == 1);
            if (method == null) return query;
            var generic = method.MakeGenericMethod(entityType);
            var result = (IQueryable?)generic.Invoke(null, new object[] { query });
            return result ?? query;
        }

        private static async Task<List<object>> ToListAsync(Type entityType, IQueryable query)
        {
            var method = typeof(EntityFrameworkQueryableExtensions).GetMethods()
                .FirstOrDefault(m => m.Name == "ToListAsync" && m.GetParameters().Length == 2);
            if (method == null)
                return query.Cast<object>().ToList();

            var generic = method.MakeGenericMethod(entityType);
            var task = (Task?)generic.Invoke(null, new object[] { query, CancellationToken.None });
            if (task == null)
                return query.Cast<object>().ToList();

            await task.ConfigureAwait(false);
            var resultProp = task.GetType().GetProperty("Result");
            var result = resultProp?.GetValue(task) as System.Collections.IEnumerable;
            if (result == null)
                return new List<object>();
            return result.Cast<object>().ToList();
        }

        private static bool IsScalarType(Type type)
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            if (t.IsEnum) return true;
            return t == typeof(string)
                || t == typeof(int)
                || t == typeof(long)
                || t == typeof(decimal)
                || t == typeof(double)
                || t == typeof(float)
                || t == typeof(bool)
                || t == typeof(DateTime)
                || t == typeof(Guid)
                || t == typeof(short)
                || t == typeof(byte);
        }

        private static string GetInputType(Type type)
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            if (t == typeof(DateTime)) return "datetime-local";
            if (t == typeof(bool)) return "checkbox";
            if (t == typeof(int) || t == typeof(long) || t == typeof(decimal) || t == typeof(double) || t == typeof(float) || t == typeof(short) || t == typeof(byte))
                return "number";
            return "text";
        }

        private static string? FormatValue(object? value, Type type)
        {
            if (value == null) return null;
            var t = Nullable.GetUnderlyingType(type) ?? type;
            if (t == typeof(DateTime))
            {
                var dt = (DateTime)value;
                return dt.ToString("yyyy-MM-ddTHH:mm");
            }
            if (t == typeof(bool))
            {
                return ((bool)value) ? "true" : "false";
            }
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        private static readonly HashSet<string> ReadOnlyFields = new(StringComparer.OrdinalIgnoreCase)
        {
            "CreatedAt",
            "UpdatedAt"
        };

        private void ApplyPostToEntity(object entity, DynamicCrudEditPostModel post, bool skipKey = false)
        {
            var type = entity.GetType();
            foreach (var kv in post.Fields)
            {
                var prop = type.GetProperty(kv.Key);
                if (prop == null || !prop.CanWrite) continue;
                if (skipKey && string.Equals(prop.Name, "Id", StringComparison.OrdinalIgnoreCase)) continue;
                if (!IsScalarType(prop.PropertyType)) continue;

                var converted = ConvertToType(kv.Value, prop.PropertyType);
                prop.SetValue(entity, converted);
            }
        }

        private static object? ConvertToType(string? value, Type targetType)
        {
            var isNullable = Nullable.GetUnderlyingType(targetType) != null;
            var t = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (string.IsNullOrWhiteSpace(value))
            {
                if (isNullable) return null;
                if (t == typeof(string)) return string.Empty;
                return Activator.CreateInstance(t);
            }

            if (t == typeof(bool))
            {
                return value == "on" || value.Equals("true", StringComparison.OrdinalIgnoreCase) || value == "1";
            }

            if (t == typeof(DateTime))
            {
                if (DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None, out var dt))
                    return dt;
                if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                    return dt;
                return DateTime.Now;
            }

            if (t == typeof(decimal))
            {
                if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out var dec))
                    return dec;
                if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out dec))
                    return dec;
                return 0m;
            }

            if (t == typeof(double))
            {
                if (double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out var d))
                    return d;
                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                    return d;
                return 0d;
            }

            if (t == typeof(float))
            {
                if (float.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out var f))
                    return f;
                if (float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out f))
                    return f;
                return 0f;
            }

            try
            {
                return Convert.ChangeType(value, t, CultureInfo.InvariantCulture);
            }
            catch
            {
                return Activator.CreateInstance(t);
            }
        }

        private void ApplySystemDefaults(object entity, bool isUpdate = false)
        {
            var type = entity.GetType();
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            var tenantProp = type.GetProperty("TenantId");
            if (tenantProp != null && tenantProp.PropertyType == typeof(int))
            {
                var current = (int)(tenantProp.GetValue(entity) ?? 0);
                if (current == 0) tenantProp.SetValue(entity, tenantId);
            }

            var createdAt = type.GetProperty("CreatedAt");
            if (createdAt != null && createdAt.PropertyType == typeof(DateTime))
            {
                var current = (DateTime)(createdAt.GetValue(entity) ?? DateTime.MinValue);
                if (current == DateTime.MinValue) createdAt.SetValue(entity, DateTime.Now);
            }

            var updatedAt = type.GetProperty("UpdatedAt");
            if (updatedAt != null && updatedAt.PropertyType == typeof(DateTime))
            {
                updatedAt.SetValue(entity, DateTime.Now);
            }
        }

        private static Dictionary<string, object?> ToRowDict(object entity, List<string> columns)
        {
            var dict = new Dictionary<string, object?>();
            var type = entity.GetType();
            foreach (var col in columns)
            {
                var prop = type.GetProperty(col);
                if (prop == null) continue;
                var value = prop.GetValue(entity);
                dict[col] = value;
            }
            return dict;
        }

        private Dictionary<string, Dictionary<string, string>> BuildSelectLookups(string moduleKey)
        {
            var configs = GetFieldConfigs(moduleKey);
            var result = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
            foreach (var kv in configs)
            {
                if (kv.Value.Select == null) continue;
                var options = LoadSelectOptions(kv.Value.Select);
                result[kv.Key] = options.ToDictionary(o => o.Value, o => o.Label);
            }
            return result;
        }

        private static List<SelectOption> LoadSelectOptions(SelectConfig select)
        {
            var options = new List<SelectOption>();
            foreach (var item in select.Query.Invoke())
            {
                var valueProp = item.GetType().GetProperty(select.ValueField);
                var labelProp = item.GetType().GetProperty(select.LabelField);
                var value = valueProp?.GetValue(item);
                var label = labelProp?.GetValue(item);
                if (value == null) continue;
                options.Add(new SelectOption
                {
                    Value = Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty,
                    Label = Convert.ToString(label, CultureInfo.InvariantCulture) ?? string.Empty
                });
            }
            return options;
        }

        private ModuleConfig? GetModuleOrDeny(string key, out IActionResult? denyResult)
        {
            denyResult = null;
            if (!Modules.TryGetValue(NormalizeKey(key), out var module))
            {
                return null;
            }

            var roles = (HttpContext.Session.GetString("Roles") ?? string.Empty)
                .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (!module.AllowedRoles.Any(r => roles.Contains(r)))
            {
                denyResult = RedirectToAction("AccessDenied", "Account");
                return null;
            }

            return module;
        }

        private IEnumerable<ModuleConfig> GetModulesForCurrentUser()
        {
            var roles = (HttpContext.Session.GetString("Roles") ?? string.Empty)
                .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            return Modules.Values.Where(m => m.AllowedRoles.Any(r => roles.Contains(r)));
        }

        private ModulesIndexViewModel BuildGroupViewModel(string title, IEnumerable<string> keys)
        {
            var keySet = new HashSet<string>(keys.Select(NormalizeKey));
            var modules = GetModulesForCurrentUser().Where(m => keySet.Contains(NormalizeKey(m.Key)));
            return new ModulesIndexViewModel
            {
                Modules = modules.Select(m => new ModuleLinkViewModel
                {
                    Key = m.Key,
                    Title = m.Title,
                    Description = m.Description
                }).ToList()
            };
        }

        private static string NormalizeKey(string key) => (key ?? string.Empty).Trim().ToLowerInvariant();

        private sealed class ModuleConfig
        {
            public ModuleConfig(string key, string title, string description, Type entityType, params string[] allowedRoles)
            {
                Key = key;
                Title = title;
                Description = description;
                EntityType = entityType;
                AllowedRoles = allowedRoles.Length == 0 ? Array.Empty<string>() : allowedRoles;
            }

            public string Key { get; }
            public string Title { get; }
            public string Description { get; }
            public Type EntityType { get; }
            public string[] AllowedRoles { get; }
        }

        private sealed class FieldConfig
        {
            public string DisplayName { get; set; } = string.Empty;
            public SelectConfig? Select { get; set; }
        }

        private sealed class SelectConfig
        {
            public Func<IEnumerable<object>> Query { get; set; } = () => Array.Empty<object>();
            public string ValueField { get; set; } = "Id";
            public string LabelField { get; set; } = "Name";
        }

        private Dictionary<string, FieldConfig> GetFieldConfigs(string moduleKey)
        {
            moduleKey = NormalizeKey(moduleKey);
            return FieldConfigs.TryGetValue(moduleKey, out var cfg)
                ? cfg
                : new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase);
        }

        private static readonly Dictionary<string, ModuleConfig> Modules = new(StringComparer.OrdinalIgnoreCase)
        {
            // RBAC
            ["permissions"] = new ModuleConfig("permissions", "Permissions", "Danh sách quyền hệ thống", typeof(Permission), "Admin"),
            ["rolepermissions"] = new ModuleConfig("rolepermissions", "Role Permissions", "Gán quyền cho vai trò", typeof(RolePermission), "Admin"),
            ["userpermissions"] = new ModuleConfig("userpermissions", "User Permissions", "Gán quyền cho người dùng", typeof(UserPermission), "Admin"),

            // Recruitment
            ["jobrequisitions"] = new ModuleConfig("jobrequisitions", "Job Requisitions", "Đơn yêu cầu tuyển dụng", typeof(JobRequisition), "Admin", "HR", "Manager"),
            ["jobrequisitionapprovals"] = new ModuleConfig("jobrequisitionapprovals", "Job Requisition Approvals", "Duyệt yêu cầu tuyển dụng", typeof(JobRequisitionApproval), "Admin", "HR", "Manager"),
            ["candidates"] = new ModuleConfig("candidates", "Candidates", "Ứng viên", typeof(Candidate), "Admin", "HR"),
            ["candidateapplications"] = new ModuleConfig("candidateapplications", "Candidate Applications", "Hồ sơ ứng tuyển", typeof(CandidateApplication), "Admin", "HR"),
            ["interviewschedules"] = new ModuleConfig("interviewschedules", "Interview Schedules", "Lịch phỏng vấn", typeof(InterviewSchedule), "Admin", "HR"),
            ["offerletters"] = new ModuleConfig("offerletters", "Offer Letters", "Thư mời nhận việc", typeof(OfferLetter), "Admin", "HR"),

            // Onboarding & Offboarding
            ["onboardingtasktemplates"] = new ModuleConfig("onboardingtasktemplates", "Onboarding Task Templates", "Mẫu công việc onboarding", typeof(OnboardingTaskTemplate), "Admin", "HR"),
            ["onboardingtasks"] = new ModuleConfig("onboardingtasks", "Onboarding Tasks", "Công việc onboarding", typeof(OnboardingTask), "Admin", "HR"),
            ["offboardingtasktemplates"] = new ModuleConfig("offboardingtasktemplates", "Offboarding Task Templates", "Mẫu công việc offboarding", typeof(OffboardingTaskTemplate), "Admin", "HR"),
            ["offboardingtasks"] = new ModuleConfig("offboardingtasks", "Offboarding Tasks", "Công việc offboarding", typeof(OffboardingTask), "Admin", "HR"),

            // Performance
            ["performancecycles"] = new ModuleConfig("performancecycles", "Performance Cycles", "Chu kỳ đánh giá", typeof(PerformanceCycle), "Admin", "HR"),
            ["performancegoals"] = new ModuleConfig("performancegoals", "Performance Goals", "Mục tiêu KPI", typeof(PerformanceGoal), "Admin", "HR", "Manager"),
            ["performancereviews"] = new ModuleConfig("performancereviews", "Performance Reviews", "Đánh giá hiệu suất", typeof(PerformanceReview), "Admin", "HR", "Manager"),
            ["performancereviewitems"] = new ModuleConfig("performancereviewitems", "Performance Review Items", "Chi tiết đánh giá", typeof(PerformanceReviewItem), "Admin", "HR", "Manager"),

            // Compensation
            ["salaryadjustmentrequests"] = new ModuleConfig("salaryadjustmentrequests", "Salary Adjustment Requests", "Đề xuất tăng lương", typeof(SalaryAdjustmentRequest), "Admin", "HR", "Manager"),
            ["bonusrequests"] = new ModuleConfig("bonusrequests", "Bonus Requests", "Đề xuất thưởng", typeof(BonusRequest), "Admin", "HR", "Manager"),

            // Training
            ["trainingcourses"] = new ModuleConfig("trainingcourses", "Training Courses", "Khóa đào tạo", typeof(TrainingCourse), "Admin", "HR"),
            ["trainingenrollments"] = new ModuleConfig("trainingenrollments", "Training Enrollments", "Ghi danh đào tạo", typeof(TrainingEnrollment), "Admin", "HR"),
            ["certifications"] = new ModuleConfig("certifications", "Certifications", "Chứng chỉ", typeof(Certification), "Admin", "HR"),
            ["certificationrenewals"] = new ModuleConfig("certificationrenewals", "Certification Renewals", "Gia hạn chứng chỉ", typeof(CertificationRenewal), "Admin", "HR"),

            // Admin Ops
            ["assetassignments"] = new ModuleConfig("assetassignments", "Asset Assignments", "Cấp phát tài sản", typeof(AssetAssignment), "Admin", "HR"),
            ["assetincidents"] = new ModuleConfig("assetincidents", "Asset Incidents", "Báo hỏng/mất", typeof(AssetIncident), "Admin", "HR"),
            ["carbookings"] = new ModuleConfig("carbookings", "Car Bookings", "Đăng ký xe", typeof(CarBooking), "Admin", "HR"),
            ["mealregistrations"] = new ModuleConfig("mealregistrations", "Meal Registrations", "Đăng ký suất ăn", typeof(MealRegistration), "Admin", "HR"),
            ["uniformrequests"] = new ModuleConfig("uniformrequests", "Uniform Requests", "Đăng ký đồng phục", typeof(UniformRequest), "Admin", "HR"),

            // Compliance
            ["policydocuments"] = new ModuleConfig("policydocuments", "Policy Documents", "Văn bản chính sách", typeof(PolicyDocument), "Admin", "HR"),
            ["policyacknowledgements"] = new ModuleConfig("policyacknowledgements", "Policy Acknowledgements", "Xác nhận chính sách", typeof(PolicyAcknowledgement), "Admin", "HR"),

            // New Modules Extensions
            ["socialinsurances"] = new ModuleConfig("socialinsurances", "Social Insurance", "Quản lý BHXH", typeof(SocialInsurance), "Admin", "HR"),
            ["employeedocuments"] = new ModuleConfig("employeedocuments", "Employee Documents", "Hồ sơ đính kèm", typeof(EmployeeDocument), "Admin", "HR"),
            ["attendancelocationconfigs"] = new ModuleConfig("attendancelocationconfigs", "Attendance Configs", "Cấu hình chấm công QR/Wifi", typeof(AttendanceLocationConfig), "Admin", "HR"),
            ["shiftswaprequests"] = new ModuleConfig("shiftswaprequests", "Shift Swap Requests", "Đơn đổi ca", typeof(ShiftSwapRequest), "Admin", "HR", "Manager")
        };

        private Dictionary<string, Dictionary<string, FieldConfig>> FieldConfigs => new(StringComparer.OrdinalIgnoreCase)
        {
            ["rolepermissions"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["RoleId"] = new FieldConfig { DisplayName = "Role", Select = SelectRoles() },
                ["PermissionId"] = new FieldConfig { DisplayName = "Permission", Select = SelectPermissions() }
            },
            ["userpermissions"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() },
                ["PermissionId"] = new FieldConfig { DisplayName = "Permission", Select = SelectPermissions() },
                ["GrantedByUserId"] = new FieldConfig { DisplayName = "Granted By", Select = SelectUsers() }
            },
            ["jobrequisitions"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["DepartmentId"] = new FieldConfig { DisplayName = "Department", Select = SelectDepartments() },
                ["JobTitleId"] = new FieldConfig { DisplayName = "Job Title", Select = SelectJobTitles() },
                ["CreatedByUserId"] = new FieldConfig { DisplayName = "Created By", Select = SelectUsers() }
            },
            ["jobrequisitionapprovals"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["JobRequisitionId"] = new FieldConfig { DisplayName = "Job Requisition", Select = SelectJobRequisitions() },
                ["ApproverId"] = new FieldConfig { DisplayName = "Approver", Select = SelectUsers() }
            },
            ["candidateapplications"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["CandidateId"] = new FieldConfig { DisplayName = "Candidate", Select = SelectCandidates() },
                ["JobRequisitionId"] = new FieldConfig { DisplayName = "Job Requisition", Select = SelectJobRequisitions() }
            },
            ["interviewschedules"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["CandidateApplicationId"] = new FieldConfig { DisplayName = "Application", Select = SelectCandidateApplications() },
                ["InterviewerId"] = new FieldConfig { DisplayName = "Interviewer", Select = SelectUsers() }
            },
            ["offerletters"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["CandidateApplicationId"] = new FieldConfig { DisplayName = "Application", Select = SelectCandidateApplications() }
            },
            ["onboardingtasktemplates"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["DefaultAssigneeRoleId"] = new FieldConfig { DisplayName = "Default Assignee Role", Select = SelectRoles() }
            },
            ["onboardingtasks"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["TemplateId"] = new FieldConfig { DisplayName = "Template", Select = SelectOnboardingTemplates() },
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() },
                ["AssignedToUserId"] = new FieldConfig { DisplayName = "Assigned To", Select = SelectUsers() }
            },
            ["offboardingtasktemplates"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["DefaultAssigneeRoleId"] = new FieldConfig { DisplayName = "Default Assignee Role", Select = SelectRoles() }
            },
            ["offboardingtasks"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["TemplateId"] = new FieldConfig { DisplayName = "Template", Select = SelectOffboardingTemplates() },
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() },
                ["AssignedToUserId"] = new FieldConfig { DisplayName = "Assigned To", Select = SelectUsers() }
            },
            ["performancegoals"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["CycleId"] = new FieldConfig { DisplayName = "Cycle", Select = SelectPerformanceCycles() },
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() }
            },
            ["performancereviews"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["CycleId"] = new FieldConfig { DisplayName = "Cycle", Select = SelectPerformanceCycles() },
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() },
                ["ReviewerId"] = new FieldConfig { DisplayName = "Reviewer", Select = SelectUsers() }
            },
            ["performancereviewitems"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["ReviewId"] = new FieldConfig { DisplayName = "Review", Select = SelectPerformanceReviews() },
                ["GoalId"] = new FieldConfig { DisplayName = "Goal", Select = SelectPerformanceGoals() }
            },
            ["salaryadjustmentrequests"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() },
                ["RequestedByUserId"] = new FieldConfig { DisplayName = "Requested By", Select = SelectUsers() }
            },
            ["bonusrequests"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() },
                ["RequestedByUserId"] = new FieldConfig { DisplayName = "Requested By", Select = SelectUsers() }
            },
            ["trainingenrollments"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["CourseId"] = new FieldConfig { DisplayName = "Course", Select = SelectTrainingCourses() },
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() }
            },
            ["certifications"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() }
            },
            ["certificationrenewals"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["CertificationId"] = new FieldConfig { DisplayName = "Certification", Select = SelectCertifications() },
                ["ApprovedByUserId"] = new FieldConfig { DisplayName = "Approved By", Select = SelectUsers() }
            },
            ["assetassignments"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["AssetId"] = new FieldConfig { DisplayName = "Asset", Select = SelectAssets() },
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() }
            },
            ["assetincidents"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["AssetId"] = new FieldConfig { DisplayName = "Asset", Select = SelectAssets() },
                ["ReportedByUserId"] = new FieldConfig { DisplayName = "Reported By", Select = SelectUsers() }
            },
            ["carbookings"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() }
            },
            ["mealregistrations"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() }
            },
            ["uniformrequests"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() }
            },
            ["policyacknowledgements"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["PolicyDocumentId"] = new FieldConfig { DisplayName = "Policy", Select = SelectPolicyDocuments() },
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() }
            },
            ["socialinsurances"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() }
            },
            ["employeedocuments"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserId"] = new FieldConfig { DisplayName = "User", Select = SelectUsers() }
            },
            ["attendancelocationconfigs"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["BranchId"] = new FieldConfig { DisplayName = "Branch", Select = SelectBranches() }
            },
            ["shiftswaprequests"] = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase)
            {
                ["RequesterId"] = new FieldConfig { DisplayName = "Requester", Select = SelectUsers() },
                ["TargetUserId"] = new FieldConfig { DisplayName = "Target User", Select = SelectUsers() },
                ["RequesterShiftId"] = new FieldConfig { DisplayName = "Requester Shift", Select = SelectShifts() },
                ["TargetShiftId"] = new FieldConfig { DisplayName = "Target Shift", Select = SelectShifts() },
                ["ApprovedByManagerId"] = new FieldConfig { DisplayName = "Approved By", Select = SelectUsers() }
            }
        };

        private SelectConfig SelectRoles() => new()
        {
            Query = () => _context.Roles.AsNoTracking().ToList<object>(),
            LabelField = "Name"
        };

        private SelectConfig SelectPermissions() => new()
        {
            Query = () => _context.Permissions.AsNoTracking().ToList<object>(),
            LabelField = "Name"
        };

        private SelectConfig SelectUsers() => new()
        {
            Query = () => _context.Users.AsNoTracking().ToList<object>(),
            LabelField = "FullName"
        };

        private SelectConfig SelectDepartments() => new()
        {
            Query = () => _context.Departments.AsNoTracking().ToList<object>(),
            LabelField = "Name"
        };

        private SelectConfig SelectJobTitles() => new()
        {
            Query = () => _context.JobTitles.AsNoTracking().ToList<object>(),
            LabelField = "Name"
        };

        private SelectConfig SelectJobRequisitions() => new()
        {
            Query = () => _context.JobRequisitions.AsNoTracking().ToList<object>(),
            LabelField = "Title"
        };

        private SelectConfig SelectCandidates() => new()
        {
            Query = () => _context.Candidates.AsNoTracking().ToList<object>(),
            LabelField = "FullName"
        };

        private SelectConfig SelectCandidateApplications() => new()
        {
            Query = () => _context.CandidateApplications.AsNoTracking().ToList<object>(),
            LabelField = "Id"
        };

        private SelectConfig SelectOnboardingTemplates() => new()
        {
            Query = () => _context.OnboardingTaskTemplates.AsNoTracking().ToList<object>(),
            LabelField = "Name"
        };

        private SelectConfig SelectOffboardingTemplates() => new()
        {
            Query = () => _context.OffboardingTaskTemplates.AsNoTracking().ToList<object>(),
            LabelField = "Name"
        };

        private SelectConfig SelectPerformanceCycles() => new()
        {
            Query = () => _context.PerformanceCycles.AsNoTracking().ToList<object>(),
            LabelField = "Name"
        };

        private SelectConfig SelectPerformanceReviews() => new()
        {
            Query = () => _context.PerformanceReviews.AsNoTracking().ToList<object>(),
            LabelField = "Id"
        };

        private SelectConfig SelectPerformanceGoals() => new()
        {
            Query = () => _context.PerformanceGoals.AsNoTracking().ToList<object>(),
            LabelField = "Title"
        };

        private SelectConfig SelectTrainingCourses() => new()
        {
            Query = () => _context.TrainingCourses.AsNoTracking().ToList<object>(),
            LabelField = "Name"
        };

        private SelectConfig SelectCertifications() => new()
        {
            Query = () => _context.Certifications.AsNoTracking().ToList<object>(),
            LabelField = "Name"
        };

        private SelectConfig SelectAssets() => new()
        {
            Query = () => _context.Assets.AsNoTracking().ToList<object>(),
            LabelField = "Name"
        };

        private SelectConfig SelectPolicyDocuments() => new()
        {
            Query = () => _context.PolicyDocuments.AsNoTracking().ToList<object>(),
            LabelField = "Title"
        };

        private SelectConfig SelectBranches() => new()
        {
            Query = () => _context.Branches.AsNoTracking().ToList<object>(),
            LabelField = "Name"
        };

        private SelectConfig SelectShifts() => new()
        {
            Query = () => _context.Shifts.AsNoTracking().ToList<object>(),
            LabelField = "Name"
        };
    }
}
