using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DANGCAPNE.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9994));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9998));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9999));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(3));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1542));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1547));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1549));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1550));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1552));

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1553));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1877));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1886));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1887));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1888));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1889));

            migrationBuilder.UpdateData(
                table: "LeaveBalances",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1891));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1940));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1944));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1946));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1968));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1982));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1989));

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1992));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9929));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9933));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9934));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9935));

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 603, DateTimeKind.Local).AddTicks(9695));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1163));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1165));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1166));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1167));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1168));

            migrationBuilder.UpdateData(
                table: "UserManagers",
                keyColumn: "Id",
                keyValue: 6,
                column: "StartDate",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1169));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1128));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1131));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1132));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1132));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1133));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1134));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1135));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1136));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "AssignedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1136));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(979), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(977), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(980) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(987), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(987), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(987) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(992), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(991), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(992) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(995), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(995), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(995) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1073), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1072), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1073) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1077), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1077), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1078) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1080), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1080), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1081) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "HireDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1083), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1083), new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1084) });

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1470));

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1474));

            migrationBuilder.UpdateData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 21, 33, 24, 604, DateTimeKind.Local).AddTicks(1475));
        }
    }
}
