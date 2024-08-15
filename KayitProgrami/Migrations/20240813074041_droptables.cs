using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KayitProgrami.Migrations
{
    /// <inheritdoc />
    public partial class droptables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IzinTalepleri_AspNetUsers_KullaniciId",
                table: "IzinTalepleri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IzinTalepleri",
                table: "IzinTalepleri");

            migrationBuilder.RenameTable(
                name: "IzinTalepleri",
                newName: "LeaveRequests");

            migrationBuilder.RenameIndex(
                name: "IX_IzinTalepleri_KullaniciId",
                table: "LeaveRequests",
                newName: "IX_LeaveRequests_KullaniciId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveRequests",
                table: "LeaveRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_KullaniciId",
                table: "LeaveRequests",
                column: "KullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_KullaniciId",
                table: "LeaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveRequests",
                table: "LeaveRequests");

            migrationBuilder.RenameTable(
                name: "LeaveRequests",
                newName: "IzinTalepleri");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveRequests_KullaniciId",
                table: "IzinTalepleri",
                newName: "IX_IzinTalepleri_KullaniciId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IzinTalepleri",
                table: "IzinTalepleri",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "IzinDurumlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurumAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzinDurumlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciRoller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciRoller", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_IzinTalepleri_AspNetUsers_KullaniciId",
                table: "IzinTalepleri",
                column: "KullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
