using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KayitProgrami.Migrations
{
    /// <inheritdoc />
    public partial class izinTalebiConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IzinTarihiBitis",
                table: "LeaveRequests",
                newName: "LeaveDateEnd");

            migrationBuilder.RenameColumn(
                name: "IzinTarihiBaslangic",
                table: "LeaveRequests",
                newName: "LeaveDateStart");

            migrationBuilder.RenameColumn(
                name: "Aciklama",
                table: "LeaveRequests",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LeaveDateStart",
                table: "LeaveRequests",
                newName: "IzinTarihiBaslangic");

            migrationBuilder.RenameColumn(
                name: "LeaveDateEnd",
                table: "LeaveRequests",
                newName: "IzinTarihiBitis");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "LeaveRequests",
                newName: "Aciklama");
        }
    }
}
