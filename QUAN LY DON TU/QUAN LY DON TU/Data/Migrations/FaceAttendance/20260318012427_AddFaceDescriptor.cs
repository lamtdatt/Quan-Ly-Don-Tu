using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DANGCAPNE.Data.Migrations.FaceAttendance
{
    /// <inheritdoc />
    public partial class AddFaceDescriptor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FaceDescriptor",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidWifi",
                table: "Timesheets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Timesheets",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QrCodeKey",
                table: "Timesheets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WifiBssid",
                table: "Timesheets",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WifiName",
                table: "Timesheets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AttendanceLocationConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    WifiName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WifiBssid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QrCodeKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AllowedLatitude = table.Column<double>(type: "float", nullable: true),
                    AllowedLongitude = table.Column<double>(type: "float", nullable: true),
                    AllowedRadiusMeters = table.Column<int>(type: "int", nullable: false),
                    RequirePhoto = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceLocationConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceLocationConfigs_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDocuments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShiftSwapRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    RequesterId = table.Column<int>(type: "int", nullable: false),
                    TargetUserId = table.Column<int>(type: "int", nullable: false),
                    RequesterShiftId = table.Column<int>(type: "int", nullable: false),
                    RequesterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TargetShiftId = table.Column<int>(type: "int", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedByManagerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftSwapRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftSwapRequests_Users_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftSwapRequests_Users_TargetUserId",
                        column: x => x.TargetUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialInsurances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InsuranceNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalaryBasis = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialInsurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialInsurances_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 626, DateTimeKind.Local).AddTicks(8886));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 626, DateTimeKind.Local).AddTicks(8889));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 626, DateTimeKind.Local).AddTicks(8891));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 626, DateTimeKind.Local).AddTicks(8892));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 626, DateTimeKind.Local).AddTicks(8893));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 626, DateTimeKind.Local).AddTicks(8894));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2877));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2880));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2882));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2884));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2885));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2887));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3027));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3036));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3037));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3039));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3040));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3041));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3091));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3094));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3095));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3271));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3274));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3275));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3116));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3124));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3130));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3132));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3294));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3295));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3296));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3296));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3301));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3301));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 626, DateTimeKind.Local).AddTicks(8820));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 626, DateTimeKind.Local).AddTicks(8826));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 626, DateTimeKind.Local).AddTicks(8828));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 626, DateTimeKind.Local).AddTicks(8829));

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 626, DateTimeKind.Local).AddTicks(8552));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2561));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2563));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartDate",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2564));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartDate",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2565));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartDate",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2566));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 6,
                column: "StartDate",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2566));

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "GrantedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(3316));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2520));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2522));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2523));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2524));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2534));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2535));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2535));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2536));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2537));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FaceDescriptor", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2440), null, new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2436), new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2441) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FaceDescriptor", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2449), null, new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2449), new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2450) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FaceDescriptor", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2453), null, new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2452), new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2453) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FaceDescriptor", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2455), null, new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2455), new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2455) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FaceDescriptor", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2457), null, new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2457), new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2458) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FaceDescriptor", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2460), null, new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2459), new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2460) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FaceDescriptor", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2462), null, new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2462), new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2462) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FaceDescriptor", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2464), null, new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2464), new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2465) });

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2812));

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2816));

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 8, 24, 23, 627, DateTimeKind.Local).AddTicks(2818));

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceLocationConfigs_BranchId",
                table: "AttendanceLocationConfigs",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_UserId",
                table: "EmployeeDocuments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSwapRequests_RequesterId",
                table: "ShiftSwapRequests",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSwapRequests_TargetUserId",
                table: "ShiftSwapRequests",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialInsurances_UserId",
                table: "SocialInsurances",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceLocationConfigs");

            migrationBuilder.DropTable(
                name: "EmployeeDocuments");

            migrationBuilder.DropTable(
                name: "ShiftSwapRequests");

            migrationBuilder.DropTable(
                name: "SocialInsurances");

            migrationBuilder.DropColumn(
                name: "FaceDescriptor",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsValidWifi",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "QrCodeKey",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "WifiBssid",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "WifiName",
                table: "Timesheets");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6427));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6431));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6432));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6433));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6434));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6435));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8207));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8213));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8215));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8216));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8218));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8219));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8384));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8389));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8391));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8392));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8393));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8395));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8445));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8449));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8450));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8667));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8670));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8671));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8474));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8483));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8489));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8491));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8691));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8697));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8698));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8699));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8699));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8700));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6364));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6368));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6369));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6372));

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(6158));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7767));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7771));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartDate",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7772));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartDate",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7773));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartDate",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7774));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 6,
                column: "StartDate",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7775));

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "GrantedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8725));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7717));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7732));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7733));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7734));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7735));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7735));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7736));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7737));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7625), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7625), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7626) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7635), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7635), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7636) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7639), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7639), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7639) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7642), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7642), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7642) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7644), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7644), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7645) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7647), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7647), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7647) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7677), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7661), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7678) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7680), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7680), new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8122));

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8128));

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 55, 14, 522, DateTimeKind.Local).AddTicks(8129));
        }
    }
}
