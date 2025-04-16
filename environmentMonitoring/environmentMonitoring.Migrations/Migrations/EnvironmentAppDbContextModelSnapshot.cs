﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using environmentMonitoring.Database.Data;

#nullable disable

namespace environmentMonitoring.Database.Migrations
{
    [DbContext(typeof(EnvironmentAppDbContext))]
    partial class EnvironmentAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("environmentMonitoring.Database.Models.Anomalies", b =>
                {
                    b.Property<int>("anomaly_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("anomaly_Id"));

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("reading_Id")
                        .HasColumnType("int");

                    b.HasKey("anomaly_Id");

                    b.HasIndex("reading_Id");

                    b.ToTable("Anomalies");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.Quantities", b =>
                {
                    b.Property<int>("quantity_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("quantity_Id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("max_threshold")
                        .HasColumnType("real");

                    b.Property<float>("min_threshold")
                        .HasColumnType("real");

                    b.Property<string>("quantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("safe_level")
                        .HasColumnType("real");

                    b.Property<string>("symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("quantity_Id");

                    b.ToTable("Quantities");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.Readings", b =>
                {
                    b.Property<int>("reading_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("reading_Id"));

                    b.Property<DateTime>("timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("v_sensor_id")
                        .HasColumnType("int");

                    b.Property<float>("value")
                        .HasColumnType("real");

                    b.HasKey("reading_Id");

                    b.HasIndex("v_sensor_id");

                    b.ToTable("Readings");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.RealSensor", b =>
                {
                    b.Property<int>("r_sensor_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("r_sensor_Id"));

                    b.Property<float>("frequency")
                        .HasColumnType("real");

                    b.Property<float>("lat")
                        .HasColumnType("real");

                    b.Property<float>("lon")
                        .HasColumnType("real");

                    b.HasKey("r_sensor_Id");

                    b.ToTable("RealSensors");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.Reports", b =>
                {
                    b.Property<int>("report_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("report_Id"));

                    b.Property<string>("body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trend")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("v_sensor_id")
                        .HasColumnType("int");

                    b.HasKey("report_Id");

                    b.HasIndex("v_sensor_id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.Role", b =>
                {
                    b.Property<int>("role_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("role_Id"));

                    b.Property<string>("role_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("role_Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.User", b =>
                {
                    b.Property<int>("user_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_Id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("role_Id")
                        .HasColumnType("int");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_Id");

                    b.HasIndex("role_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.VirtualSensor", b =>
                {
                    b.Property<int>("v_sensor_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("v_sensor_Id"));

                    b.Property<string>("catergory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("quantity_id")
                        .HasColumnType("int");

                    b.Property<int>("r_sensor_Id")
                        .HasColumnType("int");

                    b.Property<string>("reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("sensor_type")
                        .HasColumnType("real");

                    b.Property<float>("url")
                        .HasColumnType("real");

                    b.HasKey("v_sensor_Id");

                    b.HasIndex("quantity_id");

                    b.HasIndex("r_sensor_Id");

                    b.ToTable("VirtualSensors");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.Anomalies", b =>
                {
                    b.HasOne("environmentMonitoring.Database.Models.Readings", "Reading")
                        .WithMany("Anomalies")
                        .HasForeignKey("reading_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reading");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.Readings", b =>
                {
                    b.HasOne("environmentMonitoring.Database.Models.VirtualSensor", "VirtualSensor")
                        .WithMany("Readings")
                        .HasForeignKey("v_sensor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VirtualSensor");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.Reports", b =>
                {
                    b.HasOne("environmentMonitoring.Database.Models.VirtualSensor", "VirtualSensor")
                        .WithMany("Reports")
                        .HasForeignKey("v_sensor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VirtualSensor");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.User", b =>
                {
                    b.HasOne("environmentMonitoring.Database.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.VirtualSensor", b =>
                {
                    b.HasOne("environmentMonitoring.Database.Models.Quantities", "Quantity")
                        .WithMany("VirtualSensor")
                        .HasForeignKey("quantity_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("environmentMonitoring.Database.Models.RealSensor", "RealSensor")
                        .WithMany("VirtualSensor")
                        .HasForeignKey("r_sensor_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quantity");

                    b.Navigation("RealSensor");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.Quantities", b =>
                {
                    b.Navigation("VirtualSensor");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.Readings", b =>
                {
                    b.Navigation("Anomalies");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.RealSensor", b =>
                {
                    b.Navigation("VirtualSensor");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("environmentMonitoring.Database.Models.VirtualSensor", b =>
                {
                    b.Navigation("Readings");

                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
