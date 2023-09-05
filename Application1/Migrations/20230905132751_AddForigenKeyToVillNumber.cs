using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application1.Migrations
{
    /// <inheritdoc />
    public partial class AddForigenKeyToVillNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VillasNumber",
                table: "VillasNumber");

            migrationBuilder.AddColumn<int>(
                name: "VillaNo",
                table: "VillasNumber",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VillasNumber",
                table: "VillasNumber",
                column: "VillaNo");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 5, 16, 27, 51, 413, DateTimeKind.Local).AddTicks(9023));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 5, 16, 27, 51, 413, DateTimeKind.Local).AddTicks(9060));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 5, 16, 27, 51, 413, DateTimeKind.Local).AddTicks(9064));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 5, 16, 27, 51, 413, DateTimeKind.Local).AddTicks(9067));

            migrationBuilder.CreateIndex(
                name: "IX_VillasNumber_VillaId",
                table: "VillasNumber",
                column: "VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillasNumber_Villas_VillaId",
                table: "VillasNumber",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillasNumber_Villas_VillaId",
                table: "VillasNumber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VillasNumber",
                table: "VillasNumber");

            migrationBuilder.DropIndex(
                name: "IX_VillasNumber_VillaId",
                table: "VillasNumber");

            migrationBuilder.DropColumn(
                name: "VillaNo",
                table: "VillasNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VillasNumber",
                table: "VillasNumber",
                column: "VillaId");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 5, 16, 1, 18, 609, DateTimeKind.Local).AddTicks(6833));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 5, 16, 1, 18, 609, DateTimeKind.Local).AddTicks(6861));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 5, 16, 1, 18, 609, DateTimeKind.Local).AddTicks(6863));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 5, 16, 1, 18, 609, DateTimeKind.Local).AddTicks(6865));
        }
    }
}
