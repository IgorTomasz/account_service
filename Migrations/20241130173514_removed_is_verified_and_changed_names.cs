using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace account_service.Migrations
{
    /// <inheritdoc />
    public partial class removed_is_verified_and_changed_names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isVerified",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "passwordHash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "Users",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "dateOfBirth",
                table: "Users",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Users",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "passwordHash");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Users",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Users",
                newName: "dateOfBirth");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "userId");

            migrationBuilder.AddColumn<bool>(
                name: "isVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
