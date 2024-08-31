using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoppingStore.API.Migrations.ShoppingStoreMigrations
{
    /// <inheritdoc />
    public partial class AddRatingOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b47faf1c-113f-4e82-9441-70b8412d62f5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("df145648-9ce7-4dbd-bddd-f7bc93baf49d"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b468b7a6-17d3-447c-8672-2737bd30988c"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("fe684fa0-1960-4f12-809d-3229e50c870e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("de6c3b25-04ba-4fb2-87a8-3a87d7542693"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f8dae822-5c25-407d-9a03-cd7afeb5de82"));

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderCode = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderCode = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Star = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "Slug", "Status" },
                values: new object[,]
                {
                    { new Guid("4f45b248-a2ce-4a85-8873-32094aba9cd3"), "Apple is large brand in the world", "Apple", "apple", 1 },
                    { new Guid("8ba10559-eaa4-4209-b73c-4d920f86ed1d"), "Samsung is large brand in the world", "Samsung", "samsung", 1 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "Slug", "Status" },
                values: new object[,]
                {
                    { new Guid("0bc61d0f-a1c7-48cf-a783-dea62b0c3d1a"), "Macbook is large Product in the world", "Macbook", "macbook", 1 },
                    { new Guid("155289e2-72be-4cb4-9153-6e9dac901c47"), "Pc is large Product in the world", "Pc", "pc", 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "Image", "Name", "Price", "Slug" },
                values: new object[,]
                {
                    { new Guid("397e7ea6-7abc-4faf-980f-102282e9664c"), new Guid("4f45b248-a2ce-4a85-8873-32094aba9cd3"), new Guid("0bc61d0f-a1c7-48cf-a783-dea62b0c3d1a"), "Macbook is the Best", "1.jpg", "Macbook", 1233m, "macbook" },
                    { new Guid("88388825-44f6-408d-b654-6917b3b7ee39"), new Guid("8ba10559-eaa4-4209-b73c-4d920f86ed1d"), new Guid("155289e2-72be-4cb4-9153-6e9dac901c47"), "Pc is the Best", "1.jpg", "Pc", 1233m, "pc" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProductId",
                table: "Ratings",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("397e7ea6-7abc-4faf-980f-102282e9664c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("88388825-44f6-408d-b654-6917b3b7ee39"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("4f45b248-a2ce-4a85-8873-32094aba9cd3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("8ba10559-eaa4-4209-b73c-4d920f86ed1d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0bc61d0f-a1c7-48cf-a783-dea62b0c3d1a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("155289e2-72be-4cb4-9153-6e9dac901c47"));

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "Slug", "Status" },
                values: new object[,]
                {
                    { new Guid("b468b7a6-17d3-447c-8672-2737bd30988c"), "Apple is large brand in the world", "Apple", "apple", 1 },
                    { new Guid("fe684fa0-1960-4f12-809d-3229e50c870e"), "Samsung is large brand in the world", "Samsung", "samsung", 1 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "Slug", "Status" },
                values: new object[,]
                {
                    { new Guid("de6c3b25-04ba-4fb2-87a8-3a87d7542693"), "Pc is large Product in the world", "Pc", "pc", 1 },
                    { new Guid("f8dae822-5c25-407d-9a03-cd7afeb5de82"), "Macbook is large Product in the world", "Macbook", "macbook", 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "Image", "Name", "Price", "Slug" },
                values: new object[,]
                {
                    { new Guid("b47faf1c-113f-4e82-9441-70b8412d62f5"), new Guid("fe684fa0-1960-4f12-809d-3229e50c870e"), new Guid("de6c3b25-04ba-4fb2-87a8-3a87d7542693"), "Pc is the Best", "1.jpg", "Pc", 1233m, "pc" },
                    { new Guid("df145648-9ce7-4dbd-bddd-f7bc93baf49d"), new Guid("b468b7a6-17d3-447c-8672-2737bd30988c"), new Guid("f8dae822-5c25-407d-9a03-cd7afeb5de82"), "Macbook is the Best", "1.jpg", "Macbook", 1233m, "macbook" }
                });
        }
    }
}
