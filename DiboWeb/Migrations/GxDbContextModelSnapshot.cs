﻿// <auto-generated />
using DiboWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;

namespace DiboWeb.Migrations
{
    [DbContext(typeof(GxDbContext))]
    partial class GxDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("DiboWeb.Models.GeoPoint", b =>
                {
                    b.Property<long>("Id");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<int>("ProjectId");

                    b.Property<double>("X");

                    b.Property<double>("Y");

                    b.Property<double>("Z");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("ProjectId");

                    b.ToTable("GeoPoint");
                });

            modelBuilder.Entity("DiboWeb.Models.GxLine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("EndDeep");

                    b.Property<long>("EndPointId");

                    b.Property<int>("ProjectId");

                    b.Property<string>("PropertiyString");

                    b.Property<double>("StartDeep");

                    b.Property<long>("StartPointId");

                    b.HasKey("Id");

                    b.HasIndex("EndPointId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StartPointId");

                    b.ToTable("Line");
                });

            modelBuilder.Entity("DiboWeb.Models.GxPoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExpNo")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("MapNo")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("ProjectId");

                    b.Property<string>("PropertiyString");

                    b.Property<double>("X");

                    b.Property<double>("Y");

                    b.Property<double>("Z");

                    b.HasKey("Id");

                    b.HasIndex("MapNo");

                    b.HasIndex("ProjectId");

                    b.ToTable("Point");
                });

            modelBuilder.Entity("DiboWeb.Models.GxProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlternativeString");

                    b.Property<int>("Code");

                    b.Property<int>("DataType");

                    b.Property<string>("DefaultValue");

                    b.Property<int>("GeometryType");

                    b.Property<string>("Label");

                    b.Property<int>("MainType");

                    b.Property<string>("Name");

                    b.Property<bool>("Required");

                    b.Property<int>("Sequence");

                    b.HasKey("Id");

                    b.ToTable("Property");
                });

            modelBuilder.Entity("DiboWeb.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatorId");

                    b.Property<string>("Name");

                    b.Property<int>("PropertyTemplateId");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("PropertyTemplateId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("DiboWeb.Models.PropertyTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Template");
                });

            modelBuilder.Entity("DiboWeb.Models.Template_Property", b =>
                {
                    b.Property<int>("GxPropertyId");

                    b.Property<int>("PropertyTemplateId");

                    b.HasKey("GxPropertyId", "PropertyTemplateId");

                    b.HasIndex("PropertyTemplateId");

                    b.ToTable("Template_Property");
                });

            modelBuilder.Entity("DiboWeb.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("PassWord");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DiboWeb.Models.UserProject", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("ProjectId");

                    b.Property<bool>("Active");

                    b.Property<int>("Id");

                    b.Property<string>("Role");

                    b.HasKey("UserId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("UserProject");
                });

            modelBuilder.Entity("DiboWeb.Models.GeoPoint", b =>
                {
                    b.HasOne("DiboWeb.Models.Project", "Project")
                        .WithMany("GeoPoints")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiboWeb.Models.GxLine", b =>
                {
                    b.HasOne("DiboWeb.Models.GxPoint", "EndPoint")
                        .WithMany()
                        .HasForeignKey("EndPointId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DiboWeb.Models.Project", "Project")
                        .WithMany("GxLines")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DiboWeb.Models.GxPoint", "StartPoint")
                        .WithMany()
                        .HasForeignKey("StartPointId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiboWeb.Models.GxPoint", b =>
                {
                    b.HasOne("DiboWeb.Models.Project", "Project")
                        .WithMany("GxPoints")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiboWeb.Models.Project", b =>
                {
                    b.HasOne("DiboWeb.Models.User", "Creator")
                        .WithMany("CreatedProjects")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DiboWeb.Models.PropertyTemplate", "PropertyTemplate")
                        .WithMany()
                        .HasForeignKey("PropertyTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiboWeb.Models.Template_Property", b =>
                {
                    b.HasOne("DiboWeb.Models.GxProperty", "GxProperty")
                        .WithMany("Template_Properties")
                        .HasForeignKey("GxPropertyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DiboWeb.Models.PropertyTemplate", "PropertyTemplate")
                        .WithMany("Template_Properties")
                        .HasForeignKey("PropertyTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiboWeb.Models.UserProject", b =>
                {
                    b.HasOne("DiboWeb.Models.Project", "Project")
                        .WithMany("UserProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DiboWeb.Models.User", "User")
                        .WithMany("UserProjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}