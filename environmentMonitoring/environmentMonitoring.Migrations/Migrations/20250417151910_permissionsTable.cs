using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace environmentMonitoring.Database.Migrations
{
    /// <inheritdoc />
    public partial class permissionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    permission_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.permission_Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionsRole",
                columns: table => new
                {
                    Permissionspermission_Id = table.Column<int>(type: "int", nullable: false),
                    role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsRole", x => new { x.Permissionspermission_Id, x.role_Id });
                    table.ForeignKey(
                        name: "FK_PermissionsRole_Permissions_Permissionspermission_Id",
                        column: x => x.Permissionspermission_Id,
                        principalTable: "Permissions",
                        principalColumn: "permission_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionsRole_Roles_role_Id",
                        column: x => x.role_Id,
                        principalTable: "Roles",
                        principalColumn: "role_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsRole_role_Id",
                table: "PermissionsRole",
                column: "role_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionsRole");

            migrationBuilder.DropTable(
                name: "Permissions");
        }
    }
}
