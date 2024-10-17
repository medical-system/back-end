using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstNameColum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "", "AQAAAAIAAYagAAAAEHYt3FXbHSxBHry4BrBjKUDOANvJCIjw2odtzYVyeV61t8LpXyihaiO3FMyA9rznFQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEQN6wgxlfDd8AwrEjBsak25PDHosTRoI4LtTQpkLIBnpC1Q21MluoPE8Th1V6/e+g==");
        }
    }
}
