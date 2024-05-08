using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_villa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnerName",
                table: "Villas",
                newName: "UserId");

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    CouponId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountAmount = table.Column<double>(type: "float", nullable: false),
                    MinAmount = table.Column<int>(type: "int", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.CouponId);
                });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount", "EndingDate", "MinAmount", "StartingDate" },
                values: new object[,]
                {
                    { 1, "10OFF", 50.0, new DateTime(2024, 6, 5, 16, 36, 44, 550, DateTimeKind.Local).AddTicks(7484), 200, new DateTime(2024, 5, 6, 16, 36, 44, 550, DateTimeKind.Local).AddTicks(7469) },
                    { 2, "20OFF", 100.0, new DateTime(2024, 5, 16, 16, 36, 44, 550, DateTimeKind.Local).AddTicks(7490), 500, new DateTime(2024, 5, 6, 16, 36, 44, 550, DateTimeKind.Local).AddTicks(7489) }
                });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 5, 6, 16, 36, 44, 551, DateTimeKind.Local).AddTicks(334), new DateTime(2024, 5, 6, 16, 36, 44, 551, DateTimeKind.Local).AddTicks(338) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 5, 6, 16, 36, 44, 551, DateTimeKind.Local).AddTicks(343), new DateTime(2024, 5, 6, 16, 36, 44, 551, DateTimeKind.Local).AddTicks(343) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "ed4cdaa3-868e-43d6-b899-c54e5fdd76eb");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "2b4020a4-ba31-4031-bc5b-22461c00e6f1");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: "2b4020a4-ba31-4031-bc5b-22461c00e6f1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Villas",
                newName: "OwnerName");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 10, 39, 55, 306, DateTimeKind.Local).AddTicks(1112), new DateTime(2024, 3, 19, 10, 39, 55, 306, DateTimeKind.Local).AddTicks(1123) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 10, 39, 55, 306, DateTimeKind.Local).AddTicks(1124), new DateTime(2024, 3, 19, 10, 39, 55, 306, DateTimeKind.Local).AddTicks(1124) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerName",
                value: "Tung Dao Duc");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerName",
                value: "Tung Dao");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "OwnerName",
                value: "Tung");
        }
    }
}
