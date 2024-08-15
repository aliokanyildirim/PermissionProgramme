using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KayitProgrami.Migrations
{
    /// <inheritdoc />
    public partial class izinTalebiConfigurationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_KullaniciId",
                table: "LeaveRequests");

            migrationBuilder.RenameColumn(
                name: "KullaniciId",
                table: "LeaveRequests",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveRequests_KullaniciId",
                table: "LeaveRequests",
                newName: "IX_LeaveRequests_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_UserId",
                table: "LeaveRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_UserId",
                table: "LeaveRequests");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "LeaveRequests",
                newName: "KullaniciId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveRequests_UserId",
                table: "LeaveRequests",
                newName: "IX_LeaveRequests_KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_KullaniciId",
                table: "LeaveRequests",
                column: "KullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
