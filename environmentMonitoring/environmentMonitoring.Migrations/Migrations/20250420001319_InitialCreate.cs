using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace environmentMonitoring.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    permission_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.permission_Id);
                });

            migrationBuilder.CreateTable(
                name: "Quantities",
                columns: table => new
                {
                    quantity_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    safe_level = table.Column<float>(type: "real", nullable: false),
                    min_threshold = table.Column<float>(type: "real", nullable: false),
                    max_threshold = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quantities", x => x.quantity_Id);
                });

            migrationBuilder.CreateTable(
                name: "RealSensors",
                columns: table => new
                {
                    r_sensor_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lat = table.Column<float>(type: "real", nullable: false),
                    lon = table.Column<float>(type: "real", nullable: false),
                    frequency = table.Column<float>(type: "real", nullable: false),
                    status = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealSensors", x => x.r_sensor_Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    role_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.role_Id);
                });

            migrationBuilder.CreateTable(
                name: "VirtualSensors",
                columns: table => new
                {
                    v_sensor_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    catergory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sensor_type = table.Column<float>(type: "nvarchar(max)", nullable: false),
                    url = table.Column<float>(type: "nvarchar(max)", nullable: false),
                    r_sensor_Id = table.Column<int>(type: "int", nullable: false),
                    quantity_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualSensors", x => x.v_sensor_Id);
                    table.ForeignKey(
                        name: "FK_VirtualSensors_Quantities_quantity_id",
                        column: x => x.quantity_id,
                        principalTable: "Quantities",
                        principalColumn: "quantity_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VirtualSensors_RealSensors_r_sensor_Id",
                        column: x => x.r_sensor_Id,
                        principalTable: "RealSensors",
                        principalColumn: "r_sensor_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    role_Id = table.Column<int>(type: "int", nullable: false),
                    permission_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.role_Id, x.permission_Id });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permission_permission_Id",
                        column: x => x.permission_Id,
                        principalTable: "Permission",
                        principalColumn: "permission_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_role_Id",
                        column: x => x.role_Id,
                        principalTable: "Roles",
                        principalColumn: "role_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_role_Id",
                        column: x => x.role_Id,
                        principalTable: "Roles",
                        principalColumn: "role_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Readings",
                columns: table => new
                {
                    reading_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    value = table.Column<float>(type: "real", nullable: false),
                    v_sensor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readings", x => x.reading_Id);
                    table.ForeignKey(
                        name: "FK_Readings_VirtualSensors_v_sensor_id",
                        column: x => x.v_sensor_id,
                        principalTable: "VirtualSensors",
                        principalColumn: "v_sensor_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    report_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trend = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    v_sensor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.report_Id);
                    table.ForeignKey(
                        name: "FK_Reports_VirtualSensors_v_sensor_id",
                        column: x => x.v_sensor_id,
                        principalTable: "VirtualSensors",
                        principalColumn: "v_sensor_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncidentReports",
                columns: table => new
                {
                    incident_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    next_steps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reading_Id = table.Column<int>(type: "int", nullable: true),
                    r_sensor_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentReports", x => x.incident_Id);
                    table.ForeignKey(
                        name: "FK_IncidentReports_Readings_reading_Id",
                        column: x => x.reading_Id,
                        principalTable: "Readings",
                        principalColumn: "reading_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IncidentReports_RealSensors_reading_Id",
                        column: x => x.reading_Id,
                        principalTable: "RealSensors",
                        principalColumn: "r_sensor_Id");
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "permission_Id", "description", "name" },
                values: new object[,]
                {
                    { 1, "create user", "CreateUsers" },
                    { 2, "read users", "ReadUsers" },
                    { 3, "update users", "UpdateUsers" },
                    { 4, "delete users", "DeleteUsers" },
                    { 5, "create sensor", "CreateSensors" },
                    { 6, "read sensors", "ReadSensors" },
                    { 7, "update sensors", "UpdateSensors" },
                    { 8, "delete sensor", "DeleteSensors" },
                    { 9, "assign roles", "ManageUserRoles" },
                    { 10, "set role permissions", "SetRolePermissions" },
                    { 11, "create incident report", "CreateIncidentReport" },
                    { 12, "read incident report", "ReadIncidentReport" },
                    { 13, "update incident report", "UpdateIncidentReport" },
                    { 14, "delete incident report", "DeleteIncidentReport" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "role_Id", "role_type" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Environment Scientist" },
                    { 3, "Operations Manager" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "permission_Id", "role_Id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 2, 2 },
                    { 5, 2 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 2 },
                    { 11, 2 },
                    { 12, 2 },
                    { 13, 2 },
                    { 14, 2 },
                    { 2, 3 },
                    { 6, 3 },
                    { 11, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncidentReports_reading_Id",
                table: "IncidentReports",
                column: "reading_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Readings_v_sensor_id",
                table: "Readings",
                column: "v_sensor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_v_sensor_id",
                table: "Reports",
                column: "v_sensor_id");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_permission_Id",
                table: "RolePermissions",
                column: "permission_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_role_Id",
                table: "Users",
                column: "role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualSensors_quantity_id",
                table: "VirtualSensors",
                column: "quantity_id");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualSensors_r_sensor_Id",
                table: "VirtualSensors",
                column: "r_sensor_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncidentReports");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Readings");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "VirtualSensors");

            migrationBuilder.DropTable(
                name: "Quantities");

            migrationBuilder.DropTable(
                name: "RealSensors");
        }
    }
}
