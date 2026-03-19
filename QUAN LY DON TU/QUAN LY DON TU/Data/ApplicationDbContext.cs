using Microsoft.EntityFrameworkCore;
using DANGCAPNE.Models.Organization;
using DANGCAPNE.Models.Workflow;
using DANGCAPNE.Models.Requests;
using DANGCAPNE.Models.Timekeeping;
using DANGCAPNE.Models.Finance;
using DANGCAPNE.Models.SystemModels;
using DANGCAPNE.Models.Security;
using DANGCAPNE.Models.HR;
using DANGCAPNE.Models.Training;
using DANGCAPNE.Models.AdminOps;
using DANGCAPNE.Models.Compliance;

namespace DANGCAPNE.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Module 1: Organization & Identity
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<UserManager> UserManagers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

        // Module 1B: RBAC Extensions
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        // Module 2: Workflow & Dynamic Forms
        public DbSet<FormTemplate> FormTemplates { get; set; }
        public DbSet<FormField> FormFields { get; set; }
        public DbSet<FormFieldOption> FormFieldOptions { get; set; }
        public DbSet<WorkflowDef> Workflows { get; set; }
        public DbSet<WorkflowStep> WorkflowSteps { get; set; }
        public DbSet<WorkflowCondition> WorkflowConditions { get; set; }
        public DbSet<WorkflowStepApprover> WorkflowStepApprovers { get; set; }
        public DbSet<Delegation> Delegations { get; set; }
        public DbSet<SlaConfig> SlaConfigs { get; set; }
        public DbSet<EscalationRule> EscalationRules { get; set; }

        // Module 3: Requests & Execution
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestData> RequestData { get; set; }
        public DbSet<RequestAttachment> RequestAttachments { get; set; }
        public DbSet<RequestApproval> RequestApprovals { get; set; }
        public DbSet<RequestComment> RequestComments { get; set; }
        public DbSet<RequestFollower> RequestFollowers { get; set; }
        public DbSet<RequestAuditLog> RequestAuditLogs { get; set; }
        public DbSet<DraftRequest> DraftRequests { get; set; }

        // Module 3B: Recruitment & Lifecycle
        public DbSet<JobRequisition> JobRequisitions { get; set; }
        public DbSet<JobRequisitionApproval> JobRequisitionApprovals { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateApplication> CandidateApplications { get; set; }
        public DbSet<InterviewSchedule> InterviewSchedules { get; set; }
        public DbSet<OfferLetter> OfferLetters { get; set; }
        public DbSet<OnboardingTaskTemplate> OnboardingTaskTemplates { get; set; }
        public DbSet<OnboardingTask> OnboardingTasks { get; set; }
        public DbSet<OffboardingTaskTemplate> OffboardingTaskTemplates { get; set; }
        public DbSet<OffboardingTask> OffboardingTasks { get; set; }
        public DbSet<PerformanceCycle> PerformanceCycles { get; set; }
        public DbSet<PerformanceGoal> PerformanceGoals { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }
        public DbSet<PerformanceReviewItem> PerformanceReviewItems { get; set; }
        public DbSet<SalaryAdjustmentRequest> SalaryAdjustmentRequests { get; set; }
        public DbSet<BonusRequest> BonusRequests { get; set; }
        public DbSet<SocialInsurance> SocialInsurances { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }

        // Module 3C: Training & Compliance
        public DbSet<TrainingCourse> TrainingCourses { get; set; }
        public DbSet<TrainingEnrollment> TrainingEnrollments { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<CertificationRenewal> CertificationRenewals { get; set; }
        public DbSet<PolicyDocument> PolicyDocuments { get; set; }
        public DbSet<PolicyAcknowledgement> PolicyAcknowledgements { get; set; }

        // Module 3D: Admin Operations
        public DbSet<AssetAssignment> AssetAssignments { get; set; }
        public DbSet<AssetIncident> AssetIncidents { get; set; }
        public DbSet<CarBooking> CarBookings { get; set; }
        public DbSet<MealRegistration> MealRegistrations { get; set; }
        public DbSet<UniformRequest> UniformRequests { get; set; }

        // Module 4: Leave & Timekeeping
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<LeaveAccrual> LeaveAccruals { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<UserShift> UserShifts { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<DailyAttendance> DailyAttendances { get; set; }
        public DbSet<OvertimeRate> OvertimeRates { get; set; }
        public DbSet<AttendanceLocationConfig> AttendanceLocationConfigs { get; set; }
        public DbSet<ShiftSwapRequest> ShiftSwapRequests { get; set; }

        // Module 5: Finance & Utilities
        public DbSet<Project> Projects { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<AssetCategory> AssetCategories { get; set; }
        public DbSet<Asset> Assets { get; set; }

        // Module 6: Multi-Tenant & System
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConfig> TenantConfigs { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<SystemError> SystemErrors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserManager self-referencing relationship
            modelBuilder.Entity<UserManager>()
                .HasOne(um => um.User)
                .WithMany(u => u.ManagedBy)
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserManager>()
                .HasOne(um => um.Manager)
                .WithMany()
                .HasForeignKey(um => um.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Department self-referencing
            modelBuilder.Entity<Department>()
                .HasOne(d => d.ParentDepartment)
                .WithMany()
                .HasForeignKey(d => d.ParentDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Delegation
            modelBuilder.Entity<Delegation>()
                .HasOne(d => d.Delegator)
                .WithMany()
                .HasForeignKey(d => d.DelegatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Delegation>()
                .HasOne(d => d.Delegate)
                .WithMany()
                .HasForeignKey(d => d.DelegateId)
                .OnDelete(DeleteBehavior.Restrict);

            // Request
            modelBuilder.Entity<Request>()
                .HasOne(r => r.Requester)
                .WithMany()
                .HasForeignKey(r => r.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.FormTemplate)
                .WithMany()
                .HasForeignKey(r => r.FormTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            // RequestApproval
            modelBuilder.Entity<RequestApproval>()
                .HasOne(ra => ra.Approver)
                .WithMany()
                .HasForeignKey(ra => ra.ApproverId)
                .OnDelete(DeleteBehavior.Restrict);

            // RequestComment self-ref
            modelBuilder.Entity<RequestComment>()
                .HasOne(rc => rc.ParentComment)
                .WithMany()
                .HasForeignKey(rc => rc.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequestComment>()
                .HasOne(rc => rc.User)
                .WithMany()
                .HasForeignKey(rc => rc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // RequestAuditLog
            modelBuilder.Entity<RequestAuditLog>()
                .HasOne(al => al.User)
                .WithMany()
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ExchangeRate
            modelBuilder.Entity<ExchangeRate>()
                .HasOne(er => er.FromCurrency)
                .WithMany()
                .HasForeignKey(er => er.FromCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExchangeRate>()
                .HasOne(er => er.ToCurrency)
                .WithMany()
                .HasForeignKey(er => er.ToCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            // EscalationRule
            modelBuilder.Entity<EscalationRule>()
                .HasOne(er => er.EscalateToUser)
                .WithMany()
                .HasForeignKey(er => er.EscalateToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Project
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Manager)
                .WithMany()
                .HasForeignKey(p => p.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Asset
            modelBuilder.Entity<Asset>()
                .HasOne(a => a.AssignedToUser)
                .WithMany()
                .HasForeignKey(a => a.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Notification
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // DraftRequest
            modelBuilder.Entity<DraftRequest>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DraftRequest>()
                .HasOne(d => d.FormTemplate)
                .WithMany()
                .HasForeignKey(d => d.FormTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            // Team
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Leader)
                .WithMany()
                .HasForeignKey(t => t.LeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            // User
            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Branch)
                .WithMany()
                .HasForeignKey(u => u.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.JobTitle)
                .WithMany()
                .HasForeignKey(u => u.JobTitleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Position)
                .WithMany()
                .HasForeignKey(u => u.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Tenant)
                .WithMany()
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // LeaveBalance
            modelBuilder.Entity<LeaveBalance>()
                .HasOne(lb => lb.User)
                .WithMany()
                .HasForeignKey(lb => lb.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserShift
            modelBuilder.Entity<UserShift>()
                .HasOne(us => us.User)
                .WithMany()
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Timesheet
            modelBuilder.Entity<Timesheet>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ShiftSwapRequest
            modelBuilder.Entity<ShiftSwapRequest>()
                .HasOne(s => s.Requester)
                .WithMany()
                .HasForeignKey(s => s.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShiftSwapRequest>()
                .HasOne(s => s.TargetUser)
                .WithMany()
                .HasForeignKey(s => s.TargetUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // AttendanceLocationConfig
            modelBuilder.Entity<AttendanceLocationConfig>()
                .HasOne(a => a.Branch)
                .WithMany()
                .HasForeignKey(a => a.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes for performance
            modelBuilder.Entity<User>().HasIndex(u => u.Email);
            modelBuilder.Entity<User>().HasIndex(u => u.TenantId);
            modelBuilder.Entity<Request>().HasIndex(r => r.TenantId);
            modelBuilder.Entity<Request>().HasIndex(r => r.Status);
            modelBuilder.Entity<Request>().HasIndex(r => r.RequesterId);
            modelBuilder.Entity<Notification>().HasIndex(n => new { n.UserId, n.IsRead });
            modelBuilder.Entity<Timesheet>().HasIndex(t => new { t.UserId, t.Date });
            modelBuilder.Entity<LeaveBalance>().HasIndex(lb => new { lb.UserId, lb.Year });

            // Seed data
            const bool SeedDemoData = true;
            if (SeedDemoData)
                SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Tenant
            modelBuilder.Entity<Tenant>().HasData(new Tenant
            {
                Id = 1,
                CompanyName = "DANGCAPNE Corporation",
                SubDomain = "dangcapne",
                PrimaryColor = "#6366f1",
                SecondaryColor = "#8b5cf6",
                Plan = "Enterprise",
                MaxUsers = 500,
                IsActive = true
            });

            // Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, TenantId = 1, Name = "Admin", Description = "Quản trị viên hệ thống" },
                new Role { Id = 2, TenantId = 1, Name = "HR", Description = "Hành chính Nhân sự" },
                new Role { Id = 3, TenantId = 1, Name = "Manager", Description = "Quản lý" },
                new Role { Id = 4, TenantId = 1, Name = "Employee", Description = "Nhân viên" }
            );

            // Branches
            modelBuilder.Entity<Branch>().HasData(
                new Branch { Id = 1, TenantId = 1, Name = "Trụ sở chính - TP.HCM", Address = "123 Nguyễn Huệ, Quận 1, TP.HCM", Latitude = 10.7769, Longitude = 106.7009, TimeZone = "SE Asia Standard Time" },
                new Branch { Id = 2, TenantId = 1, Name = "Chi nhánh Hà Nội", Address = "456 Hoàn Kiếm, Hà Nội", Latitude = 21.0285, Longitude = 105.8542, TimeZone = "SE Asia Standard Time" }
            );

            // Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, TenantId = 1, Name = "Ban Giám đốc", Code = "BOD" },
                new Department { Id = 2, TenantId = 1, Name = "Phòng Công nghệ Thông tin", Code = "IT" },
                new Department { Id = 3, TenantId = 1, Name = "Phòng Nhân sự", Code = "HR" },
                new Department { Id = 4, TenantId = 1, Name = "Phòng Kế toán", Code = "ACC" },
                new Department { Id = 5, TenantId = 1, Name = "Phòng Kinh doanh", Code = "SALES" },
                new Department { Id = 6, TenantId = 1, Name = "Phòng Marketing", Code = "MKT" }
            );

            // JobTitles
            modelBuilder.Entity<JobTitle>().HasData(
                new JobTitle { Id = 1, TenantId = 1, Name = "Giám đốc", Level = 5 },
                new JobTitle { Id = 2, TenantId = 1, Name = "Phó Giám đốc", Level = 4 },
                new JobTitle { Id = 3, TenantId = 1, Name = "Trưởng phòng", Level = 3 },
                new JobTitle { Id = 4, TenantId = 1, Name = "Phó phòng", Level = 3 },
                new JobTitle { Id = 5, TenantId = 1, Name = "Chuyên viên", Level = 2 },
                new JobTitle { Id = 6, TenantId = 1, Name = "Nhân viên", Level = 1 },
                new JobTitle { Id = 7, TenantId = 1, Name = "Thực tập sinh", Level = 0 }
            );

            // Positions
            modelBuilder.Entity<Position>().HasData(
                new Position { Id = 1, TenantId = 1, Name = "Giám đốc điều hành", DepartmentId = 1 },
                new Position { Id = 2, TenantId = 1, Name = "Trưởng phòng IT", DepartmentId = 2 },
                new Position { Id = 3, TenantId = 1, Name = "Trưởng phòng HR", DepartmentId = 3 },
                new Position { Id = 4, TenantId = 1, Name = "Kế toán trưởng", DepartmentId = 4 },
                new Position { Id = 5, TenantId = 1, Name = "Trưởng phòng KD", DepartmentId = 5 }
            );

            // Users - Use simple hash for demo
            var pwHash = BCryptSimple("Admin@123");
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, TenantId = 1, FullName = "Nguyễn Văn Admin", Email = "admin@company.com", PasswordHash = pwHash, EmployeeCode = "NV001", DepartmentId = 1, BranchId = 1, JobTitleId = 1, PositionId = 1, Phone = "0901234567" },
                new User { Id = 2, TenantId = 1, FullName = "Trần Thị HR", Email = "hr@company.com", PasswordHash = pwHash, EmployeeCode = "NV002", DepartmentId = 3, BranchId = 1, JobTitleId = 3, PositionId = 3, Phone = "0901234568" },
                new User { Id = 3, TenantId = 1, FullName = "Lê Văn Manager", Email = "manager@company.com", PasswordHash = pwHash, EmployeeCode = "NV003", DepartmentId = 2, BranchId = 1, JobTitleId = 3, PositionId = 2, Phone = "0901234569" },
                new User { Id = 4, TenantId = 1, FullName = "Phạm Thị Employee", Email = "employee@company.com", PasswordHash = pwHash, EmployeeCode = "NV004", DepartmentId = 2, BranchId = 1, JobTitleId = 6, Phone = "0901234570" },
                new User { Id = 5, TenantId = 1, FullName = "Hoàng Văn Dev", Email = "dev@company.com", PasswordHash = pwHash, EmployeeCode = "NV005", DepartmentId = 2, BranchId = 1, JobTitleId = 5, Phone = "0901234571" },
                new User { Id = 6, TenantId = 1, FullName = "Vũ Thị Kế Toán", Email = "accountant@company.com", PasswordHash = pwHash, EmployeeCode = "NV006", DepartmentId = 4, BranchId = 1, JobTitleId = 3, PositionId = 4, Phone = "0901234572" },
                new User { Id = 7, TenantId = 1, FullName = "Đỗ Văn Sales", Email = "sales@company.com", PasswordHash = pwHash, EmployeeCode = "NV007", DepartmentId = 5, BranchId = 1, JobTitleId = 6, Phone = "0901234573" },
                new User { Id = 8, TenantId = 1, FullName = "Ngô Thị Marketing", Email = "marketing@company.com", PasswordHash = pwHash, EmployeeCode = "NV008", DepartmentId = 6, BranchId = 1, JobTitleId = 6, Phone = "0901234574" }
            );

            // UserRoles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, UserId = 1, RoleId = 1 }, // Admin
                new UserRole { Id = 2, UserId = 2, RoleId = 2 }, // HR
                new UserRole { Id = 3, UserId = 3, RoleId = 3 }, // Manager
                new UserRole { Id = 4, UserId = 4, RoleId = 4 }, // Employee
                new UserRole { Id = 5, UserId = 5, RoleId = 4 },
                new UserRole { Id = 6, UserId = 6, RoleId = 4 },
                new UserRole { Id = 7, UserId = 7, RoleId = 4 },
                new UserRole { Id = 8, UserId = 8, RoleId = 4 },
                new UserRole { Id = 9, UserId = 1, RoleId = 3 } // Admin also Manager
            );

            // UserManagers
            modelBuilder.Entity<UserManager>().HasData(
                new UserManager { Id = 1, UserId = 4, ManagerId = 3 }, // Employee -> Manager
                new UserManager { Id = 2, UserId = 5, ManagerId = 3 }, // Dev -> Manager
                new UserManager { Id = 3, UserId = 3, ManagerId = 1 }, // Manager -> Admin/Director
                new UserManager { Id = 4, UserId = 2, ManagerId = 1 }, // HR -> Admin/Director
                new UserManager { Id = 5, UserId = 7, ManagerId = 1 }, // Sales -> Director
                new UserManager { Id = 6, UserId = 8, ManagerId = 1 }  // Marketing -> Director
            );

            // Leave Types
            modelBuilder.Entity<LeaveType>().HasData(
                new LeaveType { Id = 1, TenantId = 1, Name = "Phép năm", Code = "AL", DefaultDaysPerYear = 12, AllowCarryOver = true, CarryOverMaxDays = 5, CarryOverExpiryMonth = 3, IsPaid = true, IconColor = "#10b981" },
                new LeaveType { Id = 2, TenantId = 1, Name = "Nghỉ ốm", Code = "SL", DefaultDaysPerYear = 30, IsPaid = true, IconColor = "#f59e0b" },
                new LeaveType { Id = 3, TenantId = 1, Name = "Nghỉ thai sản", Code = "ML", DefaultDaysPerYear = 180, IsPaid = true, IconColor = "#ec4899" },
                new LeaveType { Id = 4, TenantId = 1, Name = "Nghỉ không lương", Code = "UL", DefaultDaysPerYear = 365, IsPaid = false, AllowNegativeBalance = true, IconColor = "#6b7280" },
                new LeaveType { Id = 5, TenantId = 1, Name = "Nghỉ bù (Comp Off)", Code = "CO", DefaultDaysPerYear = 0, IsPaid = true, IconColor = "#3b82f6" }
            );

            // Shifts
            modelBuilder.Entity<Shift>().HasData(
                new Shift { Id = 1, TenantId = 1, Name = "Ca hành chính", Code = "HC", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(17, 0, 0), BreakStartTime = new TimeSpan(12, 0, 0), BreakEndTime = new TimeSpan(13, 0, 0), GracePeriodMinutes = 15 },
                new Shift { Id = 2, TenantId = 1, Name = "Ca sáng", Code = "S", StartTime = new TimeSpan(6, 0, 0), EndTime = new TimeSpan(14, 0, 0), GracePeriodMinutes = 10 },
                new Shift { Id = 3, TenantId = 1, Name = "Ca chiều", Code = "C", StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(22, 0, 0), GracePeriodMinutes = 10 },
                new Shift { Id = 4, TenantId = 1, Name = "Ca đêm", Code = "D", StartTime = new TimeSpan(22, 0, 0), EndTime = new TimeSpan(6, 0, 0), GracePeriodMinutes = 10 }
            );

            // OT Rates
            modelBuilder.Entity<OvertimeRate>().HasData(
                new OvertimeRate { Id = 1, TenantId = 1, Name = "Ngày thường", Multiplier = 1.5, Description = "OT ngày thường x1.5" },
                new OvertimeRate { Id = 2, TenantId = 1, Name = "Cuối tuần", Multiplier = 2.0, Description = "OT cuối tuần x2.0" },
                new OvertimeRate { Id = 3, TenantId = 1, Name = "Ngày lễ", Multiplier = 3.0, Description = "OT ngày lễ x3.0" }
            );

            // Holidays 2026
            modelBuilder.Entity<Holiday>().HasData(
                new Holiday { Id = 1, TenantId = 1, Name = "Tết Dương lịch", Date = new DateTime(2026, 1, 1), Country = "VN" },
                new Holiday { Id = 2, TenantId = 1, Name = "Giỗ Tổ Hùng Vương", Date = new DateTime(2026, 4, 26), Country = "VN" },
                new Holiday { Id = 3, TenantId = 1, Name = "Ngày Thống nhất", Date = new DateTime(2026, 4, 30), Country = "VN" },
                new Holiday { Id = 4, TenantId = 1, Name = "Quốc tế Lao động", Date = new DateTime(2026, 5, 1), Country = "VN" },
                new Holiday { Id = 5, TenantId = 1, Name = "Quốc khánh", Date = new DateTime(2026, 9, 2), Country = "VN" }
            );

            // Currencies
            modelBuilder.Entity<Currency>().HasData(
                new Currency { Id = 1, Code = "VND", Name = "Việt Nam Đồng", Symbol = "₫", IsDefault = true },
                new Currency { Id = 2, Code = "USD", Name = "US Dollar", Symbol = "$" },
                new Currency { Id = 3, Code = "EUR", Name = "Euro", Symbol = "€" },
                new Currency { Id = 4, Code = "JPY", Name = "Japanese Yen", Symbol = "¥" }
            );

            // Expense Categories
            modelBuilder.Entity<ExpenseCategory>().HasData(
                new ExpenseCategory { Id = 1, TenantId = 1, Name = "Tiền taxi/xe", Code = "TAXI", RequiresReceipt = true },
                new ExpenseCategory { Id = 2, TenantId = 1, Name = "Tiền khách sạn", Code = "HOTEL", RequiresReceipt = true },
                new ExpenseCategory { Id = 3, TenantId = 1, Name = "Tiền ăn uống", Code = "MEAL", RequiresReceipt = false, MaxAmount = 500000 },
                new ExpenseCategory { Id = 4, TenantId = 1, Name = "Chi phí khác", Code = "OTHER", RequiresReceipt = true }
            );

            // Asset Categories
            modelBuilder.Entity<AssetCategory>().HasData(
                new AssetCategory { Id = 1, TenantId = 1, Name = "Laptop", Description = "Máy tính xách tay" },
                new AssetCategory { Id = 2, TenantId = 1, Name = "Màn hình", Description = "Màn hình máy tính" },
                new AssetCategory { Id = 3, TenantId = 1, Name = "Điện thoại", Description = "Điện thoại công ty" },
                new AssetCategory { Id = 4, TenantId = 1, Name = "Bàn ghế", Description = "Bàn ghế văn phòng" }
            );

            // Projects
            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, TenantId = 1, Name = "Dự án ERP", Code = "PRJ-001", ManagerId = 3, StartDate = new DateTime(2026, 1, 1), Status = "Active", Budget = 500000000 },
                new Project { Id = 2, TenantId = 1, Name = "Dự án Website", Code = "PRJ-002", ManagerId = 3, StartDate = new DateTime(2026, 2, 1), Status = "Active", Budget = 200000000 }
            );

            // Workflow Definitions
            modelBuilder.Entity<WorkflowDef>().HasData(
                new WorkflowDef { Id = 1, TenantId = 1, Name = "Luồng duyệt cơ bản", Description = "Quản lý trực tiếp -> HR" },
                new WorkflowDef { Id = 2, TenantId = 1, Name = "Luồng duyệt tài chính", Description = "Quản lý -> Kế toán -> Giám đốc" },
                new WorkflowDef { Id = 3, TenantId = 1, Name = "Luồng duyệt vượt cấp", Description = "Quản lý -> Trưởng phòng -> HR -> Giám đốc" }
            );

            // Workflow Steps
            modelBuilder.Entity<WorkflowStep>().HasData(
                // Basic flow
                new WorkflowStep { Id = 1, WorkflowId = 1, Name = "Quản lý trực tiếp duyệt", StepOrder = 1, ApproverType = "DirectManager" },
                new WorkflowStep { Id = 2, WorkflowId = 1, Name = "HR duyệt", StepOrder = 2, ApproverType = "Role", ApproverRoleId = 2 },
                // Finance flow
                new WorkflowStep { Id = 3, WorkflowId = 2, Name = "Quản lý trực tiếp duyệt", StepOrder = 1, ApproverType = "DirectManager" },
                new WorkflowStep { Id = 4, WorkflowId = 2, Name = "Kế toán trưởng duyệt", StepOrder = 2, ApproverType = "SpecificUser", ApproverUserId = 6 },
                new WorkflowStep { Id = 5, WorkflowId = 2, Name = "Giám đốc duyệt", StepOrder = 3, ApproverType = "SpecificUser", ApproverUserId = 1 },
                // Escalation flow
                new WorkflowStep { Id = 6, WorkflowId = 3, Name = "Quản lý trực tiếp duyệt", StepOrder = 1, ApproverType = "DirectManager", CanSkipIfApplicant = true },
                new WorkflowStep { Id = 7, WorkflowId = 3, Name = "Trưởng phòng duyệt", StepOrder = 2, ApproverType = "Role", ApproverRoleId = 3, CanSkipIfApplicant = true },
                new WorkflowStep { Id = 8, WorkflowId = 3, Name = "HR duyệt", StepOrder = 3, ApproverType = "Role", ApproverRoleId = 2 },
                new WorkflowStep { Id = 9, WorkflowId = 3, Name = "Giám đốc duyệt", StepOrder = 4, ApproverType = "SpecificUser", ApproverUserId = 1 }
            );

            // Form Templates
            modelBuilder.Entity<FormTemplate>().HasData(
                new FormTemplate { Id = 1, TenantId = 1, Name = "Đơn xin nghỉ phép", Category = "Leave", Icon = "bi-calendar-x", IconColor = "#10b981", WorkflowId = 1 },
                new FormTemplate { Id = 2, TenantId = 1, Name = "Đơn làm thêm giờ (OT)", Category = "OT", Icon = "bi-clock-history", IconColor = "#f59e0b", WorkflowId = 1 },
                new FormTemplate { Id = 3, TenantId = 1, Name = "Đơn đi công tác", Category = "Travel", Icon = "bi-airplane", IconColor = "#3b82f6", WorkflowId = 1 },
                new FormTemplate { Id = 4, TenantId = 1, Name = "Đơn tạm ứng chi phí", Category = "Expense", Icon = "bi-cash-stack", IconColor = "#ef4444", WorkflowId = 2, RequiresFinancialApproval = true },
                new FormTemplate { Id = 5, TenantId = 1, Name = "Đơn yêu cầu cấp phát thiết bị", Category = "Equipment", Icon = "bi-laptop", IconColor = "#8b5cf6", WorkflowId = 1 },
                new FormTemplate { Id = 6, TenantId = 1, Name = "Đơn xin nghỉ việc", Category = "Leave", Icon = "bi-box-arrow-right", IconColor = "#dc2626", WorkflowId = 3 }
            );

            // Form Fields for Leave Request (Template 1)
            modelBuilder.Entity<FormField>().HasData(
                new FormField { Id = 1, FormTemplateId = 1, Label = "Loại nghỉ phép", FieldName = "leave_type", FieldType = "Dropdown", IsRequired = true, DisplayOrder = 1, Width = 6 },
                new FormField { Id = 2, FormTemplateId = 1, Label = "Từ ngày", FieldName = "start_date", FieldType = "Date", IsRequired = true, DisplayOrder = 2, Width = 6 },
                new FormField { Id = 3, FormTemplateId = 1, Label = "Đến ngày", FieldName = "end_date", FieldType = "Date", IsRequired = true, DisplayOrder = 3, Width = 6 },
                new FormField { Id = 4, FormTemplateId = 1, Label = "Số ngày nghỉ", FieldName = "total_days", FieldType = "Number", IsRequired = true, DisplayOrder = 4, Width = 6 },
                new FormField { Id = 5, FormTemplateId = 1, Label = "Lý do", FieldName = "reason", FieldType = "Textarea", IsRequired = true, DisplayOrder = 5, Width = 12 },
                new FormField { Id = 6, FormTemplateId = 1, Label = "File đính kèm", FieldName = "attachment", FieldType = "FileUpload", IsRequired = false, DisplayOrder = 6, Width = 12 },
                // OT Form (Template 2)
                new FormField { Id = 7, FormTemplateId = 2, Label = "Ngày làm thêm", FieldName = "ot_date", FieldType = "Date", IsRequired = true, DisplayOrder = 1, Width = 6 },
                new FormField { Id = 8, FormTemplateId = 2, Label = "Giờ bắt đầu", FieldName = "start_time", FieldType = "Text", IsRequired = true, DisplayOrder = 2, Width = 6, Placeholder = "HH:mm" },
                new FormField { Id = 9, FormTemplateId = 2, Label = "Giờ kết thúc", FieldName = "end_time", FieldType = "Text", IsRequired = true, DisplayOrder = 3, Width = 6, Placeholder = "HH:mm" },
                new FormField { Id = 10, FormTemplateId = 2, Label = "Dự án", FieldName = "project", FieldType = "Dropdown", IsRequired = true, DisplayOrder = 4, Width = 6 },
                new FormField { Id = 11, FormTemplateId = 2, Label = "Lý do làm thêm", FieldName = "reason", FieldType = "Textarea", IsRequired = true, DisplayOrder = 5, Width = 12 },
                // Travel Form (Template 3)
                new FormField { Id = 12, FormTemplateId = 3, Label = "Điểm đến", FieldName = "destination", FieldType = "Text", IsRequired = true, DisplayOrder = 1, Width = 6 },
                new FormField { Id = 13, FormTemplateId = 3, Label = "Từ ngày", FieldName = "start_date", FieldType = "Date", IsRequired = true, DisplayOrder = 2, Width = 6 },
                new FormField { Id = 14, FormTemplateId = 3, Label = "Đến ngày", FieldName = "end_date", FieldType = "Date", IsRequired = true, DisplayOrder = 3, Width = 6 },
                new FormField { Id = 15, FormTemplateId = 3, Label = "Mục đích", FieldName = "purpose", FieldType = "Textarea", IsRequired = true, DisplayOrder = 4, Width = 12 },
                // Expense Form (Template 4)
                new FormField { Id = 16, FormTemplateId = 4, Label = "Số tiền tạm ứng", FieldName = "amount", FieldType = "Number", IsRequired = true, DisplayOrder = 1, Width = 6 },
                new FormField { Id = 17, FormTemplateId = 4, Label = "Loại tiền", FieldName = "currency", FieldType = "Dropdown", IsRequired = true, DisplayOrder = 2, Width = 6 },
                new FormField { Id = 18, FormTemplateId = 4, Label = "Mục đích", FieldName = "purpose", FieldType = "Textarea", IsRequired = true, DisplayOrder = 3, Width = 12 },
                new FormField { Id = 19, FormTemplateId = 4, Label = "Hóa đơn đính kèm", FieldName = "receipt", FieldType = "FileUpload", IsRequired = false, DisplayOrder = 4, Width = 12 },
                // Equipment Form (Template 5)
                new FormField { Id = 20, FormTemplateId = 5, Label = "Loại thiết bị", FieldName = "equipment_type", FieldType = "Dropdown", IsRequired = true, DisplayOrder = 1, Width = 6 },
                new FormField { Id = 21, FormTemplateId = 5, Label = "Lý do cần cấp phát", FieldName = "reason", FieldType = "Textarea", IsRequired = true, DisplayOrder = 2, Width = 12 }
            );

            // Form Field Options
            modelBuilder.Entity<FormFieldOption>().HasData(
                // Leave types
                new FormFieldOption { Id = 1, FormFieldId = 1, Label = "Phép năm", Value = "AL", DisplayOrder = 1 },
                new FormFieldOption { Id = 2, FormFieldId = 1, Label = "Nghỉ ốm", Value = "SL", DisplayOrder = 2 },
                new FormFieldOption { Id = 3, FormFieldId = 1, Label = "Nghỉ thai sản", Value = "ML", DisplayOrder = 3 },
                new FormFieldOption { Id = 4, FormFieldId = 1, Label = "Nghỉ không lương", Value = "UL", DisplayOrder = 4 },
                new FormFieldOption { Id = 5, FormFieldId = 1, Label = "Nghỉ bù", Value = "CO", DisplayOrder = 5 },
                // Projects
                new FormFieldOption { Id = 6, FormFieldId = 10, Label = "Dự án ERP", Value = "PRJ-001", DisplayOrder = 1 },
                new FormFieldOption { Id = 7, FormFieldId = 10, Label = "Dự án Website", Value = "PRJ-002", DisplayOrder = 2 },
                // Currencies
                new FormFieldOption { Id = 8, FormFieldId = 17, Label = "VNĐ", Value = "VND", DisplayOrder = 1 },
                new FormFieldOption { Id = 9, FormFieldId = 17, Label = "USD", Value = "USD", DisplayOrder = 2 },
                // Equipment types
                new FormFieldOption { Id = 10, FormFieldId = 20, Label = "Laptop", Value = "LAPTOP", DisplayOrder = 1 },
                new FormFieldOption { Id = 11, FormFieldId = 20, Label = "Màn hình", Value = "MONITOR", DisplayOrder = 2 },
                new FormFieldOption { Id = 12, FormFieldId = 20, Label = "Điện thoại", Value = "PHONE", DisplayOrder = 3 },
                new FormFieldOption { Id = 13, FormFieldId = 20, Label = "Khác", Value = "OTHER", DisplayOrder = 4 }
            );

            // SLA Configs
            modelBuilder.Entity<SlaConfig>().HasData(
                new SlaConfig { Id = 1, TenantId = 1, FormTemplateId = 1, ReminderHours = 24, EscalationHours = 48 },
                new SlaConfig { Id = 2, TenantId = 1, FormTemplateId = 4, ReminderHours = 12, EscalationHours = 24 }
            );

            // Leave Balances for 2026
            modelBuilder.Entity<LeaveBalance>().HasData(
                new LeaveBalance { Id = 1, TenantId = 1, UserId = 4, LeaveTypeId = 1, Year = 2026, TotalEntitled = 12, Used = 3 },
                new LeaveBalance { Id = 2, TenantId = 1, UserId = 4, LeaveTypeId = 2, Year = 2026, TotalEntitled = 30, Used = 1 },
                new LeaveBalance { Id = 3, TenantId = 1, UserId = 5, LeaveTypeId = 1, Year = 2026, TotalEntitled = 12, Used = 2 },
                new LeaveBalance { Id = 4, TenantId = 1, UserId = 5, LeaveTypeId = 2, Year = 2026, TotalEntitled = 30, Used = 0 },
                new LeaveBalance { Id = 5, TenantId = 1, UserId = 7, LeaveTypeId = 1, Year = 2026, TotalEntitled = 12, Used = 5 },
                new LeaveBalance { Id = 6, TenantId = 1, UserId = 8, LeaveTypeId = 1, Year = 2026, TotalEntitled = 12, Used = 1 }
            );

            // Email Templates
            modelBuilder.Entity<EmailTemplate>().HasData(
                new EmailTemplate { Id = 1, TenantId = 1, Name = "NewRequest", Subject = "[{{CompanyName}}] Đơn mới cần duyệt: {{RequestCode}}", BodyHtml = "<h2>Xin chào {{ApproverName}},</h2><p>Bạn có một đơn mới cần xử lý từ <strong>{{RequesterName}}</strong>.</p><p>Loại đơn: {{FormName}}</p><p>Mã đơn: {{RequestCode}}</p><a href='{{ActionUrl}}'>Xem chi tiết</a>" },
                new EmailTemplate { Id = 2, TenantId = 1, Name = "Approved", Subject = "[{{CompanyName}}] Đơn {{RequestCode}} đã được duyệt", BodyHtml = "<h2>Xin chào {{RequesterName}},</h2><p>Đơn <strong>{{RequestCode}}</strong> của bạn đã được <strong>phê duyệt</strong> bởi {{ApproverName}}.</p>" },
                new EmailTemplate { Id = 3, TenantId = 1, Name = "Rejected", Subject = "[{{CompanyName}}] Đơn {{RequestCode}} bị từ chối", BodyHtml = "<h2>Xin chào {{RequesterName}},</h2><p>Đơn <strong>{{RequestCode}}</strong> của bạn đã bị <strong>từ chối</strong> bởi {{ApproverName}}.</p><p>Lý do: {{Comments}}</p>" },
                new EmailTemplate { Id = 4, TenantId = 1, Name = "Reminder", Subject = "[{{CompanyName}}] Nhắc nhở: Đơn {{RequestCode}} chưa được xử lý", BodyHtml = "<h2>Xin chào {{ApproverName}},</h2><p>Đơn <strong>{{RequestCode}}</strong> đã chờ duyệt hơn {{Hours}} giờ. Vui lòng xử lý sớm.</p>" }
            );

            // Sample Notifications
            modelBuilder.Entity<Notification>().HasData(
                new Notification { Id = 1, TenantId = 1, UserId = 3, Title = "Đơn mới cần duyệt", Message = "Phạm Thị Employee đã tạo đơn xin nghỉ phép", Type = "Approval", ActionUrl = "/Approvals" },
                new Notification { Id = 2, TenantId = 1, UserId = 4, Title = "Chào mừng!", Message = "Chào mừng bạn đến với hệ thống quản lý đơn từ DANGCAPNE", Type = "Info" },
                new Notification { Id = 3, TenantId = 1, UserId = 2, Title = "Báo cáo tuần", Message = "Có 5 đơn mới cần HR xử lý trong tuần này", Type = "Info", ActionUrl = "/HR" }
            );

            // Sample Requests
            modelBuilder.Entity<Request>().HasData(
                new Request { Id = 1, TenantId = 1, RequestCode = "REQ-20260305-001", FormTemplateId = 1, RequesterId = 4, Title = "Xin nghỉ phép năm 3 ngày", Status = "Pending", CurrentStepOrder = 1, Priority = "Normal", CreatedAt = new DateTime(2026, 3, 5) },
                new Request { Id = 2, TenantId = 1, RequestCode = "REQ-20260307-001", FormTemplateId = 2, RequesterId = 5, Title = "Làm thêm giờ dự án ERP", Status = "Approved", CurrentStepOrder = 2, Priority = "Normal", CreatedAt = new DateTime(2026, 3, 7), CompletedAt = new DateTime(2026, 3, 8) },
                new Request { Id = 3, TenantId = 1, RequestCode = "REQ-20260310-001", FormTemplateId = 4, RequesterId = 7, Title = "Tạm ứng đi công tác Đà Nẵng", Status = "InProgress", CurrentStepOrder = 2, Priority = "High", CreatedAt = new DateTime(2026, 3, 10) },
                new Request { Id = 4, TenantId = 1, RequestCode = "REQ-20260311-001", FormTemplateId = 1, RequesterId = 8, Title = "Xin nghỉ phép 1 ngày", Status = "Rejected", CurrentStepOrder = 1, Priority = "Normal", CreatedAt = new DateTime(2026, 3, 11), CompletedAt = new DateTime(2026, 3, 11) }
            );

            // Request Data
            modelBuilder.Entity<RequestData>().HasData(
                new RequestData { Id = 1, RequestId = 1, FieldName = "leave_type", FieldValue = "AL" },
                new RequestData { Id = 2, RequestId = 1, FieldName = "start_date", FieldValue = "2026-03-15" },
                new RequestData { Id = 3, RequestId = 1, FieldName = "end_date", FieldValue = "2026-03-17" },
                new RequestData { Id = 4, RequestId = 1, FieldName = "total_days", FieldValue = "3" },
                new RequestData { Id = 5, RequestId = 1, FieldName = "reason", FieldValue = "Nghỉ phép cá nhân để đi du lịch" },
                new RequestData { Id = 6, RequestId = 2, FieldName = "ot_date", FieldValue = "2026-03-08" },
                new RequestData { Id = 7, RequestId = 2, FieldName = "start_time", FieldValue = "18:00" },
                new RequestData { Id = 8, RequestId = 2, FieldName = "end_time", FieldValue = "21:00" },
                new RequestData { Id = 9, RequestId = 2, FieldName = "project", FieldValue = "PRJ-001" },
                new RequestData { Id = 10, RequestId = 2, FieldName = "reason", FieldValue = "Deploy module thanh toán" },
                new RequestData { Id = 11, RequestId = 3, FieldName = "amount", FieldValue = "15000000" },
                new RequestData { Id = 12, RequestId = 3, FieldName = "currency", FieldValue = "VND" },
                new RequestData { Id = 13, RequestId = 3, FieldName = "purpose", FieldValue = "Công tác gặp khách hàng tại Đà Nẵng" }
            );

            // Request Approvals
            modelBuilder.Entity<RequestApproval>().HasData(
                new RequestApproval { Id = 1, RequestId = 1, StepOrder = 1, StepName = "Quản lý trực tiếp duyệt", ApproverId = 3, Status = "Pending", CreatedAt = new DateTime(2026, 3, 5) },
                new RequestApproval { Id = 2, RequestId = 2, StepOrder = 1, StepName = "Quản lý trực tiếp duyệt", ApproverId = 3, Status = "Approved", ActionDate = new DateTime(2026, 3, 7), CreatedAt = new DateTime(2026, 3, 7) },
                new RequestApproval { Id = 3, RequestId = 2, StepOrder = 2, StepName = "HR duyệt", ApproverId = 2, Status = "Approved", ActionDate = new DateTime(2026, 3, 8), CreatedAt = new DateTime(2026, 3, 8) },
                new RequestApproval { Id = 4, RequestId = 3, StepOrder = 1, StepName = "Quản lý trực tiếp duyệt", ApproverId = 1, Status = "Approved", ActionDate = new DateTime(2026, 3, 10), CreatedAt = new DateTime(2026, 3, 10) },
                new RequestApproval { Id = 5, RequestId = 3, StepOrder = 2, StepName = "Kế toán trưởng duyệt", ApproverId = 6, Status = "Pending", CreatedAt = new DateTime(2026, 3, 10) },
                new RequestApproval { Id = 6, RequestId = 4, StepOrder = 1, StepName = "Quản lý trực tiếp duyệt", ApproverId = 1, Status = "Rejected", Comments = "Phòng đang có nhiều người nghỉ, vui lòng chọn ngày khác", ActionDate = new DateTime(2026, 3, 11), CreatedAt = new DateTime(2026, 3, 11) }
            );

            // Audit Logs
            modelBuilder.Entity<RequestAuditLog>().HasData(
                new RequestAuditLog { Id = 1, RequestId = 1, UserId = 4, Action = "Created", NewStatus = "Pending", Details = "Tạo đơn xin nghỉ phép", CreatedAt = new DateTime(2026, 3, 5) },
                new RequestAuditLog { Id = 2, RequestId = 2, UserId = 5, Action = "Created", NewStatus = "Pending", CreatedAt = new DateTime(2026, 3, 7) },
                new RequestAuditLog { Id = 3, RequestId = 2, UserId = 3, Action = "Approved", OldStatus = "Pending", NewStatus = "InProgress", CreatedAt = new DateTime(2026, 3, 7) },
                new RequestAuditLog { Id = 4, RequestId = 2, UserId = 2, Action = "Approved", OldStatus = "InProgress", NewStatus = "Approved", CreatedAt = new DateTime(2026, 3, 8) }
            );
            // Assets (sample for admin ops)
            modelBuilder.Entity<Asset>().HasData(
                new Asset
                {
                    Id = 1,
                    TenantId = 1,
                    AssetCode = "AST-001",
                    Name = "Laptop Dell",
                    CategoryId = 1,
                    Status = "Available",
                    PurchasePrice = 15000000,
                    PurchaseDate = new DateTime(2025, 1, 10)
                }
            );

            // RBAC Permissions
            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, TenantId = 1, Name = "Create Request", Code = "REQUEST_CREATE", Description = "Create request" },
                new Permission { Id = 2, TenantId = 1, Name = "Approve Request", Code = "REQUEST_APPROVE", Description = "Approve request" },
                new Permission { Id = 3, TenantId = 1, Name = "System Admin", Code = "SYSTEM_ADMIN", Description = "System administration" }
            );

            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission { Id = 1, RoleId = 4, PermissionId = 1 },
                new RolePermission { Id = 2, RoleId = 3, PermissionId = 2 },
                new RolePermission { Id = 3, RoleId = 1, PermissionId = 1 },
                new RolePermission { Id = 4, RoleId = 1, PermissionId = 2 },
                new RolePermission { Id = 5, RoleId = 1, PermissionId = 3 },
                new RolePermission { Id = 6, RoleId = 2, PermissionId = 2 }
            );

            modelBuilder.Entity<UserPermission>().HasData(
                new UserPermission { Id = 1, UserId = 2, PermissionId = 2, GrantedByUserId = 1, IsActive = true }
            );

            // Recruitment
            modelBuilder.Entity<JobRequisition>().HasData(
                new JobRequisition
                {
                    Id = 1,
                    TenantId = 1,
                    Title = "HR Specialist",
                    DepartmentId = 3,
                    JobTitleId = 5,
                    Headcount = 1,
                    BudgetMin = 8000000,
                    BudgetMax = 12000000,
                    Status = "Pending",
                    CreatedByUserId = 3,
                    CreatedAt = new DateTime(2026, 3, 1)
                }
            );

            modelBuilder.Entity<JobRequisitionApproval>().HasData(
                new JobRequisitionApproval
                {
                    Id = 1,
                    JobRequisitionId = 1,
                    ApproverId = 1,
                    Status = "Approved",
                    ActionDate = new DateTime(2026, 3, 2),
                    Comments = "Approved for hiring"
                }
            );

            modelBuilder.Entity<Candidate>().HasData(
                new Candidate { Id = 1, TenantId = 1, FullName = "Tran Thi Candidate", Email = "candidate@demo.com", Phone = "0909000001", Source = "Referral", CreatedAt = new DateTime(2026, 3, 3) }
            );

            modelBuilder.Entity<CandidateApplication>().HasData(
                new CandidateApplication { Id = 1, CandidateId = 1, JobRequisitionId = 1, AppliedAt = new DateTime(2026, 3, 3), Status = "Applied" }
            );

            modelBuilder.Entity<InterviewSchedule>().HasData(
                new InterviewSchedule { Id = 1, CandidateApplicationId = 1, InterviewerId = 3, ScheduledAt = new DateTime(2026, 3, 5, 9, 0, 0), Location = "Meeting Room 1", Status = "Scheduled" }
            );

            modelBuilder.Entity<OfferLetter>().HasData(
                new OfferLetter { Id = 1, CandidateApplicationId = 1, OfferedSalary = 10000000, StartDate = new DateTime(2026, 4, 1), Status = "Sent", SentAt = new DateTime(2026, 3, 6) }
            );

            // Onboarding & Offboarding
            modelBuilder.Entity<OnboardingTaskTemplate>().HasData(
                new OnboardingTaskTemplate { Id = 1, TenantId = 1, Name = "Laptop Setup", Description = "Prepare laptop and account", DefaultDueDays = 3, DefaultAssigneeRoleId = 2 }
            );
            modelBuilder.Entity<OnboardingTask>().HasData(
                new OnboardingTask { Id = 1, TemplateId = 1, UserId = 4, AssignedToUserId = 2, Status = "Open", DueDate = new DateTime(2026, 3, 12) }
            );
            modelBuilder.Entity<OffboardingTaskTemplate>().HasData(
                new OffboardingTaskTemplate { Id = 1, TenantId = 1, Name = "Return Assets", Description = "Collect laptop and badge", DefaultDueDays = 2, DefaultAssigneeRoleId = 2 }
            );
            modelBuilder.Entity<OffboardingTask>().HasData(
                new OffboardingTask { Id = 1, TemplateId = 1, UserId = 8, AssignedToUserId = 2, Status = "Open", DueDate = new DateTime(2026, 3, 15) }
            );

            // Performance
            modelBuilder.Entity<PerformanceCycle>().HasData(
                new PerformanceCycle { Id = 1, TenantId = 1, Name = "2026 H1 Review", StartDate = new DateTime(2026, 1, 1), EndDate = new DateTime(2026, 6, 30), Status = "Open", CreatedAt = new DateTime(2026, 1, 1) }
            );
            modelBuilder.Entity<PerformanceGoal>().HasData(
                new PerformanceGoal { Id = 1, CycleId = 1, UserId = 4, Title = "Deliver projects on time", Weight = 1.0m, Status = "Active" }
            );
            modelBuilder.Entity<PerformanceReview>().HasData(
                new PerformanceReview { Id = 1, CycleId = 1, UserId = 4, ReviewerId = 3, Status = "Draft" }
            );
            modelBuilder.Entity<PerformanceReviewItem>().HasData(
                new PerformanceReviewItem { Id = 1, ReviewId = 1, GoalId = 1, Score = 4.0m, Comment = "Good performance" }
            );

            // Compensation
            modelBuilder.Entity<SalaryAdjustmentRequest>().HasData(
                new SalaryAdjustmentRequest { Id = 1, TenantId = 1, UserId = 4, RequestedByUserId = 3, ProposedSalary = 12000000, EffectiveDate = new DateTime(2026, 4, 1), Reason = "High performance", Status = "Pending", CreatedAt = new DateTime(2026, 3, 10) }
            );
            modelBuilder.Entity<BonusRequest>().HasData(
                new BonusRequest { Id = 1, TenantId = 1, UserId = 5, RequestedByUserId = 3, Amount = 2000000, Type = "Spot", Reason = "Project delivery", Status = "Pending", CreatedAt = new DateTime(2026, 3, 10) }
            );

            // Training & Certification
            modelBuilder.Entity<TrainingCourse>().HasData(
                new TrainingCourse { Id = 1, TenantId = 1, Name = "Advanced Excel", Provider = "Internal", Cost = 500000, StartDate = new DateTime(2026, 3, 20), EndDate = new DateTime(2026, 3, 21), IsActive = true }
            );
            modelBuilder.Entity<TrainingEnrollment>().HasData(
                new TrainingEnrollment { Id = 1, CourseId = 1, UserId = 4, Status = "Enrolled", EnrolledAt = new DateTime(2026, 3, 11) }
            );
            modelBuilder.Entity<Certification>().HasData(
                new Certification { Id = 1, TenantId = 1, UserId = 4, Name = "Safety Basics", IssuedDate = new DateTime(2025, 6, 1), ExpiryDate = new DateTime(2027, 6, 1), Status = "Active" }
            );
            modelBuilder.Entity<CertificationRenewal>().HasData(
                new CertificationRenewal { Id = 1, CertificationId = 1, RequestedAt = new DateTime(2026, 3, 1), Status = "Pending", ApprovedByUserId = 2 }
            );

            // Admin Operations
            modelBuilder.Entity<AssetAssignment>().HasData(
                new AssetAssignment { Id = 1, AssetId = 1, UserId = 4, AssignedAt = new DateTime(2026, 3, 1), Status = "Assigned" }
            );
            modelBuilder.Entity<AssetIncident>().HasData(
                new AssetIncident { Id = 1, AssetId = 1, ReportedByUserId = 4, Type = "Damage", Description = "Screen cracked", ReportedAt = new DateTime(2026, 3, 9), Status = "Open" }
            );
            modelBuilder.Entity<CarBooking>().HasData(
                new CarBooking { Id = 1, TenantId = 1, UserId = 5, StartTime = new DateTime(2026, 3, 12, 8, 0, 0), EndTime = new DateTime(2026, 3, 12, 17, 0, 0), PickupLocation = "Office", Destination = "Client Site", Status = "Pending", DriverName = "Nguyen Driver" }
            );
            modelBuilder.Entity<MealRegistration>().HasData(
                new MealRegistration { Id = 1, TenantId = 1, UserId = 4, Date = new DateTime(2026, 3, 12), MealType = "Overtime", Notes = "Vegetarian" }
            );
            modelBuilder.Entity<UniformRequest>().HasData(
                new UniformRequest { Id = 1, TenantId = 1, UserId = 7, Size = "L", Quantity = 2, Status = "Pending", RequestedAt = new DateTime(2026, 3, 11) }
            );

            // Compliance
            modelBuilder.Entity<PolicyDocument>().HasData(
                new PolicyDocument { Id = 1, TenantId = 1, Title = "Employee Handbook", Version = "1.0", PublishedAt = new DateTime(2026, 3, 1), FileUrl = "/docs/handbook.pdf", IsActive = true }
            );
            modelBuilder.Entity<PolicyAcknowledgement>().HasData(
                new PolicyAcknowledgement { Id = 1, PolicyDocumentId = 1, UserId = 4, AcknowledgedAt = new DateTime(2026, 3, 2), Status = "Acknowledged" }
            );
        }

        private static string BCryptSimple(string password)
        {
            // Simple hash for seeding - in production, use proper BCrypt
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + "DANGCAPNE_SALT"));
            return Convert.ToBase64String(bytes);
        }
    }
}
