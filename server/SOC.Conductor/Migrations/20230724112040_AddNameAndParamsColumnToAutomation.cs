using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC.Conductor.Migrations
{
    /// <inheritdoc />
    public partial class AddNameAndParamsColumnToAutomation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Automations",
                table: "Automations");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Automations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InputParameters",
                table: "Automations",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Automations",
                table: "Automations",
                columns: new[] { "WorkflowId", "TriggerId", "Name" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Automations",
                table: "Automations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Automations");

            migrationBuilder.DropColumn(
                name: "InputParameters",
                table: "Automations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Automations",
                table: "Automations",
                columns: new[] { "WorkflowId", "TriggerId" });
        }
    }
}
