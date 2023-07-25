using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC.Conductor.Migrations
{
    /// <inheritdoc />
    public partial class InputParamsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Automations",
                table: "Automations");

            migrationBuilder.AlterColumn<string>(
                name: "InputParameters",
                table: "Automations",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Automations",
                table: "Automations",
                columns: new[] { "WorkflowId", "TriggerId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Automations",
                table: "Automations");

            migrationBuilder.AlterColumn<string>(
                name: "InputParameters",
                table: "Automations",
                type: "jsonb",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Automations",
                table: "Automations",
                columns: new[] { "WorkflowId", "TriggerId", "Name" });
        }
    }
}
