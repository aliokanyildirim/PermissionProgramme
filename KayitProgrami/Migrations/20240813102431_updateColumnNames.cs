using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KayitProgrami.Migrations
{
    /// <inheritdoc />
    public partial class updateColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetUsers_KullaniciId",
                table: "Permissions");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Permissions",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "KullanilanIzin",
                table: "Permissions",
                newName: "UsedLeave");

            migrationBuilder.RenameColumn(
                name: "KullaniciId",
                table: "Permissions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "KalanIzin",
                table: "Permissions",
                newName: "AnnualLeave");

            migrationBuilder.RenameColumn(
                name: "BitisTarihi",
                table: "Permissions",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "BaslangicTarihi",
                table: "Permissions",
                newName: "StarDate");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_KullaniciId",
                table: "Permissions",
                newName: "IX_Permissions_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetUsers_UserId",
                table: "Permissions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetUsers_UserId",
                table: "Permissions");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Permissions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Permissions",
                newName: "KullaniciId");

            migrationBuilder.RenameColumn(
                name: "UsedLeave",
                table: "Permissions",
                newName: "KullanilanIzin");

            migrationBuilder.RenameColumn(
                name: "StarDate",
                table: "Permissions",
                newName: "BaslangicTarihi");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Permissions",
                newName: "BitisTarihi");

            migrationBuilder.RenameColumn(
                name: "AnnualLeave",
                table: "Permissions",
                newName: "KalanIzin");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_UserId",
                table: "Permissions",
                newName: "IX_Permissions_KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetUsers_KullaniciId",
                table: "Permissions",
                column: "KullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
