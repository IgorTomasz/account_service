using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace account_service.Migrations
{
    /// <inheritdoc />
    public partial class changed_lastLogin_to_LastLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastLogin",
                table: "Users",
                newName: "LastLogin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLogin",
                table: "Users",
                newName: "lastLogin");
        }
    }
}
