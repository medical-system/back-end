using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MedicalSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumDateFromMedicalRecordAndAddAuditableEntityAndServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

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

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AspNetUsers_RefreshToken",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers_RefreshToken", x => new { x.ApplicationUserId, x.Id });
                    table.ForeignKey(
                        name: "FK_AspNetUsers_RefreshToken_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Complaint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Treatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VitalSigns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalRecords_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalRecords_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InStock = table.Column<int>(type: "int", nullable: false),
                    Measure = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicess_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicess_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<int>(type: "int", nullable: false),
                    ItemPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instraction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Amout = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => new { x.MedicalRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_Prescriptions_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9eaa03df-8e4f-4161-85de-0f6e5e30bfd4",
                column: "IsDefault",
                value: true);

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
                columns: new[] { "Age", "BloodyGroup", "CreatedAt", "DoctorId", "FullName", "Gender", "ImageUrl", "Password", "PasswordHash" },
                values: new object[] { 0, "", new DateTime(2025, 2, 25, 18, 28, 20, 733, DateTimeKind.Utc).AddTicks(6350), null, "Medical-System-Admin", "", "", "P@ssword123", "AQAAAAIAAYagAAAAEKSzoFBWSWNy2AQ5XTptnr7J/HRbFE2KZ1sEUwEy6RDCv7umtT9Fjg0OxwijttMMaQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DoctorId",
                table: "AspNetUsers",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_CreatedById",
                table: "MedicalRecords",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_UpdatedById",
                table: "MedicalRecords",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Servicess_CreatedById",
                table: "Servicess",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Servicess_UpdatedById",
                table: "Servicess",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_DoctorId",
                table: "AspNetUsers",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_DoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetUsers_RefreshToken");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Servicess");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DoctorId",
                table: "AspNetUsers");

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

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.ApplicationUserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9eaa03df-8e4f-4161-85de-0f6e5e30bfd4",
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                columns: new[] { "FullName", "Password", "PasswordHash" },
                values: new object[] { "", "", "AQAAAAIAAYagAAAAEAQ2VfOOXQ5fQxq8/YcNg6LL8nhpi2/IgrHVe9eJ4/a45NcHXIunrmq9hTEUtQqUnw==" });
        }
    }
}
