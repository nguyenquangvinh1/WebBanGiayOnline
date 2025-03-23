using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class dat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("224a2944-21fc-4036-be13-9c7f1b6ba045"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("84d87dac-2912-42f4-9b60-b7c9669c996a"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("d2780ece-596e-4051-b63b-3a4c1c4a6a77"));

            migrationBuilder.AlterColumn<double>(
                name: "thanh_tien",
                table: "don_Chi_Tiets",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateTable(
                name: "shippingModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    tinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    huyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    xa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shippingModels", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("0d671287-f0a2-487e-8ac2-f833bdf70bf0"), null, new DateTime(2025, 3, 19, 15, 57, 3, 824, DateTimeKind.Local).AddTicks(6744), "Khách hàng", 1 },
                    { new Guid("611e1538-ed29-4a22-b144-529766f382d7"), null, new DateTime(2025, 3, 19, 15, 57, 3, 824, DateTimeKind.Local).AddTicks(6738), "Nhân Viên", 1 },
                    { new Guid("e9c7bd8b-1944-43a9-ba8b-c6880c566918"), null, new DateTime(2025, 3, 19, 15, 57, 3, 824, DateTimeKind.Local).AddTicks(6709), "Admin", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shippingModels");

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("0d671287-f0a2-487e-8ac2-f833bdf70bf0"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("611e1538-ed29-4a22-b144-529766f382d7"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("e9c7bd8b-1944-43a9-ba8b-c6880c566918"));

            migrationBuilder.AlterColumn<double>(
                name: "thanh_tien",
                table: "don_Chi_Tiets",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("224a2944-21fc-4036-be13-9c7f1b6ba045"), null, new DateTime(2025, 3, 18, 21, 24, 32, 247, DateTimeKind.Local).AddTicks(9681), "Admin", 1 },
                    { new Guid("84d87dac-2912-42f4-9b60-b7c9669c996a"), null, new DateTime(2025, 3, 18, 21, 24, 32, 247, DateTimeKind.Local).AddTicks(9713), "Khách hàng", 1 },
                    { new Guid("d2780ece-596e-4051-b63b-3a4c1c4a6a77"), null, new DateTime(2025, 3, 18, 21, 24, 32, 247, DateTimeKind.Local).AddTicks(9710), "Nhân Viên", 1 }
                });
        }
    }
}
