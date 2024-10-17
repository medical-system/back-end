using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordColum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                columns: new[] { "Password", "PasswordHash" },
                values: new object[] { "", "AQAAAAIAAYagAAAAEAQ2VfOOXQ5fQxq8/YcNg6LL8nhpi2/IgrHVe9eJ4/a45NcHXIunrmq9hTEUtQqUnw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHYt3FXbHSxBHry4BrBjKUDOANvJCIjw2odtzYVyeV61t8LpXyihaiO3FMyA9rznFQ==");
        }
    }
}
