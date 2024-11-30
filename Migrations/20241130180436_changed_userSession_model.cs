using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace account_service.Migrations
{
    /// <inheritdoc />
    public partial class changed_userSession_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_Users_userId",
                table: "UserSessions");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserSessions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "UserSessions",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "startTime",
                table: "UserSessions",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "endTime",
                table: "UserSessions",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "deviceInfo",
                table: "UserSessions",
                newName: "DeviceInfo");

            migrationBuilder.RenameColumn(
                name: "sessionId",
                table: "UserSessions",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "UserSessions",
                newName: "Reftoken");

            migrationBuilder.RenameColumn(
                name: "idAddress",
                table: "UserSessions",
                newName: "IpAddress");

            migrationBuilder.RenameIndex(
                name: "IX_UserSessions_userId",
                table: "UserSessions",
                newName: "IX_UserSessions_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_Users_UserId",
                table: "UserSessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_Users_UserId",
                table: "UserSessions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserSessions",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "UserSessions",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "UserSessions",
                newName: "startTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "UserSessions",
                newName: "endTime");

            migrationBuilder.RenameColumn(
                name: "DeviceInfo",
                table: "UserSessions",
                newName: "deviceInfo");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "UserSessions",
                newName: "sessionId");

            migrationBuilder.RenameColumn(
                name: "Reftoken",
                table: "UserSessions",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "UserSessions",
                newName: "idAddress");

            migrationBuilder.RenameIndex(
                name: "IX_UserSessions_UserId",
                table: "UserSessions",
                newName: "IX_UserSessions_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_Users_userId",
                table: "UserSessions",
                column: "userId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
