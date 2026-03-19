using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DANGCAPNE.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AssetCategories",
                columns: new[] { "Id", "Description", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "Máy tính xách tay", "Laptop", 1 },
                    { 2, "Màn hình máy tính", "Màn hình", 1 },
                    { 3, "Điện thoại công ty", "Điện thoại", 1 },
                    { 4, "Bàn ghế văn phòng", "Bàn ghế", 1 }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Address", "AllowedRadius", "IsActive", "Latitude", "Longitude", "Name", "TenantId", "TimeZone" },
                values: new object[,]
                {
                    { 1, "123 Nguyễn Huệ, Quận 1, TP.HCM", 200.0, true, 10.776899999999999, 106.7009, "Trụ sở chính - TP.HCM", 1, "SE Asia Standard Time" },
                    { 2, "456 Hoàn Kiếm, Hà Nội", 200.0, true, 21.028500000000001, 105.85420000000001, "Chi nhánh Hà Nội", 1, "SE Asia Standard Time" }
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "Phone", "Source", "TenantId" },
                values: new object[] { 1, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidate@demo.com", "Tran Thi Candidate", "0909000001", "Referral", 1 });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "IsDefault", "Name", "Symbol" },
                values: new object[,]
                {
                    { 1, "VND", true, "Việt Nam Đồng", "₫" },
                    { 2, "USD", false, "US Dollar", "$" },
                    { 3, "EUR", false, "Euro", "€" },
                    { 4, "JPY", false, "Japanese Yen", "¥" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "CreatedAt", "EnglishName", "IsActive", "ManagerId", "Name", "ParentDepartmentId", "TenantId" },
                values: new object[,]
                {
                    { 1, "BOD", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6427), null, true, null, "Ban Giám đốc", null, 1 },
                    { 2, "IT", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6431), null, true, null, "Phòng Công nghệ Thông tin", null, 1 },
                    { 3, "HR", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6432), null, true, null, "Phòng Nhân sự", null, 1 },
                    { 4, "ACC", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6433), null, true, null, "Phòng Kế toán", null, 1 },
                    { 5, "SALES", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6434), null, true, null, "Phòng Kinh doanh", null, 1 },
                    { 6, "MKT", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6435), null, true, null, "Phòng Marketing", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyHtml", "IsActive", "Name", "Subject", "TenantId" },
                values: new object[,]
                {
                    { 1, "<h2>Xin chào {{ApproverName}},</h2><p>Bạn có một đơn mới cần xử lý từ <strong>{{RequesterName}}</strong>.</p><p>Loại đơn: {{FormName}}</p><p>Mã đơn: {{RequestCode}}</p><a href='{{ActionUrl}}'>Xem chi tiết</a>", true, "NewRequest", "[{{CompanyName}}] Đơn mới cần duyệt: {{RequestCode}}", 1 },
                    { 2, "<h2>Xin chào {{RequesterName}},</h2><p>Đơn <strong>{{RequestCode}}</strong> của bạn đã được <strong>phê duyệt</strong> bởi {{ApproverName}}.</p>", true, "Approved", "[{{CompanyName}}] Đơn {{RequestCode}} đã được duyệt", 1 },
                    { 3, "<h2>Xin chào {{RequesterName}},</h2><p>Đơn <strong>{{RequestCode}}</strong> của bạn đã bị <strong>từ chối</strong> bởi {{ApproverName}}.</p><p>Lý do: {{Comments}}</p>", true, "Rejected", "[{{CompanyName}}] Đơn {{RequestCode}} bị từ chối", 1 },
                    { 4, "<h2>Xin chào {{ApproverName}},</h2><p>Đơn <strong>{{RequestCode}}</strong> đã chờ duyệt hơn {{Hours}} giờ. Vui lòng xử lý sớm.</p>", true, "Reminder", "[{{CompanyName}}] Nhắc nhở: Đơn {{RequestCode}} chưa được xử lý", 1 }
                });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Code", "IsActive", "MaxAmount", "Name", "RequiresReceipt", "TenantId" },
                values: new object[,]
                {
                    { 1, "TAXI", true, null, "Tiền taxi/xe", true, 1 },
                    { 2, "HOTEL", true, null, "Tiền khách sạn", true, 1 },
                    { 3, "MEAL", true, 500000m, "Tiền ăn uống", false, 1 },
                    { 4, "OTHER", true, null, "Chi phí khác", true, 1 }
                });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "Country", "Date", "IsRecurring", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "VN", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Tết Dương lịch", 1 },
                    { 2, "VN", new DateTime(2026, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Giỗ Tổ Hùng Vương", 1 },
                    { 3, "VN", new DateTime(2026, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Ngày Thống nhất", 1 },
                    { 4, "VN", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Quốc tế Lao động", 1 },
                    { 5, "VN", new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Quốc khánh", 1 }
                });

            migrationBuilder.InsertData(
                table: "JobTitles",
                columns: new[] { "Id", "IsActive", "Level", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, true, 5, "Giám đốc", 1 },
                    { 2, true, 4, "Phó Giám đốc", 1 },
                    { 3, true, 3, "Trưởng phòng", 1 },
                    { 4, true, 3, "Phó phòng", 1 },
                    { 5, true, 2, "Chuyên viên", 1 },
                    { 6, true, 1, "Nhân viên", 1 },
                    { 7, true, 0, "Thực tập sinh", 1 }
                });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "AllowCarryOver", "AllowNegativeBalance", "CarryOverExpiryMonth", "CarryOverMaxDays", "Code", "DefaultDaysPerYear", "IconColor", "IsActive", "IsPaid", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, true, false, 3, 5, "AL", 12.0, "#10b981", true, true, "Phép năm", 1 },
                    { 2, false, false, 3, 0, "SL", 30.0, "#f59e0b", true, true, "Nghỉ ốm", 1 },
                    { 3, false, false, 3, 0, "ML", 180.0, "#ec4899", true, true, "Nghỉ thai sản", 1 },
                    { 4, false, true, 3, 0, "UL", 365.0, "#6b7280", true, false, "Nghỉ không lương", 1 },
                    { 5, false, false, 3, 0, "CO", 0.0, "#3b82f6", true, true, "Nghỉ bù (Comp Off)", 1 }
                });

            migrationBuilder.InsertData(
                table: "OvertimeRates",
                columns: new[] { "Id", "Description", "IsActive", "Multiplier", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "OT ngày thường x1.5", true, 1.5, "Ngày thường", 1 },
                    { 2, "OT cuối tuần x2.0", true, 2.0, "Cuối tuần", 1 },
                    { 3, "OT ngày lễ x3.0", true, 3.0, "Ngày lễ", 1 }
                });

            migrationBuilder.InsertData(
                table: "PerformanceCycles",
                columns: new[] { "Id", "CreatedAt", "EndDate", "Name", "StartDate", "Status", "TenantId" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "2026 H1 Review", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open", 1 });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "IsActive", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "REQUEST_CREATE", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8667), "Create request", true, "Create Request", 1 },
                    { 2, "REQUEST_APPROVE", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8670), "Approve request", true, "Approve Request", 1 },
                    { 3, "SYSTEM_ADMIN", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8671), "System administration", true, "System Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "PolicyDocuments",
                columns: new[] { "Id", "FileUrl", "IsActive", "PublishedAt", "TenantId", "Title", "Version" },
                values: new object[] { 1, "/docs/handbook.pdf", true, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Employee Handbook", "1.0" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6364), "Quản trị viên hệ thống", "Admin", 1 },
                    { 2, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6368), "Hành chính Nhân sự", "HR", 1 },
                    { 3, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6369), "Quản lý", "Manager", 1 },
                    { 4, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6372), "Nhân viên", "Employee", 1 }
                });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "BreakEndTime", "BreakStartTime", "Code", "EndTime", "GracePeriodMinutes", "IsActive", "Name", "StartTime", "TenantId" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 13, 0, 0, 0), new TimeSpan(0, 12, 0, 0, 0), "HC", new TimeSpan(0, 17, 0, 0, 0), 15, true, "Ca hành chính", new TimeSpan(0, 8, 0, 0, 0), 1 },
                    { 2, null, null, "S", new TimeSpan(0, 14, 0, 0, 0), 10, true, "Ca sáng", new TimeSpan(0, 6, 0, 0, 0), 1 },
                    { 3, null, null, "C", new TimeSpan(0, 22, 0, 0, 0), 10, true, "Ca chiều", new TimeSpan(0, 14, 0, 0, 0), 1 },
                    { 4, null, null, "D", new TimeSpan(0, 6, 0, 0, 0), 10, true, "Ca đêm", new TimeSpan(0, 22, 0, 0, 0), 1 }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "CompanyName", "CreatedAt", "ExpiresAt", "IsActive", "LogoUrl", "MaxUsers", "Plan", "PrimaryColor", "SecondaryColor", "SubDomain" },
                values: new object[] { 1, "DANGCAPNE Corporation", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6158), null, true, "", 500, "Enterprise", "#6366f1", "#8b5cf6", "dangcapne" });

            migrationBuilder.InsertData(
                table: "TrainingCourses",
                columns: new[] { "Id", "Cost", "EndDate", "IsActive", "Name", "Provider", "StartDate", "TenantId" },
                values: new object[] { 1, 500000m, new DateTime(2026, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Advanced Excel", "Internal", new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Workflows",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8122), "Quản lý trực tiếp -> HR", true, "Luồng duyệt cơ bản", 1 },
                    { 2, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8128), "Quản lý -> Kế toán -> Giám đốc", true, "Luồng duyệt tài chính", 1 },
                    { 3, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8129), "Quản lý -> Trưởng phòng -> HR -> Giám đốc", true, "Luồng duyệt vượt cấp", 1 }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "AssetCode", "AssignedDate", "AssignedToUserId", "CategoryId", "Name", "PurchaseDate", "PurchasePrice", "SerialNumber", "Status", "TenantId" },
                values: new object[] { 1, "AST-001", null, null, 1, "Laptop Dell", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 15000000m, null, "Available", 1 });

            migrationBuilder.InsertData(
                table: "FormTemplates",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "EnglishName", "Icon", "IconColor", "IsActive", "Name", "RequiresFinancialApproval", "TenantId", "WorkflowId" },
                values: new object[,]
                {
                    { 1, "Leave", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8207), "", null, "bi-calendar-x", "#10b981", true, "Đơn xin nghỉ phép", false, 1, 1 },
                    { 2, "OT", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8213), "", null, "bi-clock-history", "#f59e0b", true, "Đơn làm thêm giờ (OT)", false, 1, 1 },
                    { 3, "Travel", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8215), "", null, "bi-airplane", "#3b82f6", true, "Đơn đi công tác", false, 1, 1 },
                    { 4, "Expense", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8216), "", null, "bi-cash-stack", "#ef4444", true, "Đơn tạm ứng chi phí", true, 1, 2 },
                    { 5, "Equipment", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8218), "", null, "bi-laptop", "#8b5cf6", true, "Đơn yêu cầu cấp phát thiết bị", false, 1, 1 },
                    { 6, "Leave", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8219), "", null, "bi-box-arrow-right", "#dc2626", true, "Đơn xin nghỉ việc", false, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "OffboardingTaskTemplates",
                columns: new[] { "Id", "DefaultAssigneeRoleId", "DefaultDueDays", "Description", "Name", "TenantId" },
                values: new object[] { 1, 2, 2, "Collect laptop and badge", "Return Assets", 1 });

            migrationBuilder.InsertData(
                table: "OnboardingTaskTemplates",
                columns: new[] { "Id", "DefaultAssigneeRoleId", "DefaultDueDays", "Description", "Name", "TenantId" },
                values: new object[] { 1, 2, 3, "Prepare laptop and account", "Laptop Setup", 1 });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "DepartmentId", "IsActive", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, 1, true, "Giám đốc điều hành", 1 },
                    { 2, 2, true, "Trưởng phòng IT", 1 },
                    { 3, 3, true, "Trưởng phòng HR", 1 },
                    { 4, 4, true, "Kế toán trưởng", 1 },
                    { 5, 5, true, "Trưởng phòng KD", 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "AssignedAt", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8691), 1, 4 },
                    { 2, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8697), 2, 3 },
                    { 3, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8698), 1, 1 },
                    { 4, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8699), 2, 1 },
                    { 5, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8699), 3, 1 },
                    { 6, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8700), 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "BranchId", "CreatedAt", "DepartmentId", "Email", "EmployeeCode", "FullName", "HireDate", "JobTitleId", "Locale", "PasswordHash", "Phone", "PinHash", "PositionId", "Status", "TenantId", "TerminationDate", "TimeZone", "TwoFactorEnabled", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, "", 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7642), 2, "employee@company.com", "NV004", "Phạm Thị Employee", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7642), 6, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234570", null, null, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7642) },
                    { 5, "", 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7644), 2, "dev@company.com", "NV005", "Hoàng Văn Dev", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7644), 5, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234571", null, null, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7645) },
                    { 7, "", 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7677), 5, "sales@company.com", "NV007", "Đỗ Văn Sales", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7661), 6, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234573", null, null, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7678) },
                    { 8, "", 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7680), 6, "marketing@company.com", "NV008", "Ngô Thị Marketing", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7680), 6, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234574", null, null, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7680) }
                });

            migrationBuilder.InsertData(
                table: "WorkflowSteps",
                columns: new[] { "Id", "ApproverRoleId", "ApproverType", "ApproverUserId", "CanSkipIfApplicant", "IsActive", "Name", "StepOrder", "WorkflowId" },
                values: new object[,]
                {
                    { 1, null, "DirectManager", null, false, true, "Quản lý trực tiếp duyệt", 1, 1 },
                    { 2, 2, "Role", null, false, true, "HR duyệt", 2, 1 },
                    { 3, null, "DirectManager", null, false, true, "Quản lý trực tiếp duyệt", 1, 2 },
                    { 4, null, "SpecificUser", 6, false, true, "Kế toán trưởng duyệt", 2, 2 },
                    { 5, null, "SpecificUser", 1, false, true, "Giám đốc duyệt", 3, 2 },
                    { 6, null, "DirectManager", null, true, true, "Quản lý trực tiếp duyệt", 1, 3 },
                    { 7, 3, "Role", null, true, true, "Trưởng phòng duyệt", 2, 3 },
                    { 8, 2, "Role", null, false, true, "HR duyệt", 3, 3 },
                    { 9, null, "SpecificUser", 1, false, true, "Giám đốc duyệt", 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "AssetAssignments",
                columns: new[] { "Id", "AssetId", "AssignedAt", "ReturnedAt", "Status", "UserId" },
                values: new object[] { 1, 1, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Assigned", 4 });

            migrationBuilder.InsertData(
                table: "AssetIncidents",
                columns: new[] { "Id", "AssetId", "Description", "ReportedAt", "ReportedByUserId", "Status", "Type" },
                values: new object[] { 1, 1, "Screen cracked", new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Open", "Damage" });

            migrationBuilder.InsertData(
                table: "CarBookings",
                columns: new[] { "Id", "Destination", "DriverName", "EndTime", "PickupLocation", "StartTime", "Status", "TenantId", "UserId" },
                values: new object[] { 1, "Client Site", "Nguyen Driver", new DateTime(2026, 3, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), "Office", new DateTime(2026, 3, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 1, 5 });

            migrationBuilder.InsertData(
                table: "Certifications",
                columns: new[] { "Id", "ExpiryDate", "IssuedDate", "Name", "Status", "TenantId", "UserId" },
                values: new object[] { 1, new DateTime(2027, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Safety Basics", "Active", 1, 4 });

            migrationBuilder.InsertData(
                table: "FormFields",
                columns: new[] { "Id", "DefaultValue", "DisplayOrder", "FieldName", "FieldType", "FormTemplateId", "IsRequired", "Label", "Placeholder", "ValidationRules", "Width" },
                values: new object[,]
                {
                    { 1, null, 1, "leave_type", "Dropdown", 1, true, "Loại nghỉ phép", null, null, 6 },
                    { 2, null, 2, "start_date", "Date", 1, true, "Từ ngày", null, null, 6 },
                    { 3, null, 3, "end_date", "Date", 1, true, "Đến ngày", null, null, 6 },
                    { 4, null, 4, "total_days", "Number", 1, true, "Số ngày nghỉ", null, null, 6 },
                    { 5, null, 5, "reason", "Textarea", 1, true, "Lý do", null, null, 12 },
                    { 6, null, 6, "attachment", "FileUpload", 1, false, "File đính kèm", null, null, 12 },
                    { 7, null, 1, "ot_date", "Date", 2, true, "Ngày làm thêm", null, null, 6 },
                    { 8, null, 2, "start_time", "Text", 2, true, "Giờ bắt đầu", "HH:mm", null, 6 },
                    { 9, null, 3, "end_time", "Text", 2, true, "Giờ kết thúc", "HH:mm", null, 6 },
                    { 10, null, 4, "project", "Dropdown", 2, true, "Dự án", null, null, 6 },
                    { 11, null, 5, "reason", "Textarea", 2, true, "Lý do làm thêm", null, null, 12 },
                    { 12, null, 1, "destination", "Text", 3, true, "Điểm đến", null, null, 6 },
                    { 13, null, 2, "start_date", "Date", 3, true, "Từ ngày", null, null, 6 },
                    { 14, null, 3, "end_date", "Date", 3, true, "Đến ngày", null, null, 6 },
                    { 15, null, 4, "purpose", "Textarea", 3, true, "Mục đích", null, null, 12 },
                    { 16, null, 1, "amount", "Number", 4, true, "Số tiền tạm ứng", null, null, 6 },
                    { 17, null, 2, "currency", "Dropdown", 4, true, "Loại tiền", null, null, 6 },
                    { 18, null, 3, "purpose", "Textarea", 4, true, "Mục đích", null, null, 12 },
                    { 19, null, 4, "receipt", "FileUpload", 4, false, "Hóa đơn đính kèm", null, null, 12 },
                    { 20, null, 1, "equipment_type", "Dropdown", 5, true, "Loại thiết bị", null, null, 6 },
                    { 21, null, 2, "reason", "Textarea", 5, true, "Lý do cần cấp phát", null, null, 12 }
                });

            migrationBuilder.InsertData(
                table: "LeaveBalances",
                columns: new[] { "Id", "CarriedOver", "CompensatoryDays", "LeaveTypeId", "SeniorityBonus", "TenantId", "TotalEntitled", "UpdatedAt", "Used", "UserId", "Year" },
                values: new object[,]
                {
                    { 1, 0.0, 0.0, 1, 0.0, 1, 12.0, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8384), 3.0, 4, 2026 },
                    { 2, 0.0, 0.0, 2, 0.0, 1, 30.0, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8389), 1.0, 4, 2026 },
                    { 3, 0.0, 0.0, 1, 0.0, 1, 12.0, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8391), 2.0, 5, 2026 },
                    { 4, 0.0, 0.0, 2, 0.0, 1, 30.0, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8392), 0.0, 5, 2026 },
                    { 5, 0.0, 0.0, 1, 0.0, 1, 12.0, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8393), 5.0, 7, 2026 },
                    { 6, 0.0, 0.0, 1, 0.0, 1, 12.0, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8395), 1.0, 8, 2026 }
                });

            migrationBuilder.InsertData(
                table: "MealRegistrations",
                columns: new[] { "Id", "Date", "MealType", "Notes", "TenantId", "UserId" },
                values: new object[] { 1, new DateTime(2026, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Overtime", "Vegetarian", 1, 4 });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "ActionUrl", "CreatedAt", "IsRead", "Message", "ReadAt", "RelatedRequestId", "TenantId", "Title", "Type", "UserId" },
                values: new object[] { 2, null, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8449), false, "Chào mừng bạn đến với hệ thống quản lý đơn từ DANGCAPNE", null, null, 1, "Chào mừng!", "Info", 4 });

            migrationBuilder.InsertData(
                table: "PerformanceGoals",
                columns: new[] { "Id", "CycleId", "Status", "Title", "UserId", "Weight" },
                values: new object[] { 1, 1, "Active", "Deliver projects on time", 4, 1.0m });

            migrationBuilder.InsertData(
                table: "PolicyAcknowledgements",
                columns: new[] { "Id", "AcknowledgedAt", "PolicyDocumentId", "Status", "UserId" },
                values: new object[] { 1, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Acknowledged", 4 });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "CompletedAt", "CreatedAt", "CurrentStepOrder", "FormTemplateId", "Priority", "RequestCode", "RequesterId", "Status", "TenantId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Normal", "REQ-20260305-001", 4, "Pending", 1, "Xin nghỉ phép năm 3 ngày", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8474) },
                    { 2, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Normal", "REQ-20260307-001", 5, "Approved", 1, "Làm thêm giờ dự án ERP", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8483) },
                    { 3, null, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, "High", "REQ-20260310-001", 7, "InProgress", 1, "Tạm ứng đi công tác Đà Nẵng", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8489) },
                    { 4, new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Normal", "REQ-20260311-001", 8, "Rejected", 1, "Xin nghỉ phép 1 ngày", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8491) }
                });

            migrationBuilder.InsertData(
                table: "SlaConfigs",
                columns: new[] { "Id", "AutoEscalate", "AutoRemind", "EscalationHours", "FormTemplateId", "ReminderHours", "TenantId" },
                values: new object[,]
                {
                    { 1, true, true, 48, 1, 24, 1 },
                    { 2, true, true, 24, 4, 12, 1 }
                });

            migrationBuilder.InsertData(
                table: "TrainingEnrollments",
                columns: new[] { "Id", "CompletedAt", "CourseId", "EnrolledAt", "Status", "UserId" },
                values: new object[] { 1, null, 1, new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enrolled", 4 });

            migrationBuilder.InsertData(
                table: "UniformRequests",
                columns: new[] { "Id", "Quantity", "RequestedAt", "Size", "Status", "TenantId", "UserId" },
                values: new object[] { 1, 2, new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "L", "Pending", 1, 7 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "AssignedAt", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 4, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7734), 4, 4 },
                    { 5, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7735), 4, 5 },
                    { 7, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7736), 4, 7 },
                    { 8, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7737), 4, 8 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "BranchId", "CreatedAt", "DepartmentId", "Email", "EmployeeCode", "FullName", "HireDate", "JobTitleId", "Locale", "PasswordHash", "Phone", "PinHash", "PositionId", "Status", "TenantId", "TerminationDate", "TimeZone", "TwoFactorEnabled", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "", 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7625), 1, "admin@company.com", "NV001", "Nguyễn Văn Admin", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7625), 1, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234567", null, 1, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7626) },
                    { 2, "", 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7635), 3, "hr@company.com", "NV002", "Trần Thị HR", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7635), 3, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234568", null, 3, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7636) },
                    { 3, "", 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7639), 2, "manager@company.com", "NV003", "Lê Văn Manager", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7639), 3, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234569", null, 2, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7639) },
                    { 6, "", 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7647), 4, "accountant@company.com", "NV006", "Vũ Thị Kế Toán", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7647), 3, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234572", null, 4, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7647) }
                });

            migrationBuilder.InsertData(
                table: "BonusRequests",
                columns: new[] { "Id", "Amount", "CreatedAt", "Reason", "RequestedByUserId", "Status", "TenantId", "Type", "UserId" },
                values: new object[] { 1, 2000000m, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Project delivery", 3, "Pending", 1, "Spot", 5 });

            migrationBuilder.InsertData(
                table: "CertificationRenewals",
                columns: new[] { "Id", "ApprovedAt", "ApprovedByUserId", "CertificationId", "RequestedAt", "Status" },
                values: new object[] { 1, null, 2, 1, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending" });

            migrationBuilder.InsertData(
                table: "FormFieldOptions",
                columns: new[] { "Id", "DisplayOrder", "FormFieldId", "Label", "Value" },
                values: new object[,]
                {
                    { 1, 1, 1, "Phép năm", "AL" },
                    { 2, 2, 1, "Nghỉ ốm", "SL" },
                    { 3, 3, 1, "Nghỉ thai sản", "ML" },
                    { 4, 4, 1, "Nghỉ không lương", "UL" },
                    { 5, 5, 1, "Nghỉ bù", "CO" },
                    { 6, 1, 10, "Dự án ERP", "PRJ-001" },
                    { 7, 2, 10, "Dự án Website", "PRJ-002" },
                    { 8, 1, 17, "VNĐ", "VND" },
                    { 9, 2, 17, "USD", "USD" },
                    { 10, 1, 20, "Laptop", "LAPTOP" },
                    { 11, 2, 20, "Màn hình", "MONITOR" },
                    { 12, 3, 20, "Điện thoại", "PHONE" },
                    { 13, 4, 20, "Khác", "OTHER" }
                });

            migrationBuilder.InsertData(
                table: "JobRequisitions",
                columns: new[] { "Id", "BudgetMax", "BudgetMin", "CreatedAt", "CreatedByUserId", "DepartmentId", "Headcount", "JobTitleId", "Status", "TenantId", "Title" },
                values: new object[] { 1, 12000000m, 8000000m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 1, 5, "Pending", 1, "HR Specialist" });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "ActionUrl", "CreatedAt", "IsRead", "Message", "ReadAt", "RelatedRequestId", "TenantId", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, "/Approvals", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8445), false, "Phạm Thị Employee đã tạo đơn xin nghỉ phép", null, null, 1, "Đơn mới cần duyệt", "Approval", 3 },
                    { 3, "/HR", new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8450), false, "Có 5 đơn mới cần HR xử lý trong tuần này", null, null, 1, "Báo cáo tuần", "Info", 2 }
                });

            migrationBuilder.InsertData(
                table: "OffboardingTasks",
                columns: new[] { "Id", "AssignedToUserId", "CompletedAt", "DueDate", "Status", "TemplateId", "UserId" },
                values: new object[] { 1, 2, null, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open", 1, 8 });

            migrationBuilder.InsertData(
                table: "OnboardingTasks",
                columns: new[] { "Id", "AssignedToUserId", "CompletedAt", "DueDate", "Status", "TemplateId", "UserId" },
                values: new object[] { 1, 2, null, new DateTime(2026, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open", 1, 4 });

            migrationBuilder.InsertData(
                table: "PerformanceReviews",
                columns: new[] { "Id", "CycleId", "ReviewerId", "Score", "Status", "SubmittedAt", "UserId" },
                values: new object[] { 1, 1, 3, null, "Draft", null, 4 });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Budget", "Code", "EndDate", "IsActive", "ManagerId", "Name", "OtCost", "StartDate", "Status", "TenantId" },
                values: new object[,]
                {
                    { 1, 500000000m, "PRJ-001", null, true, 3, "Dự án ERP", 0m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", 1 },
                    { 2, 200000000m, "PRJ-002", null, true, 3, "Dự án Website", 0m, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", 1 }
                });

            migrationBuilder.InsertData(
                table: "RequestApprovals",
                columns: new[] { "Id", "ActionDate", "ApproverId", "Comments", "CreatedAt", "IpAddress", "RequestId", "Status", "StepName", "StepOrder", "VerifiedByPin" },
                values: new object[,]
                {
                    { 1, null, 3, null, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Pending", "Quản lý trực tiếp duyệt", 1, false },
                    { 2, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, "Approved", "Quản lý trực tiếp duyệt", 1, false },
                    { 3, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, "Approved", "HR duyệt", 2, false },
                    { 4, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, "Approved", "Quản lý trực tiếp duyệt", 1, false },
                    { 5, null, 6, null, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, "Pending", "Kế toán trưởng duyệt", 2, false },
                    { 6, new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Phòng đang có nhiều người nghỉ, vui lòng chọn ngày khác", new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, "Rejected", "Quản lý trực tiếp duyệt", 1, false }
                });

            migrationBuilder.InsertData(
                table: "RequestAuditLogs",
                columns: new[] { "Id", "Action", "CreatedAt", "Details", "IpAddress", "NewStatus", "OldStatus", "RequestId", "UserAgent", "UserId" },
                values: new object[,]
                {
                    { 1, "Created", new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tạo đơn xin nghỉ phép", null, "Pending", null, 1, null, 4 },
                    { 2, "Created", new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Pending", null, 2, null, 5 },
                    { 3, "Approved", new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "InProgress", "Pending", 2, null, 3 },
                    { 4, "Approved", new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Approved", "InProgress", 2, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "RequestData",
                columns: new[] { "Id", "FieldName", "FieldValue", "RequestId" },
                values: new object[,]
                {
                    { 1, "leave_type", "AL", 1 },
                    { 2, "start_date", "2026-03-15", 1 },
                    { 3, "end_date", "2026-03-17", 1 },
                    { 4, "total_days", "3", 1 },
                    { 5, "reason", "Nghỉ phép cá nhân để đi du lịch", 1 },
                    { 6, "ot_date", "2026-03-08", 2 },
                    { 7, "start_time", "18:00", 2 },
                    { 8, "end_time", "21:00", 2 },
                    { 9, "project", "PRJ-001", 2 },
                    { 10, "reason", "Deploy module thanh toán", 2 },
                    { 11, "amount", "15000000", 3 },
                    { 12, "currency", "VND", 3 },
                    { 13, "purpose", "Công tác gặp khách hàng tại Đà Nẵng", 3 }
                });

            migrationBuilder.InsertData(
                table: "SalaryAdjustmentRequests",
                columns: new[] { "Id", "CreatedAt", "EffectiveDate", "ProposedSalary", "Reason", "RequestedByUserId", "Status", "TenantId", "UserId" },
                values: new object[] { 1, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12000000m, "High performance", 3, "Pending", 1, 4 });

            migrationBuilder.InsertData(
                table: "UserManagers",
                columns: new[] { "Id", "EndDate", "IsPrimary", "ManagerId", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, null, true, 3, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7767), 4 },
                    { 2, null, true, 3, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7771), 5 },
                    { 3, null, true, 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7772), 3 },
                    { 4, null, true, 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7773), 2 },
                    { 5, null, true, 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7774), 7 },
                    { 6, null, true, 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7775), 8 }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Id", "GrantedAt", "GrantedByUserId", "IsActive", "PermissionId", "UserId" },
                values: new object[] { 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8725), 1, true, 2, 2 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "AssignedAt", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7717), 1, 1 },
                    { 2, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7732), 2, 2 },
                    { 3, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7733), 3, 3 },
                    { 6, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7735), 4, 6 },
                    { 9, new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7738), 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "CandidateApplications",
                columns: new[] { "Id", "AppliedAt", "CandidateId", "JobRequisitionId", "Status" },
                values: new object[] { 1, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Applied" });

            migrationBuilder.InsertData(
                table: "JobRequisitionApprovals",
                columns: new[] { "Id", "ActionDate", "ApproverId", "Comments", "JobRequisitionId", "Status" },
                values: new object[] { 1, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Approved for hiring", 1, "Approved" });

            migrationBuilder.InsertData(
                table: "PerformanceReviewItems",
                columns: new[] { "Id", "Comment", "GoalId", "ReviewId", "Score" },
                values: new object[] { 1, "Good performance", 1, 1, 4.0m });

            migrationBuilder.InsertData(
                table: "InterviewSchedules",
                columns: new[] { "Id", "CandidateApplicationId", "InterviewerId", "Location", "Notes", "ScheduledAt", "Status" },
                values: new object[] { 1, 1, 3, "Meeting Room 1", null, new DateTime(2026, 3, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), "Scheduled" });

            migrationBuilder.InsertData(
                table: "OfferLetters",
                columns: new[] { "Id", "CandidateApplicationId", "OfferedSalary", "SentAt", "StartDate", "Status" },
                values: new object[] { 1, 1, 10000000m, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sent" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AssetAssignments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AssetCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AssetCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AssetCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AssetIncidents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BonusRequests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarBookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CertificationRenewals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ExpenseCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExpenseCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExpenseCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExpenseCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "FormFieldOptions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InterviewSchedules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobRequisitionApprovals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MealRegistrations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OffboardingTasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OfferLetters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OvertimeRates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OvertimeRates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OvertimeRates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PerformanceReviewItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PolicyAcknowledgements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequestApprovals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RequestApprovals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequestApprovals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RequestApprovals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RequestApprovals",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RequestApprovals",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RequestAuditLogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RequestAuditLogs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequestAuditLogs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RequestAuditLogs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RequestData",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SalaryAdjustmentRequests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SlaConfigs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SlaConfigs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TrainingEnrollments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UniformRequests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "WorkflowSteps",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkflowSteps",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkflowSteps",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorkflowSteps",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WorkflowSteps",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WorkflowSteps",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "WorkflowSteps",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "WorkflowSteps",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "WorkflowSteps",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CandidateApplications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Certifications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OffboardingTaskTemplates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OnboardingTaskTemplates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PerformanceGoals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PerformanceReviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PolicyDocuments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TrainingCourses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AssetCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "JobRequisitions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PerformanceCycles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
