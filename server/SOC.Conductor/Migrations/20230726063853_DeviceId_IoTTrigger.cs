using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC.Conductor.Migrations
{
    /// <inheritdoc />
    public partial class DeviceId_IoTTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "IoTTriggers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "IoTTriggers");
        }
    }
}
