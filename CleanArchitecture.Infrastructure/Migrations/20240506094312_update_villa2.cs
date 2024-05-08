using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_villa2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Villas",
                newName: "OwnerId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Villas",
                newName: "UserId");

            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "EndingDate", "StartingDate" },
                values: new object[] { new DateTime(2024, 6, 5, 16, 36, 44, 550, DateTimeKind.Local).AddTicks(7484), new DateTime(2024, 5, 6, 16, 36, 44, 550, DateTimeKind.Local).AddTicks(7469) });

            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "CouponId",
                keyValue: 2,
                columns: new[] { "EndingDate", "StartingDate" },
                values: new object[] { new DateTime(2024, 5, 16, 16, 36, 44, 550, DateTimeKind.Local).AddTicks(7490), new DateTime(2024, 5, 6, 16, 36, 44, 550, DateTimeKind.Local).AddTicks(7489) });

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
        }
    }
}
