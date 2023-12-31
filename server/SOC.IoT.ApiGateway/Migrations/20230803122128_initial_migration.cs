﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SOC.IoT.ApiGateway.Migrations
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
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
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    LastName = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Username = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Email = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name", "RoleId" },
                values: new object[,]
                {
                    { 1, "Create-Workflow", 1 },
                    { 2, "Update-Workflow", 1 },
                    { 3, "Read-Workflow", 1 },
                    { 4, "Delete-Workflow", 1 },
                    { 5, "Create-Trigrer", 1 },
                    { 6, "Update-Trigger", 1 },
                    { 7, "Read-Trigger", 1 },
                    { 8, "Delete-Trigger", 1 },
                    { 9, "Create-Automation", 1 },
                    { 10, "Update-Automation", 1 },
                    { 11, "Read-Automation", 1 },
                    { 12, "Delete-Automation", 1 },
                    { 13, "Create-Device", 1 },
                    { 14, "Update-Device", 1 },
                    { 15, "Read-Device", 1 },
                    { 16, "Delete-Device", 1 },
                    { 17, "Create-DeviceHistory", 1 },
                    { 18, "Update-DeviceHistory", 1 },
                    { 19, "Read-DeviceHistory", 1 },
                    { 20, "Delete-DeviceHistory", 1 },
                    { 21, "Read-Workflow", 2 },
                    { 22, "Read-Automation", 2 },
                    { 23, "Read-Device", 2 },
                    { 24, "Read-DeviceHistory", 2 },
                    { 26, "Read-Trigger", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevicesHistory_DeviceID",
                table: "DevicesHistory",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevicesHistory");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
