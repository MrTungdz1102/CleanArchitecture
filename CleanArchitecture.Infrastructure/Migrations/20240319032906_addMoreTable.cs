using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addMoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StartRating",
                table: "Villas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostOffice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryName", "Name", "PostOffice" },
                values: new object[,]
                {
                    { 1, "Việt Nam", "Hà Nội", "100000" },
                    { 2, "Việt Nam", "Hải Phòng", "01234" },
                    { 3, "Việt Nam", "Hội An", "51000" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CreatedAt", "ModifiedAt", "Rating", "UserId", "VillaId" },
                values: new object[,]
                {
                    { 1, "Rất vui được ở lại ! Các chủ nhà đã chào đón", new DateTime(2024, 3, 19, 10, 29, 6, 222, DateTimeKind.Local).AddTicks(9961), new DateTime(2024, 3, 19, 10, 29, 6, 222, DateTimeKind.Local).AddTicks(9973), 5, "3bb437a2-da65-4b7e-bf85-e24bc6052031", 1 },
                    { 2, "Thanks for serving our!", new DateTime(2024, 3, 19, 10, 29, 6, 222, DateTimeKind.Local).AddTicks(9974), new DateTime(2024, 3, 19, 10, 29, 6, 222, DateTimeKind.Local).AddTicks(9975), 4, "6af96a07-d096-46a8-aec8-9aa8566617bd", 1 }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_VillaId",
                table: "Reviews",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Villas");

            migrationBuilder.DropColumn(
                name: "StartRating",
                table: "Villas");

            migrationBuilder.DropColumn(
                name: "AdultCapacity",
                table: "VillaNumbers");

            migrationBuilder.DropColumn(
                name: "ChildCapacity",
                table: "VillaNumbers");
        }
    }
}
