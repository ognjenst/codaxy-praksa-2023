using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC.IoT.ApiGateway.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationfixpermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "Name",
                value: "Read-Workflow");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "Name",
                value: "Read-Automation");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "Name",
                value: "Read-Device");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "Name",
                value: "Read-DeviceHistory");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "Name",
                value: "Read-Trigger");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "Name",
                value: "Create-Workflow");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "Name",
                value: "Update-Workflow");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "Name",
                value: "Read-Workflow");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "Name",
                value: "Delete-Workflow");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "Name",
                value: "Update-Trigger");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name", "RoleId" },
                values: new object[] { 27, "Read-Trigger", 2 });
        }
    }
}
