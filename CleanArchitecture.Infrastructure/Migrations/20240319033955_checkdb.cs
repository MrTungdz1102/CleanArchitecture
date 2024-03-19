using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class checkdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "OwnerName", "StartRating" },
                values: new object[] { "Tung Dao Duc", 5.0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OwnerName", "StartRating" },
                values: new object[] { "Tung Dao", 4.5 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OwnerName", "StartRating" },
                values: new object[] { "Tung", 4.9000000000000004 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 10, 33, 32, 29, DateTimeKind.Local).AddTicks(1029), new DateTime(2024, 3, 19, 10, 33, 32, 29, DateTimeKind.Local).AddTicks(1047) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 10, 33, 32, 29, DateTimeKind.Local).AddTicks(1048), new DateTime(2024, 3, 19, 10, 33, 32, 29, DateTimeKind.Local).AddTicks(1049) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OwnerName", "StartRating" },
                values: new object[] { null, 0.0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OwnerName", "StartRating" },
                values: new object[] { null, 0.0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OwnerName", "StartRating" },
                values: new object[] { null, 0.0 });
        }
    }
}
