using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Thang.IDP.Migrations
{
    /// <inheritdoc />
    public partial class InitialThangIdentityDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SecurityCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SecurityCodeExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false),
                    UserRoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Provider = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ProviderIdentityKey = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSecrets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Secret = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSecrets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSecrets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Value" },
                values: new object[,]
                {
                    { new Guid("1069eee8-509a-46f9-9800-da3d0e12d560"), "57efa1cb-5cf7-47aa-b0b6-1e91f8556af9", "FreeUser" },
                    { new Guid("d7ab6668-2af4-4ea4-a93b-3d96dc475d8e"), "3b45d37e-0fbe-488c-89ef-1289a793f5b1", "PayingUser" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Email", "Password", "SecurityCode", "SecurityCodeExpirationDate", "Subject", "UserName", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), true, "3c60e77a-719d-476c-871d-dbe11688bae7", "david@someprovider.com", "AQAAAAIAAYagAAAAEIGgD0IAkW+5XFBUko8X0PnlkVWcWfaJv2mYU2mACHEZ5ilrceWrxHYBreWHKwRfYw==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d860efca-22d9-47fd-8249-791ba61b07c7", "David", new Guid("1069eee8-509a-46f9-9800-da3d0e12d560") },
                    { new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), true, "2956b00a-d52b-4f31-b166-4cd1542e173e", "emma@someprovider.com", "AQAAAAIAAYagAAAAEIGgD0IAkW+5XFBUko8X0PnlkVWcWfaJv2mYU2mACHEZ5ilrceWrxHYBreWHKwRfYw==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b7539694-97e7-4dfe-84da-b4256e1ff5c7", "Emma", new Guid("d7ab6668-2af4-4ea4-a93b-3d96dc475d8e") }
                });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("2abb8a12-ddc0-464c-9f8c-329262cadda2"), "cdc91ecf-3d41-4cd6-8067-b10b9e156d32", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" },
                    { new Guid("3fab1468-f040-42a5-a024-31f8c906096c"), "d9738725-3bfa-47e8-809e-efd1c7c9a1ab", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" },
                    { new Guid("45e8a317-a1df-4f55-baa0-d286f3f22b9c"), "c233470d-f444-4fef-8bd9-b4a8ea9d1fd8", "email", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "emma@someprovider.com" },
                    { new Guid("5d454416-9d82-416b-9e00-c9747b12d686"), "37da4e47-aa26-48c7-b847-7f2a5090e738", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" },
                    { new Guid("6488b9d2-625e-40c5-98dc-a93a0d7bc0c2"), "14c2435c-6410-47b5-aa42-4eed0ebbc2cf", "email", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "david@someprovider.com" },
                    { new Guid("81419629-0b66-4f88-ac2c-8f5a40a43727"), "184c1d14-8650-41d1-80ff-b09597a04029", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" },
                    { new Guid("982be67c-4303-4f09-81d1-c21cdaec05d3"), "bbaf2af9-3bdc-4509-acb4-458ca62a1584", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" },
                    { new Guid("a6d2a9aa-5878-4b07-a2f4-02e49894f75e"), "dab54667-b61a-4a76-9220-763187536ce8", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" },
                    { new Guid("eb5c5280-3659-4624-bec4-69a52fc2b640"), "34bc56dc-f79f-4057-8779-b9f1cc0557ee", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" },
                    { new Guid("f392c0f8-8aa6-430a-8e50-595a039cb356"), "419c3e92-9b75-49d8-bc7a-5b563f490772", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Subject",
                table: "Users",
                column: "Subject",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSecrets_UserId",
                table: "UserSecrets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserSecrets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
