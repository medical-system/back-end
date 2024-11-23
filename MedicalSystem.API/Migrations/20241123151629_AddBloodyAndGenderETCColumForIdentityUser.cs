using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MedicalSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBloodyAndGenderETCColumForIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BloodyGroup",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "096c40db-bbd6-4f29-a643-e4f5c12ab0a9", "1b153437-cb20-48b7-abe7-a55ddce20f95", false, false, "Reception", "RECEPTION" },
                    { "35caa9a9-eddd-4022-8b1f-ead30fae196d", "778589ec-2e7d-499a-8b51-96b6cfff8cd7", false, false, "Patient", "PATIENT" },
                    { "773f4c1b-da3f-40f3-bd41-08338332eab9", "c93c54e0-6f99-4d6e-8a4f-b9e2a3208163", false, false, "Doctor", "DOCTOR" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                columns: new[] { "Age", "BloodyGroup", "CreatedAt", "FullName", "Password", "PasswordHash" },
                values: new object[] { 0, "", new DateTime(2024, 11, 23, 15, 16, 28, 805, DateTimeKind.Utc).AddTicks(6085), "Medical-System-Admin", "P@ssword123", "AQAAAAIAAYagAAAAEDfLVonCvVY6tsJ98WADT9dat+PIYZkbMCwaY+ito+slahWJDuqUd/IXJkiEW5BK6Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "096c40db-bbd6-4f29-a643-e4f5c12ab0a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35caa9a9-eddd-4022-8b1f-ead30fae196d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "773f4c1b-da3f-40f3-bd41-08338332eab9");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BloodyGroup",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                columns: new[] { "FullName", "Password", "PasswordHash" },
                values: new object[] { "", "", "AQAAAAIAAYagAAAAEAQ2VfOOXQ5fQxq8/YcNg6LL8nhpi2/IgrHVe9eJ4/a45NcHXIunrmq9hTEUtQqUnw==" });
        }
    }
}
