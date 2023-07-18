﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SOC.IoT.ApiGateway.Entities.Contexts;

#nullable disable

namespace SOC.IoT.ApiGateway.Migrations
{
    [DbContext(typeof(SOCIoTDbContext))]
    [Migration("20230718114151_InitialIoTMigration")]
    partial class InitialIoTMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SOC.IoT.ApiGateway.Entities.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IoTId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Devices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Hue white and color ambiance E26/E27",
                            IoTId = "0x00178801062f573d",
                            Manufacturer = "Philips",
                            Model = "9290022166",
                            Name = "Philips Lightbulb",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            Description = "Wireless button",
                            IoTId = "0x00124b002268b3b2",
                            Manufacturer = "SONOFF",
                            Model = "SNZB-01",
                            Name = "Philips Lightbulb",
                            Type = 3
                        },
                        new
                        {
                            Id = 3,
                            Description = "Temperature and humidity sensor",
                            IoTId = "0x00124b0022d2d320",
                            Manufacturer = "SONOFF",
                            Model = "SNZB-02",
                            Name = "SONOFF Temp and Humidity Sensor",
                            Type = 2
                        },
                        new
                        {
                            Id = 4,
                            Description = "Mi power plug ZigBee EU",
                            IoTId = "0x04cf8cdf3c7597cd",
                            Manufacturer = "Xiaomi",
                            Model = "ZNCZ04LM",
                            Name = "Xiaomi Mi Socket",
                            Type = 1
                        },
                        new
                        {
                            Id = 5,
                            Description = "Contact sensor",
                            IoTId = "0x00124b00226969ac",
                            Manufacturer = "SONOFF",
                            Model = "SNZB-04",
                            Name = "Contact Sensor",
                            Type = 2
                        },
                        new
                        {
                            Id = 6,
                            Description = "Wireless switch with 4 buttons",
                            IoTId = "0xa4c13893cdd87674",
                            Manufacturer = "TuYa",
                            Model = "TS0044",
                            Name = "Wireless switch",
                            Type = 3
                        },
                        new
                        {
                            Id = 7,
                            Description = "Smart 7W E27 light bulb",
                            IoTId = "0x00158d0001dd7e46",
                            Manufacturer = "Nue / 3A",
                            Model = "HGZB-06A",
                            Name = "Light bulb",
                            Type = 0
                        });
                });

            modelBuilder.Entity("SOC.IoT.ApiGateway.Entities.DeviceHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Configuration")
                        .HasColumnType("jsonb");

                    b.Property<int>("DeviceID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeviceID");

                    b.ToTable("DevicesHistory");
                });

            modelBuilder.Entity("SOC.IoT.ApiGateway.Entities.DeviceHistory", b =>
                {
                    b.HasOne("SOC.IoT.ApiGateway.Entities.Device", "Device")
                        .WithMany("DevicesHistory")
                        .HasForeignKey("DeviceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("SOC.IoT.ApiGateway.Entities.Device", b =>
                {
                    b.Navigation("DevicesHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
