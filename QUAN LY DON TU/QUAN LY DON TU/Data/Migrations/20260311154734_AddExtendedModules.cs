using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DANGCAPNE.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddExtendedModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetAssignments_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetAssignments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetIncidents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    ReportedByUserId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReportedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetIncidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetIncidents_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetIncidents_Users_ReportedByUserId",
                        column: x => x.ReportedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BonusRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RequestedByUserId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonusRequests_Users_RequestedByUserId",
                        column: x => x.RequestedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BonusRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PickupLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobRequisitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    JobTitleId = table.Column<int>(type: "int", nullable: true),
                    Headcount = table.Column<int>(type: "int", nullable: false),
                    BudgetMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BudgetMax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequisitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequisitions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobRequisitions_JobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobRequisitions_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MealRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MealType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealRegistrations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OffboardingTaskTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DefaultDueDays = table.Column<int>(type: "int", nullable: false),
                    DefaultAssigneeRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffboardingTaskTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OffboardingTaskTemplates_Roles_DefaultAssigneeRoleId",
                        column: x => x.DefaultAssigneeRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OnboardingTaskTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DefaultDueDays = table.Column<int>(type: "int", nullable: false),
                    DefaultAssigneeRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnboardingTaskTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnboardingTaskTemplates_Roles_DefaultAssigneeRoleId",
                        column: x => x.DefaultAssigneeRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PerformanceCycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceCycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicyDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryAdjustmentRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RequestedByUserId = table.Column<int>(type: "int", nullable: true),
                    ProposedSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryAdjustmentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryAdjustmentRequests_Users_RequestedByUserId",
                        column: x => x.RequestedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalaryAdjustmentRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UniformRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniformRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniformRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CertificationRenewals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificationId = table.Column<int>(type: "int", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ApprovedByUserId = table.Column<int>(type: "int", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationRenewals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificationRenewals_Certifications_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CertificationRenewals_Users_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    JobRequisitionId = table.Column<int>(type: "int", nullable: false),
                    AppliedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateApplications_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateApplications_JobRequisitions_JobRequisitionId",
                        column: x => x.JobRequisitionId,
                        principalTable: "JobRequisitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobRequisitionApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobRequisitionId = table.Column<int>(type: "int", nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequisitionApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequisitionApprovals_JobRequisitions_JobRequisitionId",
                        column: x => x.JobRequisitionId,
                        principalTable: "JobRequisitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobRequisitionApprovals_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OffboardingTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AssignedToUserId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffboardingTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OffboardingTasks_OffboardingTaskTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "OffboardingTaskTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OffboardingTasks_Users_AssignedToUserId",
                        column: x => x.AssignedToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OffboardingTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnboardingTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AssignedToUserId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnboardingTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnboardingTasks_OnboardingTaskTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "OnboardingTaskTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnboardingTasks_Users_AssignedToUserId",
                        column: x => x.AssignedToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OnboardingTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CycleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceGoals_PerformanceCycles_CycleId",
                        column: x => x.CycleId,
                        principalTable: "PerformanceCycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerformanceGoals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CycleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_PerformanceCycles_CycleId",
                        column: x => x.CycleId,
                        principalTable: "PerformanceCycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_Users_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    GrantedByUserId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    GrantedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_GrantedByUserId",
                        column: x => x.GrantedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyAcknowledgements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyDocumentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AcknowledgedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyAcknowledgements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyAcknowledgements_PolicyDocuments_PolicyDocumentId",
                        column: x => x.PolicyDocumentId,
                        principalTable: "PolicyDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyAcknowledgements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingEnrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EnrolledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingEnrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingEnrollments_TrainingCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "TrainingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingEnrollments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterviewSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateApplicationId = table.Column<int>(type: "int", nullable: false),
                    InterviewerId = table.Column<int>(type: "int", nullable: true),
                    ScheduledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterviewSchedules_CandidateApplications_CandidateApplicationId",
                        column: x => x.CandidateApplicationId,
                        principalTable: "CandidateApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewSchedules_Users_InterviewerId",
                        column: x => x.InterviewerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfferLetters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateApplicationId = table.Column<int>(type: "int", nullable: false),
                    OfferedSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferLetters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferLetters_CandidateApplications_CandidateApplicationId",
                        column: x => x.CandidateApplicationId,
                        principalTable: "CandidateApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceReviewItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewId = table.Column<int>(type: "int", nullable: false),
                    GoalId = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviewItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceReviewItems_PerformanceGoals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "PerformanceGoals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PerformanceReviewItems_PerformanceReviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "PerformanceReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "AssetCode", "AssignedDate", "AssignedToUserId", "CategoryId", "Name", "PurchaseDate", "PurchasePrice", "SerialNumber", "Status", "TenantId" },
                values: new object[] { 1, "AST-001", null, null, 1, "Laptop Dell", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 15000000m, null, "Available", 1 });

            migrationBuilder.InsertData(
                table: "BonusRequests",
                columns: new[] { "Id", "Amount", "CreatedAt", "Reason", "RequestedByUserId", "Status", "TenantId", "Type", "UserId" },
                values: new object[] { 1, 2000000m, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Project delivery", 3, "Pending", 1, "Spot", 5 });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "Phone", "Source", "TenantId" },
                values: new object[] { 1, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidate@demo.com", "Tran Thi Candidate", "0909000001", "Referral", 1 });

            migrationBuilder.InsertData(
                table: "CarBookings",
                columns: new[] { "Id", "Destination", "DriverName", "EndTime", "PickupLocation", "StartTime", "Status", "TenantId", "UserId" },
                values: new object[] { 1, "Client Site", "Nguyen Driver", new DateTime(2026, 3, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), "Office", new DateTime(2026, 3, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 1, 5 });

            migrationBuilder.InsertData(
                table: "Certifications",
                columns: new[] { "Id", "ExpiryDate", "IssuedDate", "Name", "Status", "TenantId", "UserId" },
                values: new object[] { 1, new DateTime(2027, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Safety Basics", "Active", 1, 4 });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 939, DateTimeKind.Local).AddTicks(9308));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 939, DateTimeKind.Local).AddTicks(9310));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 939, DateTimeKind.Local).AddTicks(9311));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 939, DateTimeKind.Local).AddTicks(9312));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 939, DateTimeKind.Local).AddTicks(9322));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 939, DateTimeKind.Local).AddTicks(9323));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(715));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(723));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(724));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(726));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(728));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(730));

            migrationBuilder.InsertData(
                table: "JobRequisitions",
                columns: new[] { "Id", "BudgetMax", "BudgetMin", "CreatedAt", "CreatedByUserId", "DepartmentId", "Headcount", "JobTitleId", "Status", "TenantId", "Title" },
                values: new object[] { 1, 12000000m, 8000000m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 1, 5, "Pending", 1, "HR Specialist" });

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(903));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(907));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(909));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(910));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(911));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(912));

            migrationBuilder.InsertData(
                table: "MealRegistrations",
                columns: new[] { "Id", "Date", "MealType", "Notes", "TenantId", "UserId" },
                values: new object[] { 1, new DateTime(2026, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Overtime", "Vegetarian", 1, 4 });

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(968));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(973));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(974));

            migrationBuilder.InsertData(
                table: "OffboardingTaskTemplates",
                columns: new[] { "Id", "DefaultAssigneeRoleId", "DefaultDueDays", "Description", "Name", "TenantId" },
                values: new object[] { 1, 2, 2, "Collect laptop and badge", "Return Assets", 1 });

            migrationBuilder.InsertData(
                table: "OnboardingTaskTemplates",
                columns: new[] { "Id", "DefaultAssigneeRoleId", "DefaultDueDays", "Description", "Name", "TenantId" },
                values: new object[] { 1, 2, 3, "Prepare laptop and account", "Laptop Setup", 1 });

            migrationBuilder.InsertData(
                table: "PerformanceCycles",
                columns: new[] { "Id", "CreatedAt", "EndDate", "Name", "StartDate", "Status", "TenantId" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "2026 H1 Review", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open", 1 });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "IsActive", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "REQUEST_CREATE", new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1221), "Create request", true, "Create Request", 1 },
                    { 2, "REQUEST_APPROVE", new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1223), "Approve request", true, "Approve Request", 1 },
                    { 3, "SYSTEM_ADMIN", new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1224), "System administration", true, "System Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "PolicyDocuments",
                columns: new[] { "Id", "FileUrl", "IsActive", "PublishedAt", "TenantId", "Title", "Version" },
                values: new object[] { 1, "/docs/handbook.pdf", true, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Employee Handbook", "1.0" });

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1009));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1021));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1026));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1029));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 939, DateTimeKind.Local).AddTicks(9240));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 939, DateTimeKind.Local).AddTicks(9244));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 939, DateTimeKind.Local).AddTicks(9245));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 939, DateTimeKind.Local).AddTicks(9246));

            migrationBuilder.InsertData(
                table: "SalaryAdjustmentRequests",
                columns: new[] { "Id", "CreatedAt", "EffectiveDate", "ProposedSalary", "Reason", "RequestedByUserId", "Status", "TenantId", "UserId" },
                values: new object[] { 1, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12000000m, "High performance", 3, "Pending", 1, 4 });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 939, DateTimeKind.Local).AddTicks(9045));

            migrationBuilder.InsertData(
                table: "TrainingCourses",
                columns: new[] { "Id", "Cost", "EndDate", "IsActive", "Name", "Provider", "StartDate", "TenantId" },
                values: new object[] { 1, 500000m, new DateTime(2026, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Advanced Excel", "Internal", new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "UniformRequests",
                columns: new[] { "Id", "Quantity", "RequestedAt", "Size", "Status", "TenantId", "UserId" },
                values: new object[] { 1, 2, new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "L", "Pending", 1, 7 });

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(335));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(337));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(338));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(338));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(339));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 6,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(340));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(293));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(295));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(296));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(297));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(298));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(299));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(300));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(300));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(301));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(207), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(205), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(208) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(215), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(214), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(215) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(218), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(218), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(218) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(221), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(221), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(222) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(224), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(224), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(224) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(227), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(226), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(227) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(244), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(237), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(244) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(247), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(247), new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(247) });

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(642));

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(645));

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(646));

            migrationBuilder.InsertData(
                table: "AssetAssignments",
                columns: new[] { "Id", "AssetId", "AssignedAt", "ReturnedAt", "Status", "UserId" },
                values: new object[] { 1, 1, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Assigned", 4 });

            migrationBuilder.InsertData(
                table: "AssetIncidents",
                columns: new[] { "Id", "AssetId", "Description", "ReportedAt", "ReportedByUserId", "Status", "Type" },
                values: new object[] { 1, 1, "Screen cracked", new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Open", "Damage" });

            migrationBuilder.InsertData(
                table: "CandidateApplications",
                columns: new[] { "Id", "AppliedAt", "CandidateId", "JobRequisitionId", "Status" },
                values: new object[] { 1, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Applied" });

            migrationBuilder.InsertData(
                table: "CertificationRenewals",
                columns: new[] { "Id", "ApprovedAt", "ApprovedByUserId", "CertificationId", "RequestedAt", "Status" },
                values: new object[] { 1, null, 2, 1, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending" });

            migrationBuilder.InsertData(
                table: "JobRequisitionApprovals",
                columns: new[] { "Id", "ActionDate", "ApproverId", "Comments", "JobRequisitionId", "Status" },
                values: new object[] { 1, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Approved for hiring", 1, "Approved" });

            migrationBuilder.InsertData(
                table: "OffboardingTasks",
                columns: new[] { "Id", "AssignedToUserId", "CompletedAt", "DueDate", "Status", "TemplateId", "UserId" },
                values: new object[] { 1, 2, null, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open", 1, 8 });

            migrationBuilder.InsertData(
                table: "OnboardingTasks",
                columns: new[] { "Id", "AssignedToUserId", "CompletedAt", "DueDate", "Status", "TemplateId", "UserId" },
                values: new object[] { 1, 2, null, new DateTime(2026, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open", 1, 4 });

            migrationBuilder.InsertData(
                table: "PerformanceGoals",
                columns: new[] { "Id", "CycleId", "Status", "Title", "UserId", "Weight" },
                values: new object[] { 1, 1, "Active", "Deliver projects on time", 4, 1.0m });

            migrationBuilder.InsertData(
                table: "PerformanceReviews",
                columns: new[] { "Id", "CycleId", "ReviewerId", "Score", "Status", "SubmittedAt", "UserId" },
                values: new object[] { 1, 1, 3, null, "Draft", null, 4 });

            migrationBuilder.InsertData(
                table: "PolicyAcknowledgements",
                columns: new[] { "Id", "AcknowledgedAt", "PolicyDocumentId", "Status", "UserId" },
                values: new object[] { 1, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Acknowledged", 4 });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "AssignedAt", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1248), 1, 4 },
                    { 2, new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1250), 2, 3 },
                    { 3, new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1251), 1, 1 },
                    { 4, new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1252), 2, 1 },
                    { 5, new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1252), 3, 1 },
                    { 6, new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1253), 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "TrainingEnrollments",
                columns: new[] { "Id", "CompletedAt", "CourseId", "EnrolledAt", "Status", "UserId" },
                values: new object[] { 1, null, 1, new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enrolled", 4 });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Id", "GrantedAt", "GrantedByUserId", "IsActive", "PermissionId", "UserId" },
                values: new object[] { 1, new DateTime(2026, 3, 11, 22, 47, 32, 940, DateTimeKind.Local).AddTicks(1282), 1, true, 2, 2 });

            migrationBuilder.InsertData(
                table: "InterviewSchedules",
                columns: new[] { "Id", "CandidateApplicationId", "InterviewerId", "Location", "Notes", "ScheduledAt", "Status" },
                values: new object[] { 1, 1, 3, "Meeting Room 1", null, new DateTime(2026, 3, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), "Scheduled" });

            migrationBuilder.InsertData(
                table: "OfferLetters",
                columns: new[] { "Id", "CandidateApplicationId", "OfferedSalary", "SentAt", "StartDate", "Status" },
                values: new object[] { 1, 1, 10000000m, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sent" });

            migrationBuilder.InsertData(
                table: "PerformanceReviewItems",
                columns: new[] { "Id", "Comment", "GoalId", "ReviewId", "Score" },
                values: new object[] { 1, "Good performance", 1, 1, 4.0m });

            migrationBuilder.CreateIndex(
                name: "IX_AssetAssignments_AssetId",
                table: "AssetAssignments",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAssignments_UserId",
                table: "AssetAssignments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetIncidents_AssetId",
                table: "AssetIncidents",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetIncidents_ReportedByUserId",
                table: "AssetIncidents",
                column: "ReportedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusRequests_RequestedByUserId",
                table: "BonusRequests",
                column: "RequestedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusRequests_UserId",
                table: "BonusRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateApplications_CandidateId",
                table: "CandidateApplications",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateApplications_JobRequisitionId",
                table: "CandidateApplications",
                column: "JobRequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBookings_UserId",
                table: "CarBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationRenewals_ApprovedByUserId",
                table: "CertificationRenewals",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationRenewals_CertificationId",
                table: "CertificationRenewals",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Certifications_UserId",
                table: "Certifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_CandidateApplicationId",
                table: "InterviewSchedules",
                column: "CandidateApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_InterviewerId",
                table: "InterviewSchedules",
                column: "InterviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequisitionApprovals_ApproverId",
                table: "JobRequisitionApprovals",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequisitionApprovals_JobRequisitionId",
                table: "JobRequisitionApprovals",
                column: "JobRequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequisitions_CreatedByUserId",
                table: "JobRequisitions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequisitions_DepartmentId",
                table: "JobRequisitions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequisitions_JobTitleId",
                table: "JobRequisitions",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_MealRegistrations_UserId",
                table: "MealRegistrations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OffboardingTasks_AssignedToUserId",
                table: "OffboardingTasks",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OffboardingTasks_TemplateId",
                table: "OffboardingTasks",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_OffboardingTasks_UserId",
                table: "OffboardingTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OffboardingTaskTemplates_DefaultAssigneeRoleId",
                table: "OffboardingTaskTemplates",
                column: "DefaultAssigneeRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferLetters_CandidateApplicationId",
                table: "OfferLetters",
                column: "CandidateApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OnboardingTasks_AssignedToUserId",
                table: "OnboardingTasks",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OnboardingTasks_TemplateId",
                table: "OnboardingTasks",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_OnboardingTasks_UserId",
                table: "OnboardingTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OnboardingTaskTemplates_DefaultAssigneeRoleId",
                table: "OnboardingTaskTemplates",
                column: "DefaultAssigneeRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoals_CycleId",
                table: "PerformanceGoals",
                column: "CycleId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoals_UserId",
                table: "PerformanceGoals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviewItems_GoalId",
                table: "PerformanceReviewItems",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviewItems_ReviewId",
                table: "PerformanceReviewItems",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_CycleId",
                table: "PerformanceReviews",
                column: "CycleId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_ReviewerId",
                table: "PerformanceReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_UserId",
                table: "PerformanceReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyAcknowledgements_PolicyDocumentId",
                table: "PolicyAcknowledgements",
                column: "PolicyDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyAcknowledgements_UserId",
                table: "PolicyAcknowledgements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryAdjustmentRequests_RequestedByUserId",
                table: "SalaryAdjustmentRequests",
                column: "RequestedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryAdjustmentRequests_UserId",
                table: "SalaryAdjustmentRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEnrollments_CourseId",
                table: "TrainingEnrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEnrollments_UserId",
                table: "TrainingEnrollments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UniformRequests_UserId",
                table: "UniformRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_GrantedByUserId",
                table: "UserPermissions",
                column: "GrantedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_UserId",
                table: "UserPermissions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetAssignments");

            migrationBuilder.DropTable(
                name: "AssetIncidents");

            migrationBuilder.DropTable(
                name: "BonusRequests");

            migrationBuilder.DropTable(
                name: "CarBookings");

            migrationBuilder.DropTable(
                name: "CertificationRenewals");

            migrationBuilder.DropTable(
                name: "InterviewSchedules");

            migrationBuilder.DropTable(
                name: "JobRequisitionApprovals");

            migrationBuilder.DropTable(
                name: "MealRegistrations");

            migrationBuilder.DropTable(
                name: "OffboardingTasks");

            migrationBuilder.DropTable(
                name: "OfferLetters");

            migrationBuilder.DropTable(
                name: "OnboardingTasks");

            migrationBuilder.DropTable(
                name: "PerformanceReviewItems");

            migrationBuilder.DropTable(
                name: "PolicyAcknowledgements");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SalaryAdjustmentRequests");

            migrationBuilder.DropTable(
                name: "TrainingEnrollments");

            migrationBuilder.DropTable(
                name: "UniformRequests");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Certifications");

            migrationBuilder.DropTable(
                name: "OffboardingTaskTemplates");

            migrationBuilder.DropTable(
                name: "CandidateApplications");

            migrationBuilder.DropTable(
                name: "OnboardingTaskTemplates");

            migrationBuilder.DropTable(
                name: "PerformanceGoals");

            migrationBuilder.DropTable(
                name: "PerformanceReviews");

            migrationBuilder.DropTable(
                name: "PolicyDocuments");

            migrationBuilder.DropTable(
                name: "TrainingCourses");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "JobRequisitions");

            migrationBuilder.DropTable(
                name: "PerformanceCycles");

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(1236));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(1240));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(1241));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(1242));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(1243));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(1244));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3110));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3116));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3118));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3119));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3121));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3123));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3301));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3305));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3307));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3308));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3309));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3310));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3441));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3446));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3447));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3470));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3482));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3487));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3490));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(1109));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(1116));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(1117));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(1118));

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(738));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2692));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2695));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2696));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2697));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2697));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 6,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2698));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2600));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2602));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2603));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2603));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2604));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2605));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2606));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2607));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2607));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2525), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2523), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2526) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2536), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2536), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2537) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2540), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2539), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2543), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2542), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2543) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2545), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2545), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2546) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2548), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2548), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2549) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2551), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2551), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2551) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2554), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2553), new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(2554) });

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3000));

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3004));

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 40, 35, 100, DateTimeKind.Local).AddTicks(3005));
        }
    }
}
