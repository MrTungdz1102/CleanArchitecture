using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_coupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new DateTime(2024, 6, 13, 17, 5, 50, 217, DateTimeKind.Local).AddTicks(4677), new DateTime(2024, 5, 14, 17, 5, 50, 217, DateTimeKind.Local).AddTicks(4664) });

            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "CouponId",
                keyValue: 2,
                columns: new[] { "EndingDate", "StartingDate" },
                values: new object[] { new DateTime(2024, 5, 24, 17, 5, 50, 217, DateTimeKind.Local).AddTicks(4683), new DateTime(2024, 5, 14, 17, 5, 50, 217, DateTimeKind.Local).AddTicks(4683) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 5, 14, 17, 5, 50, 217, DateTimeKind.Local).AddTicks(6546), new DateTime(2024, 5, 14, 17, 5, 50, 217, DateTimeKind.Local).AddTicks(6551) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 5, 14, 17, 5, 50, 217, DateTimeKind.Local).AddTicks(6552), new DateTime(2024, 5, 14, 17, 5, 50, 217, DateTimeKind.Local).AddTicks(6553) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "EndingDate", "StartingDate" },
                values: new object[] { new DateTime(2024, 6, 5, 16, 43, 12, 336, DateTimeKind.Local).AddTicks(4911), new DateTime(2024, 5, 6, 16, 43, 12, 336, DateTimeKind.Local).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "CouponId",
                keyValue: 2,
                columns: new[] { "EndingDate", "StartingDate" },
                values: new object[] { new DateTime(2024, 5, 16, 16, 43, 12, 336, DateTimeKind.Local).AddTicks(4917), new DateTime(2024, 5, 6, 16, 43, 12, 336, DateTimeKind.Local).AddTicks(4916) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 5, 6, 16, 43, 12, 336, DateTimeKind.Local).AddTicks(6770), new DateTime(2024, 5, 6, 16, 43, 12, 336, DateTimeKind.Local).AddTicks(6774) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 5, 6, 16, 43, 12, 336, DateTimeKind.Local).AddTicks(6776), new DateTime(2024, 5, 6, 16, 43, 12, 336, DateTimeKind.Local).AddTicks(6776) });
        }
    }
}
