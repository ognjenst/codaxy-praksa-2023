﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC.Conductor.Migrations
{
    /// <inheritdoc />
    public partial class TriggerDateTimeOffsetChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Start",
                table: "PeriodicTriggers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Start",
                table: "PeriodicTriggers",
                type: "interval",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");
        }
    }
}
