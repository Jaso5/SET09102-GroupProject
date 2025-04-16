using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace environmentMonitoring.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    frequency = table.Column<float>(type: "real", nullable: false)
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
                    sensor_type = table.Column<float>(type: "real", nullable: false),
                    url = table.Column<float>(type: "real", nullable: false),
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
                name: "Anomalies",
                columns: table => new
                {
                    anomaly_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reading_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anomalies", x => x.anomaly_Id);
                    table.ForeignKey(
                        name: "FK_Anomalies_Readings_reading_Id",
                        column: x => x.reading_Id,
                        principalTable: "Readings",
                        principalColumn: "reading_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anomalies_reading_Id",
                table: "Anomalies",
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
                name: "Anomalies");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Readings");

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
