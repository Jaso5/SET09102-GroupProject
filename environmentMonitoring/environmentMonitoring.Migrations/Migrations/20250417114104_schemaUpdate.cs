using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace environmentMonitoring.Database.Migrations
{
    /// <inheritdoc />
    public partial class schemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anomalies");

            migrationBuilder.AddColumn<float>(
                name: "status",
                table: "RealSensors",
                type: "real",
                nullable: false,
                defaultValue: 0f);

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

            migrationBuilder.CreateIndex(
                name: "IX_IncidentReports_reading_Id",
                table: "IncidentReports",
                column: "reading_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncidentReports");

            migrationBuilder.DropColumn(
                name: "status",
                table: "RealSensors");

            migrationBuilder.CreateTable(
                name: "Anomalies",
                columns: table => new
                {
                    anomaly_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reading_Id = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
        }
    }
}
