using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoppingStore.API.Migrations.ShoppingStoreMigrations
{
    /// <inheritdoc />
    public partial class UpdateProductRatingConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_ProductId",
                table: "Ratings");

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
                    { new Guid("9a4e82a3-c89a-4b46-a4d6-27d6cf3f7c71"), "Apple is large brand in the world", "Apple", "apple", 1 },
                    { new Guid("e04c01ba-a089-4b9b-908a-9e14c850d88c"), "Samsung is large brand in the world", "Samsung", "samsung", 1 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "Slug", "Status" },
                values: new object[,]
                {
                    { new Guid("7da31de5-0799-4016-9d74-68544779af47"), "Macbook is large Product in the world", "Macbook", "macbook", 1 },
                    { new Guid("f96b0cdd-a262-4242-8d78-a69f984ae1a4"), "Pc is large Product in the world", "Pc", "pc", 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "Image", "Name", "Price", "Slug" },
                values: new object[,]
                {
                    { new Guid("7d15e3d1-20d4-4c10-9167-f47d73677ea2"), new Guid("9a4e82a3-c89a-4b46-a4d6-27d6cf3f7c71"), new Guid("7da31de5-0799-4016-9d74-68544779af47"), "Macbook is the Best", "1.jpg", "Macbook", 1233m, "macbook" },
                    { new Guid("d49b18e7-63a3-4694-a360-c66cc79a08fd"), new Guid("e04c01ba-a089-4b9b-908a-9e14c850d88c"), new Guid("f96b0cdd-a262-4242-8d78-a69f984ae1a4"), "Pc is the Best", "1.jpg", "Pc", 1233m, "pc" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProductId",
                table: "Ratings",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_ProductId",
                table: "Ratings");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7d15e3d1-20d4-4c10-9167-f47d73677ea2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d49b18e7-63a3-4694-a360-c66cc79a08fd"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9a4e82a3-c89a-4b46-a4d6-27d6cf3f7c71"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e04c01ba-a089-4b9b-908a-9e14c850d88c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7da31de5-0799-4016-9d74-68544779af47"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f96b0cdd-a262-4242-8d78-a69f984ae1a4"));

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
                name: "IX_Ratings_ProductId",
                table: "Ratings",
                column: "ProductId",
                unique: true);
        }
    }
}
