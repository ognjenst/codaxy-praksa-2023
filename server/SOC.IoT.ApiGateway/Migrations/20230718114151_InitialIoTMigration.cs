using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SOC.IoT.ApiGateway.Migrations
{
    /// <inheritdoc />
    public partial class InitialIoTMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IoTId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Manufacturer = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DevicesHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceID = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Configuration = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicesHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevicesHistory_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "Description", "IoTId", "Manufacturer", "Model", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Hue white and color ambiance E26/E27", "0x00178801062f573d", "Philips", "9290022166", "Philips Lightbulb", 0 },
                    { 2, "Wireless button", "0x00124b002268b3b2", "SONOFF", "SNZB-01", "Philips Lightbulb", 3 },
                    { 3, "Temperature and humidity sensor", "0x00124b0022d2d320", "SONOFF", "SNZB-02", "SONOFF Temp and Humidity Sensor", 2 },
                    { 4, "Mi power plug ZigBee EU", "0x04cf8cdf3c7597cd", "Xiaomi", "ZNCZ04LM", "Xiaomi Mi Socket", 1 },
                    { 5, "Contact sensor", "0x00124b00226969ac", "SONOFF", "SNZB-04", "Contact Sensor", 2 },
                    { 6, "Wireless switch with 4 buttons", "0xa4c13893cdd87674", "TuYa", "TS0044", "Wireless switch", 3 },
                    { 7, "Smart 7W E27 light bulb", "0x00158d0001dd7e46", "Nue / 3A", "HGZB-06A", "Light bulb", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevicesHistory_DeviceID",
                table: "DevicesHistory",
                column: "DeviceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevicesHistory");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
