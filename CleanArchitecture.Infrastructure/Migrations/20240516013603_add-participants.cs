using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addparticipants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultCapacity",
                table: "VillaNumbers");

            migrationBuilder.DropColumn(
                name: "ChildCapacity",
                table: "VillaNumbers");

            migrationBuilder.AddColumn<int>(
                name: "Participants",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "EndingDate", "StartingDate" },
                values: new object[] { new DateTime(2024, 6, 15, 8, 36, 3, 419, DateTimeKind.Local).AddTicks(4918), new DateTime(2024, 5, 16, 8, 36, 3, 419, DateTimeKind.Local).AddTicks(4907) });

            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "CouponId",
                keyValue: 2,
                columns: new[] { "EndingDate", "StartingDate" },
                values: new object[] { new DateTime(2024, 5, 26, 8, 36, 3, 419, DateTimeKind.Local).AddTicks(4927), new DateTime(2024, 5, 16, 8, 36, 3, 419, DateTimeKind.Local).AddTicks(4926) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 5, 16, 8, 36, 3, 419, DateTimeKind.Local).AddTicks(6770), new DateTime(2024, 5, 16, 8, 36, 3, 419, DateTimeKind.Local).AddTicks(6774) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 5, 16, 8, 36, 3, 419, DateTimeKind.Local).AddTicks(6775), new DateTime(2024, 5, 16, 8, 36, 3, 419, DateTimeKind.Local).AddTicks(6776) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Participants",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "AdultCapacity",
                table: "VillaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChildCapacity",
                table: "VillaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 101,
                columns: new[] { "AdultCapacity", "ChildCapacity" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 102,
                columns: new[] { "AdultCapacity", "ChildCapacity" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 103,
                columns: new[] { "AdultCapacity", "ChildCapacity" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 104,
                columns: new[] { "AdultCapacity", "ChildCapacity" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 105,
                columns: new[] { "AdultCapacity", "ChildCapacity" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 106,
                columns: new[] { "AdultCapacity", "ChildCapacity" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 107,
                columns: new[] { "AdultCapacity", "ChildCapacity" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 108,
                columns: new[] { "AdultCapacity", "ChildCapacity" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 109,
                columns: new[] { "AdultCapacity", "ChildCapacity" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 110,
                columns: new[] { "AdultCapacity", "ChildCapacity" },
                values: new object[] { 0, 0 });
        }
    }
}
