using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoppingStore.API.Migrations.ShoppingStoreMigrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

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
                    { new Guid("90ba2b80-8a65-44e3-a8f5-771b475f1487"), "Apple is large brand in the world", "Apple", "apple", 1 },
                    { new Guid("a4fc0aea-eae4-46c6-8c44-8f9955b2f2a5"), "Samsung is large brand in the world", "Samsung", "samsung", 1 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "Slug", "Status" },
                values: new object[,]
                {
                    { new Guid("2cddf569-8327-4cbe-9041-b3dfd7ef8439"), "Macbook is large Product in the world", "Macbook", "macbook", 1 },
                    { new Guid("3cb3329d-0a73-4980-9f7d-261edd70daba"), "Pc is large Product in the world", "Pc", "pc", 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "Image", "Name", "Price", "Slug" },
                values: new object[,]
                {
                    { new Guid("5f52613a-71ce-49c5-bb37-9bff3786a7dc"), new Guid("a4fc0aea-eae4-46c6-8c44-8f9955b2f2a5"), new Guid("3cb3329d-0a73-4980-9f7d-261edd70daba"), "Pc is the Best", "1.jpg", "Pc", 1233m, "pc" },
                    { new Guid("b30a01e9-def6-4620-86b2-97fb3e076d6f"), new Guid("90ba2b80-8a65-44e3-a8f5-771b475f1487"), new Guid("2cddf569-8327-4cbe-9041-b3dfd7ef8439"), "Macbook is the Best", "1.jpg", "Macbook", 1233m, "macbook" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5f52613a-71ce-49c5-bb37-9bff3786a7dc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b30a01e9-def6-4620-86b2-97fb3e076d6f"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("90ba2b80-8a65-44e3-a8f5-771b475f1487"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("a4fc0aea-eae4-46c6-8c44-8f9955b2f2a5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2cddf569-8327-4cbe-9041-b3dfd7ef8439"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3cb3329d-0a73-4980-9f7d-261edd70daba"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
