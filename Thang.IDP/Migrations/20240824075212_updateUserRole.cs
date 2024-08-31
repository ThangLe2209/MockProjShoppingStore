using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Thang.IDP.Migrations
{
    /// <inheritdoc />
    public partial class updateUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("014efdf6-f875-46d3-89f1-a6d863c16c59"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("0aef4933-a237-4154-a339-eec2f5b1ae5b"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("112810d8-20f0-48cf-9464-0baef4e299ba"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("31cb0de1-6a91-46bb-8058-c3b14d73b584"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("33006bff-809e-4dfb-9257-3236969461ae"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("4928956e-9946-42df-b9f6-85087448f8f9"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("5a4cee75-53c5-4a13-aa2f-89f9cc4c6c6a"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("96ada893-c136-4633-985c-a4aebee5da59"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserRoleId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("04283986-07a0-42f3-acc8-d5c42141d598"), "4fd25dec-a76e-4bd9-9d2b-6ec66b6adcf8", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" },
                    { new Guid("2366557f-0355-4f22-a3c7-e084ee79e4cc"), "fb825772-07a2-449f-bc35-2a2171ca5816", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" },
                    { new Guid("28f78391-3f79-4ff0-997e-e994a62dd989"), "9e67c21b-2be8-4cc6-86c2-e838b8c5c8a4", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" },
                    { new Guid("2faffde3-7606-4724-afb9-45da033ba1ea"), "2a318fcb-7fd9-4dd1-b864-3f9b8996c3db", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" },
                    { new Guid("a1099cd1-ba13-4718-89d7-0cd9feb4855d"), "871cd4bd-7d6a-4336-a6d1-3f44fcaa0999", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" },
                    { new Guid("a57e550b-b97a-4346-8192-c3b1af35c59c"), "2ad378e2-196f-4f3f-9637-c56e13121797", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" },
                    { new Guid("b1896442-ae87-4ce8-a20b-d39a615ef398"), "acf81770-cce3-46ea-8cd0-8dcd2ebe35e2", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" },
                    { new Guid("d5dff601-2cb1-4d4b-b0b1-88ef87e273a5"), "09ff8b3d-a3d2-4271-b52b-4d9b3d63352a", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Value" },
                values: new object[,]
                {
                    { new Guid("1069eee8-509a-46f9-9800-da3d0e12d560"), "f6d56efc-dcdf-4b50-b63d-8d2eccd40114", "FreeUser" },
                    { new Guid("d7ab6668-2af4-4ea4-a93b-3d96dc475d8e"), "c904cd78-91db-43ca-9299-cba64d471598", "PayingUser" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                columns: new[] { "ConcurrencyStamp", "UserRoleId" },
                values: new object[] { "8f9cca9d-7e14-435a-abf9-10d5d4d36c45", new Guid("1069eee8-509a-46f9-9800-da3d0e12d560") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                columns: new[] { "ConcurrencyStamp", "UserRoleId" },
                values: new object[] { "9cddb23c-33a5-495d-b038-3a6e6a1d3137", new Guid("d7ab6668-2af4-4ea4-a93b-3d96dc475d8e") });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRoles_UserRoleId",
                table: "Users",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_UserRoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserRoleId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("04283986-07a0-42f3-acc8-d5c42141d598"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("2366557f-0355-4f22-a3c7-e084ee79e4cc"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("28f78391-3f79-4ff0-997e-e994a62dd989"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("2faffde3-7606-4724-afb9-45da033ba1ea"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("a1099cd1-ba13-4718-89d7-0cd9feb4855d"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("a57e550b-b97a-4346-8192-c3b1af35c59c"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("b1896442-ae87-4ce8-a20b-d39a615ef398"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("d5dff601-2cb1-4d4b-b0b1-88ef87e273a5"));

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "Users");

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("014efdf6-f875-46d3-89f1-a6d863c16c59"), "c8f0698a-c936-48d7-9e47-c99bb18eb70a", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" },
                    { new Guid("0aef4933-a237-4154-a339-eec2f5b1ae5b"), "78a36b32-3739-46a8-a284-a64ae6c364fb", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" },
                    { new Guid("112810d8-20f0-48cf-9464-0baef4e299ba"), "08eeafe4-11ba-47d6-83b9-c3a999b777d6", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" },
                    { new Guid("31cb0de1-6a91-46bb-8058-c3b14d73b584"), "919f28a6-2bde-490c-8d21-0adaf1a0ae03", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" },
                    { new Guid("33006bff-809e-4dfb-9257-3236969461ae"), "de373467-4052-4669-b50c-f20d8fb6bee6", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" },
                    { new Guid("4928956e-9946-42df-b9f6-85087448f8f9"), "c394ab39-af2e-4205-b3b5-5f8a734fbcf0", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" },
                    { new Guid("5a4cee75-53c5-4a13-aa2f-89f9cc4c6c6a"), "6eea856e-4899-4f5a-8e9c-cfc6f8842eec", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" },
                    { new Guid("96ada893-c136-4633-985c-a4aebee5da59"), "754cd690-cf14-4fc8-bc31-c2f5b92e43fe", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                column: "ConcurrencyStamp",
                value: "54a28ccd-7e33-42d8-8827-43e5012729a2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                column: "ConcurrencyStamp",
                value: "0408e716-dde0-422f-a9bc-1197b81749bd");
        }
    }
}
