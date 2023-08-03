﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SOC.Conductor.Entities.Contexts;

#nullable disable

namespace SOC.Conductor.Migrations
{
    [DbContext(typeof(SOCDbContext))]
    partial class SOCDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SOC.Conductor.Entities.Automation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("InputParameters")
                        .HasColumnType("jsonb");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TriggerId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkflowId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TriggerId");

                    b.HasIndex("WorkflowId");

                    b.ToTable("Automations");
                });

            modelBuilder.Entity("SOC.Conductor.Entities.Trigger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Triggers");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SOC.Conductor.Entities.Workflow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Workflows");
                });

            modelBuilder.Entity("SOC.Conductor.Entities.IoTTrigger", b =>
                {
                    b.HasBaseType("SOC.Conductor.Entities.Trigger");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Property")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("IoTTriggers");
                });

            modelBuilder.Entity("SOC.Conductor.Entities.PeriodicTrigger", b =>
                {
                    b.HasBaseType("SOC.Conductor.Entities.Trigger");

                    b.Property<int>("Period")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Unit")
                        .HasColumnType("integer");

                    b.ToTable("PeriodicTriggers");
                });

            modelBuilder.Entity("SOC.Conductor.Entities.Automation", b =>
                {
                    b.HasOne("SOC.Conductor.Entities.Trigger", "Trigger")
                        .WithMany()
                        .HasForeignKey("TriggerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOC.Conductor.Entities.Workflow", "Workflow")
                        .WithMany()
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trigger");

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("SOC.Conductor.Entities.IoTTrigger", b =>
                {
                    b.HasOne("SOC.Conductor.Entities.Trigger", null)
                        .WithOne()
                        .HasForeignKey("SOC.Conductor.Entities.IoTTrigger", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SOC.Conductor.Entities.PeriodicTrigger", b =>
                {
                    b.HasOne("SOC.Conductor.Entities.Trigger", null)
                        .WithOne()
                        .HasForeignKey("SOC.Conductor.Entities.PeriodicTrigger", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
