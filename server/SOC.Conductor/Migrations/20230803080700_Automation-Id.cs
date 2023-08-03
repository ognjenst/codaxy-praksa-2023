using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SOC.Conductor.Migrations
{
    /// <inheritdoc />
    public partial class AutomationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Automations",
                table: "Automations");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Automations",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Automations",
                table: "Automations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Automations_WorkflowId",
                table: "Automations",
                column: "WorkflowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Automations",
                table: "Automations");

            migrationBuilder.DropIndex(
                name: "IX_Automations_WorkflowId",
                table: "Automations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Automations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Automations",
                table: "Automations",
                columns: new[] { "WorkflowId", "TriggerId" });
        }
    }
}
