using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMissedTableAndFixEnumsSaving : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                columns: new[] { "CreatedAt", "Gender", "PasswordHash" },
                values: new object[] { new DateTime(2025, 2, 24, 18, 4, 53, 615, DateTimeKind.Utc).AddTicks(5551), "", "AQAAAAIAAYagAAAAEP7TciUGQCzXwtcwuPLBMrJ71TfLN5EJeda+Yqzh7sestBW+dxJrOyx7IdjQ/5sqtw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 12, 12, 13, 55, 5, 45, DateTimeKind.Utc).AddTicks(1158), "AQAAAAIAAYagAAAAEFEkFtgdGzCAFTCO5Ix5YF8+q4ojolWnDmRbnlh1jzokXGOO/mno8UYPcXN3Df3UZQ==" });
        }
    }
}
