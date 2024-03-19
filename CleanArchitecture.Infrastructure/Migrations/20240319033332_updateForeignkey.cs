using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateForeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Villas",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                column: "CityId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CityId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CityId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Villas_CityId",
                table: "Villas",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Villas_Cities_CityId",
                table: "Villas",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Villas_Cities_CityId",
                table: "Villas");

            migrationBuilder.DropIndex(
                name: "IX_Villas_CityId",
                table: "Villas");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Villas");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 10, 29, 6, 222, DateTimeKind.Local).AddTicks(9961), new DateTime(2024, 3, 19, 10, 29, 6, 222, DateTimeKind.Local).AddTicks(9973) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 10, 29, 6, 222, DateTimeKind.Local).AddTicks(9974), new DateTime(2024, 3, 19, 10, 29, 6, 222, DateTimeKind.Local).AddTicks(9975) });
        }
    }
}
