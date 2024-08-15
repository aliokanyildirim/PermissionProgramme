using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KayitProgrami.Migrations
{
    /// <inheritdoc />
    public partial class annualLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnnualLeave",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsedLeave",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualLeave",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UsedLeave",
                table: "AspNetUsers");
        }
    }
}
