using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class a12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaSP",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropColumn(
                name: "moTa",
                table: "san_Pham_Chi_Tiets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaSP",
                table: "san_Pham_Chi_Tiets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "moTa",
                table: "san_Pham_Chi_Tiets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
