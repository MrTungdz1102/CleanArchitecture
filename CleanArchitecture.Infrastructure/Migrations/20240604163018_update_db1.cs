using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_db1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Coupon_CouponId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CouponId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "EndingDate", "StartingDate" },
                values: new object[] { new DateTime(2024, 7, 4, 23, 30, 18, 642, DateTimeKind.Local).AddTicks(3540), new DateTime(2024, 6, 4, 23, 30, 18, 642, DateTimeKind.Local).AddTicks(3525) });

            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "CouponId",
                keyValue: 2,
                columns: new[] { "EndingDate", "StartingDate" },
                values: new object[] { new DateTime(2024, 6, 14, 23, 30, 18, 642, DateTimeKind.Local).AddTicks(3546), new DateTime(2024, 6, 4, 23, 30, 18, 642, DateTimeKind.Local).AddTicks(3545) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 6, 4, 23, 30, 18, 642, DateTimeKind.Local).AddTicks(5271), new DateTime(2024, 6, 4, 23, 30, 18, 642, DateTimeKind.Local).AddTicks(5278) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 6, 4, 23, 30, 18, 642, DateTimeKind.Local).AddTicks(5280), new DateTime(2024, 6, 4, 23, 30, 18, 642, DateTimeKind.Local).AddTicks(5281) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "CouponId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "EndingDate", "StartingDate" },
                values: new object[] { new DateTime(2024, 6, 26, 22, 40, 24, 482, DateTimeKind.Local).AddTicks(1904), new DateTime(2024, 5, 27, 22, 40, 24, 482, DateTimeKind.Local).AddTicks(1891) });

            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "CouponId",
                keyValue: 2,
                columns: new[] { "EndingDate", "StartingDate" },
                values: new object[] { new DateTime(2024, 6, 6, 22, 40, 24, 482, DateTimeKind.Local).AddTicks(1909), new DateTime(2024, 5, 27, 22, 40, 24, 482, DateTimeKind.Local).AddTicks(1908) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 5, 27, 22, 40, 24, 482, DateTimeKind.Local).AddTicks(3458), new DateTime(2024, 5, 27, 22, 40, 24, 482, DateTimeKind.Local).AddTicks(3466) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 5, 27, 22, 40, 24, 482, DateTimeKind.Local).AddTicks(3468), new DateTime(2024, 5, 27, 22, 40, 24, 482, DateTimeKind.Local).AddTicks(3469) });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CouponId",
                table: "Bookings",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Coupon_CouponId",
                table: "Bookings",
                column: "CouponId",
                principalTable: "Coupon",
                principalColumn: "CouponId");
        }
    }
}
