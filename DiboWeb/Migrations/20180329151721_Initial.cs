using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DiboWeb.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    AlternativeString = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    DataType = table.Column<int>(nullable: false),
                    DefaultValue = table.Column<string>(nullable: true),
                    GeometryType = table.Column<int>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    MainType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Sequence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    PassWord = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Template_Property",
                columns: table => new
                {
                    GxPropertyId = table.Column<int>(nullable: false),
                    PropertyTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template_Property", x => new { x.GxPropertyId, x.PropertyTemplateId });
                    table.ForeignKey(
                        name: "FK_Template_Property_Property_GxPropertyId",
                        column: x => x.GxPropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Template_Property_Template_PropertyTemplateId",
                        column: x => x.PropertyTemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatorId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PropertyTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_Template_PropertyTemplateId",
                        column: x => x.PropertyTemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeoPoint",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    X = table.Column<double>(nullable: false),
                    Y = table.Column<double>(nullable: false),
                    Z = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeoPoint_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ExpNo = table.Column<string>(maxLength: 128, nullable: false),
                    MapNo = table.Column<string>(maxLength: 128, nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    PropertiyString = table.Column<string>(nullable: true),
                    X = table.Column<double>(nullable: false),
                    Y = table.Column<double>(nullable: false),
                    Z = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Point_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProject",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProject", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_UserProject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProject_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Line",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    EndDeep = table.Column<double>(nullable: false),
                    EndPointId = table.Column<long>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    PropertiyString = table.Column<string>(nullable: true),
                    StartDeep = table.Column<double>(nullable: false),
                    StartPointId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Line_Point_EndPointId",
                        column: x => x.EndPointId,
                        principalTable: "Point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Line_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Line_Point_StartPointId",
                        column: x => x.StartPointId,
                        principalTable: "Point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoPoint_Name",
                table: "GeoPoint",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_GeoPoint_ProjectId",
                table: "GeoPoint",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Line_EndPointId",
                table: "Line",
                column: "EndPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Line_ProjectId",
                table: "Line",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Line_StartPointId",
                table: "Line",
                column: "StartPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Point_MapNo",
                table: "Point",
                column: "MapNo");

            migrationBuilder.CreateIndex(
                name: "IX_Point_ProjectId",
                table: "Point",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CreatorId",
                table: "Project",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_PropertyTemplateId",
                table: "Project",
                column: "PropertyTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Template_Property_PropertyTemplateId",
                table: "Template_Property",
                column: "PropertyTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_ProjectId",
                table: "UserProject",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoPoint");

            migrationBuilder.DropTable(
                name: "Line");

            migrationBuilder.DropTable(
                name: "Template_Property");

            migrationBuilder.DropTable(
                name: "UserProject");

            migrationBuilder.DropTable(
                name: "Point");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Template");
        }
    }
}
