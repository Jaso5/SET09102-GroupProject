using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace environmentMonitoring.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIncidentReportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "date",
                table: "IncidentReports",
                newName: "reportDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "lastUpdatedDate",
                table: "IncidentReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "resolution",
                table: "IncidentReports",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastUpdatedDate",
                table: "IncidentReports");

            migrationBuilder.DropColumn(
                name: "resolution",
                table: "IncidentReports");

            migrationBuilder.RenameColumn(
                name: "reportDate",
                table: "IncidentReports",
                newName: "date");
        }
    }
}
