using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DANGCAPNE.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    AllowedRadius = table.Column<double>(type: "float", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyAttendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    ActualCheckIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualCheckOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsLate = table.Column<bool>(type: "bit", nullable: false),
                    LateMinutes = table.Column<int>(type: "int", nullable: false),
                    IsEarlyLeave = table.Column<bool>(type: "bit", nullable: false),
                    EarlyLeaveMinutes = table.Column<int>(type: "int", nullable: false),
                    EffectiveHours = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    HasApprovedLeave = table.Column<bool>(type: "bit", nullable: false),
                    HasApprovedOutside = table.Column<bool>(type: "bit", nullable: false),
                    CalculatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyAttendances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    ToEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RelatedRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BodyHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RequiresReceipt = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveAccruals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<double>(type: "float", nullable: false),
                    AccrualType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccrualDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RelatedRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveAccruals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DefaultDaysPerYear = table.Column<double>(type: "float", nullable: false),
                    AllowCarryOver = table.Column<bool>(type: "bit", nullable: false),
                    CarryOverMaxDays = table.Column<int>(type: "int", nullable: false),
                    CarryOverExpiryMonth = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    AllowNegativeBalance = table.Column<bool>(type: "bit", nullable: false),
                    IconColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OvertimeRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Multiplier = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    BreakStartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    BreakEndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    GracePeriodMinutes = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemErrors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Severity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemErrors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubDomain = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PrimaryColor = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    SecondaryColor = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Plan = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MaxUsers = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workflows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    FromCurrencyId = table.Column<int>(type: "int", nullable: false),
                    ToCurrencyId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currencies_FromCurrencyId",
                        column: x => x.FromCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currencies_ToCurrencyId",
                        column: x => x.ToCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Plan = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MonthlyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantConfigs_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IconColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WorkflowId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RequiresFinancialApproval = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormTemplates_Workflows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflows",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StepOrder = table.Column<int>(type: "int", nullable: false),
                    ApproverType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApproverUserId = table.Column<int>(type: "int", nullable: true),
                    ApproverRoleId = table.Column<int>(type: "int", nullable: true),
                    CanSkipIfApplicant = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowSteps_Workflows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormTemplateId = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FieldType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Placeholder = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ValidationRules = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DefaultValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Width = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormFields_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlaConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    FormTemplateId = table.Column<int>(type: "int", nullable: true),
                    ReminderHours = table.Column<int>(type: "int", nullable: false),
                    EscalationHours = table.Column<int>(type: "int", nullable: false),
                    AutoRemind = table.Column<bool>(type: "bit", nullable: false),
                    AutoEscalate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlaConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlaConfigs_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowStepId = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NextStepId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowConditions_WorkflowSteps_WorkflowStepId",
                        column: x => x.WorkflowStepId,
                        principalTable: "WorkflowSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowStepApprovers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowStepId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowStepApprovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowStepApprovers_WorkflowSteps_WorkflowStepId",
                        column: x => x.WorkflowStepId,
                        principalTable: "WorkflowSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormFieldOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormFieldId = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFieldOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormFieldOptions_FormFields_FormFieldId",
                        column: x => x.FormFieldId,
                        principalTable: "FormFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    AssetCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AssignedToUserId = table.Column<int>(type: "int", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AssetCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AssetCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Delegations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    DelegatorId = table.Column<int>(type: "int", nullable: false),
                    DelegateId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delegations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParentDepartmentId = table.Column<int>(type: "int", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Departments_ParentDepartmentId",
                        column: x => x.ParentDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    JobTitleId = table.Column<int>(type: "int", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Locale = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    PinHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_JobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DraftRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FormTemplateId = table.Column<int>(type: "int", nullable: false),
                    FormDataJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastSavedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftRequests_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EscalationRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    SlaConfigId = table.Column<int>(type: "int", nullable: false),
                    EscalateToUserId = table.Column<int>(type: "int", nullable: false),
                    EscalateAfterHours = table.Column<int>(type: "int", nullable: false),
                    NotificationMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalationRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalationRules_SlaConfigs_SlaConfigId",
                        column: x => x.SlaConfigId,
                        principalTable: "SlaConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscalationRules_Users_EscalateToUserId",
                        column: x => x.EscalateToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveBalances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    TotalEntitled = table.Column<double>(type: "float", nullable: false),
                    Used = table.Column<double>(type: "float", nullable: false),
                    CarriedOver = table.Column<double>(type: "float", nullable: false),
                    SeniorityBonus = table.Column<double>(type: "float", nullable: false),
                    CompensatoryDays = table.Column<double>(type: "float", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveBalances_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveBalances_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActionUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RelatedRequestId = table.Column<int>(type: "int", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    RequestCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FormTemplateId = table.Column<int>(type: "int", nullable: false),
                    RequesterId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CurrentStepOrder = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Users_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LeaderId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Users_LeaderId",
                        column: x => x.LeaderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Timesheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GpsLatitude = table.Column<double>(type: "float", nullable: true),
                    GpsLongitude = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    WorkHours = table.Column<double>(type: "float", nullable: false),
                    OtHours = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsValidGps = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timesheets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserManagers_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserManagers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserShifts_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserShifts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    StepOrder = table.Column<int>(type: "int", nullable: false),
                    StepName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerifiedByPin = table.Column<bool>(type: "bit", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestApprovals_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestApprovals_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestAttachments_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestAuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OldStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NewStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestAuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestAuditLogs_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestAuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCommentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestComments_RequestComments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "RequestComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestComments_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FieldValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestData_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestFollowers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FollowedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestFollowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestFollowers_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "Id", "Code", "CreatedAt", "IsActive", "ManagerId", "Name", "ParentDepartmentId", "TenantId" },
                values: new object[,]
                {
                    { 1, "BOD", new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9994), true, null, "Ban Giám đốc", null, 1 },
                    { 2, "IT", new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9998), true, null, "Phòng Công nghệ Thông tin", null, 1 },
                    { 3, "HR", new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9999), true, null, "Phòng Nhân sự", null, 1 },
                    { 4, "ACC", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local), true, null, "Phòng Kế toán", null, 1 },
                    { 5, "SALES", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1), true, null, "Phòng Kinh doanh", null, 1 },
                    { 6, "MKT", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(3), true, null, "Phòng Marketing", null, 1 }
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
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9929), "Quản trị viên hệ thống", "Admin", 1 },
                    { 2, new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9933), "Hành chính Nhân sự", "HR", 1 },
                    { 3, new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9934), "Quản lý", "Manager", 1 },
                    { 4, new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9935), "Nhân viên", "Employee", 1 }
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
                values: new object[] { 1, "DANGCAPNE Corporation", new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9695), null, true, "", 500, "Enterprise", "#6366f1", "#8b5cf6", "dangcapne" });

            migrationBuilder.InsertData(
                table: "Workflows",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1470), "Quản lý trực tiếp -> HR", true, "Luồng duyệt cơ bản", 1 },
                    { 2, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1474), "Quản lý -> Kế toán -> Giám đốc", true, "Luồng duyệt tài chính", 1 },
                    { 3, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1475), "Quản lý -> Trưởng phòng -> HR -> Giám đốc", true, "Luồng duyệt vượt cấp", 1 }
                });

            migrationBuilder.InsertData(
                table: "FormTemplates",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "Icon", "IconColor", "IsActive", "Name", "RequiresFinancialApproval", "TenantId", "WorkflowId" },
                values: new object[,]
                {
                    { 1, "Leave", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1542), "", "bi-calendar-x", "#10b981", true, "Đơn xin nghỉ phép", false, 1, 1 },
                    { 2, "OT", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1547), "", "bi-clock-history", "#f59e0b", true, "Đơn làm thêm giờ (OT)", false, 1, 1 },
                    { 3, "Travel", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1549), "", "bi-airplane", "#3b82f6", true, "Đơn đi công tác", false, 1, 1 },
                    { 4, "Expense", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1550), "", "bi-cash-stack", "#ef4444", true, "Đơn tạm ứng chi phí", true, 1, 2 },
                    { 5, "Equipment", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1552), "", "bi-laptop", "#8b5cf6", true, "Đơn yêu cầu cấp phát thiết bị", false, 1, 1 },
                    { 6, "Leave", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1553), "", "bi-box-arrow-right", "#dc2626", true, "Đơn xin nghỉ việc", false, 1, 3 }
                });

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
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "BranchId", "CreatedAt", "DepartmentId", "Email", "EmployeeCode", "FullName", "HireDate", "JobTitleId", "Locale", "PasswordHash", "Phone", "PinHash", "PositionId", "Status", "TenantId", "TerminationDate", "TimeZone", "TwoFactorEnabled", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, "", 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(995), 2, "employee@company.com", "NV004", "Phạm Thị Employee", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(995), 6, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234570", null, null, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(995) },
                    { 5, "", 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1073), 2, "dev@company.com", "NV005", "Hoàng Văn Dev", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1072), 5, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234571", null, null, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1073) },
                    { 7, "", 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1080), 5, "sales@company.com", "NV007", "Đỗ Văn Sales", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1080), 6, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234573", null, null, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1081) },
                    { 8, "", 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1083), 6, "marketing@company.com", "NV008", "Ngô Thị Marketing", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1083), 6, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234574", null, null, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1084) }
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
                    { 1, 0.0, 0.0, 1, 0.0, 1, 12.0, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1877), 3.0, 4, 2026 },
                    { 2, 0.0, 0.0, 2, 0.0, 1, 30.0, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1886), 1.0, 4, 2026 },
                    { 3, 0.0, 0.0, 1, 0.0, 1, 12.0, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1887), 2.0, 5, 2026 },
                    { 4, 0.0, 0.0, 2, 0.0, 1, 30.0, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1888), 0.0, 5, 2026 },
                    { 5, 0.0, 0.0, 1, 0.0, 1, 12.0, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1889), 5.0, 7, 2026 },
                    { 6, 0.0, 0.0, 1, 0.0, 1, 12.0, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1891), 1.0, 8, 2026 }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "ActionUrl", "CreatedAt", "IsRead", "Message", "ReadAt", "RelatedRequestId", "TenantId", "Title", "Type", "UserId" },
                values: new object[] { 2, null, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1944), false, "Chào mừng bạn đến với hệ thống quản lý đơn từ DANGCAPNE", null, null, 1, "Chào mừng!", "Info", 4 });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "CompletedAt", "CreatedAt", "CurrentStepOrder", "FormTemplateId", "Priority", "RequestCode", "RequesterId", "Status", "TenantId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Normal", "REQ-20260305-001", 4, "Pending", 1, "Xin nghỉ phép năm 3 ngày", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1968) },
                    { 2, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Normal", "REQ-20260307-001", 5, "Approved", 1, "Làm thêm giờ dự án ERP", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1982) },
                    { 3, null, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, "High", "REQ-20260310-001", 7, "InProgress", 1, "Tạm ứng đi công tác Đà Nẵng", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1989) },
                    { 4, new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Normal", "REQ-20260311-001", 8, "Rejected", 1, "Xin nghỉ phép 1 ngày", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1992) }
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
                table: "UserRoles",
                columns: new[] { "Id", "AssignedAt", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 4, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1132), 4, 4 },
                    { 5, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1133), 4, 5 },
                    { 7, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1135), 4, 7 },
                    { 8, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1136), 4, 8 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "BranchId", "CreatedAt", "DepartmentId", "Email", "EmployeeCode", "FullName", "HireDate", "JobTitleId", "Locale", "PasswordHash", "Phone", "PinHash", "PositionId", "Status", "TenantId", "TerminationDate", "TimeZone", "TwoFactorEnabled", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "", 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(979), 1, "admin@company.com", "NV001", "Nguyễn Văn Admin", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(977), 1, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234567", null, 1, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(980) },
                    { 2, "", 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(987), 3, "hr@company.com", "NV002", "Trần Thị HR", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(987), 3, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234568", null, 3, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(987) },
                    { 3, "", 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(992), 2, "manager@company.com", "NV003", "Lê Văn Manager", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(991), 3, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234569", null, 2, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(992) },
                    { 6, "", 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1077), 4, "accountant@company.com", "NV006", "Vũ Thị Kế Toán", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1077), 3, "vi-VN", "HSEqbOoWNtVcp7r8Ous+JPgWx7cfiZ9kKGR02yw1Vk8=", "0901234572", null, 4, "Active", 1, null, "SE Asia Standard Time", false, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1078) }
                });

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
                table: "Notifications",
                columns: new[] { "Id", "ActionUrl", "CreatedAt", "IsRead", "Message", "ReadAt", "RelatedRequestId", "TenantId", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, "/Approvals", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1940), false, "Phạm Thị Employee đã tạo đơn xin nghỉ phép", null, null, 1, "Đơn mới cần duyệt", "Approval", 3 },
                    { 3, "/HR", new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1946), false, "Có 5 đơn mới cần HR xử lý trong tuần này", null, null, 1, "Báo cáo tuần", "Info", 2 }
                });

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
                table: "UserManagers",
                columns: new[] { "Id", "EndDate", "IsPrimary", "ManagerId", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, null, true, 3, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1163), 4 },
                    { 2, null, true, 3, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1165), 5 },
                    { 3, null, true, 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1166), 3 },
                    { 4, null, true, 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1167), 2 },
                    { 5, null, true, 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1168), 7 },
                    { 6, null, true, 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1169), 8 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "AssignedAt", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1128), 1, 1 },
                    { 2, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1131), 2, 2 },
                    { 3, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1132), 3, 3 },
                    { 6, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1134), 4, 6 },
                    { 9, new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1136), 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssignedToUserId",
                table: "Assets",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_CategoryId",
                table: "Assets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Delegations_DelegateId",
                table: "Delegations",
                column: "DelegateId");

            migrationBuilder.CreateIndex(
                name: "IX_Delegations_DelegatorId",
                table: "Delegations",
                column: "DelegatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentDepartmentId",
                table: "Departments",
                column: "ParentDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftRequests_FormTemplateId",
                table: "DraftRequests",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftRequests_UserId",
                table: "DraftRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalationRules_EscalateToUserId",
                table: "EscalationRules",
                column: "EscalateToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalationRules_SlaConfigId",
                table: "EscalationRules",
                column: "SlaConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_FromCurrencyId",
                table: "ExchangeRates",
                column: "FromCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_ToCurrencyId",
                table: "ExchangeRates",
                column: "ToCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFieldOptions_FormFieldId",
                table: "FormFieldOptions",
                column: "FormFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFields_FormTemplateId",
                table: "FormFields",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplates_WorkflowId",
                table: "FormTemplates",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveBalances_LeaveTypeId",
                table: "LeaveBalances",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveBalances_UserId_Year",
                table: "LeaveBalances",
                columns: new[] { "UserId", "Year" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_IsRead",
                table: "Notifications",
                columns: new[] { "UserId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DepartmentId",
                table: "Positions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestApprovals_ApproverId",
                table: "RequestApprovals",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestApprovals_RequestId",
                table: "RequestApprovals",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAttachments_RequestId",
                table: "RequestAttachments",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAuditLogs_RequestId",
                table: "RequestAuditLogs",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAuditLogs_UserId",
                table: "RequestAuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestComments_ParentCommentId",
                table: "RequestComments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestComments_RequestId",
                table: "RequestComments",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestComments_UserId",
                table: "RequestComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestData_RequestId",
                table: "RequestData",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestFollowers_RequestId",
                table: "RequestFollowers",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_FormTemplateId",
                table: "Requests",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequesterId",
                table: "Requests",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_Status",
                table: "Requests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_TenantId",
                table: "Requests",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_SlaConfigs_FormTemplateId",
                table: "SlaConfigs",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_TenantId",
                table: "Subscriptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamId",
                table: "TeamMembers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_UserId",
                table: "TeamMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeaderId",
                table: "Teams",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantConfigs_TenantId",
                table: "TenantConfigs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_UserId_Date",
                table: "Timesheets",
                columns: new[] { "UserId", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_UserManagers_ManagerId",
                table: "UserManagers",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserManagers_UserId",
                table: "UserManagers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BranchId",
                table: "Users",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Users_JobTitleId",
                table: "Users",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PositionId",
                table: "Users",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                table: "Users",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserShifts_ShiftId",
                table: "UserShifts",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserShifts_UserId",
                table: "UserShifts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowConditions_WorkflowStepId",
                table: "WorkflowConditions",
                column: "WorkflowStepId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStepApprovers_WorkflowStepId",
                table: "WorkflowStepApprovers",
                column: "WorkflowStepId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowSteps_WorkflowId",
                table: "WorkflowSteps",
                column: "WorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Users_AssignedToUserId",
                table: "Assets",
                column: "AssignedToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Delegations_Users_DelegateId",
                table: "Delegations",
                column: "DelegateId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Delegations_Users_DelegatorId",
                table: "Delegations",
                column: "DelegatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Users_ManagerId",
                table: "Departments",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_ManagerId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "DailyAttendances");

            migrationBuilder.DropTable(
                name: "Delegations");

            migrationBuilder.DropTable(
                name: "DraftRequests");

            migrationBuilder.DropTable(
                name: "EmailLogs");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "EscalationRules");

            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.DropTable(
                name: "FormFieldOptions");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "LeaveAccruals");

            migrationBuilder.DropTable(
                name: "LeaveBalances");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OvertimeRates");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "RequestApprovals");

            migrationBuilder.DropTable(
                name: "RequestAttachments");

            migrationBuilder.DropTable(
                name: "RequestAuditLogs");

            migrationBuilder.DropTable(
                name: "RequestComments");

            migrationBuilder.DropTable(
                name: "RequestData");

            migrationBuilder.DropTable(
                name: "RequestFollowers");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SystemErrors");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "TenantConfigs");

            migrationBuilder.DropTable(
                name: "Timesheets");

            migrationBuilder.DropTable(
                name: "UserManagers");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserShifts");

            migrationBuilder.DropTable(
                name: "WorkflowConditions");

            migrationBuilder.DropTable(
                name: "WorkflowStepApprovers");

            migrationBuilder.DropTable(
                name: "AssetCategories");

            migrationBuilder.DropTable(
                name: "SlaConfigs");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "FormFields");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "WorkflowSteps");

            migrationBuilder.DropTable(
                name: "FormTemplates");

            migrationBuilder.DropTable(
                name: "Workflows");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
