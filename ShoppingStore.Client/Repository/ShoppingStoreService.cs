using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using ShoppingStore.Model.Dtos;
using ShoppingStore.Models;
using System.Net.Http.Json;

namespace ShoppingStore.Client.Repository
{
	public class ShoppingStoreService
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration Configuration;

		public ShoppingStoreService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			Configuration = configuration;

			_httpClient.BaseAddress = new Uri(Configuration["ShoppingStoreAPIRoot"]);
			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
		}

		// Products API
		public async Task<IEnumerable<ProductDto>?> GetProductsAsync(string? searchTerm) =>
			await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"/api/products?searchTerm={searchTerm}");
		public async Task<HttpResponseMessage> GetProductsByBrandSlug(string? slug) =>
			 await _httpClient.GetAsync("/api/products/productByBrandSlug?slug=" + slug);
		public async Task<IEnumerable<ProductDto>?> GetProductsByCategorySlugAsync(string? slug) =>
			 await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("/api/products/productByCategorySlug?slug=" + slug);
		public async Task<ProductDto?> GetProductByProductSlugAsync(string? slug) =>
			 await _httpClient.GetFromJsonAsync<ProductDto>($"/api/products/productSlug?slug={slug}");
		public async Task<ProductDto?> GetProductByIdAsync(Guid productId) =>
			 await _httpClient.GetFromJsonAsync<ProductDto>($"/api/products/{productId}");

		public async Task<HttpResponseMessage> CreateProductAsync(dynamic productContent)
			=> await _httpClient.PostAsync($"/api/products/", productContent);

		public async Task<HttpResponseMessage> UpdateProductAsync(Guid productId, dynamic productContent)
			=> await _httpClient.PutAsync($"/api/products/{productId}", productContent);

		public async Task<HttpResponseMessage> DeleteProductAsync(Guid productId)
			=> await _httpClient.DeleteAsync($"/api/products/{productId}");

		// Categories API
		public async Task<IEnumerable<CategoryDto>?> GetCategoriesAsync() =>
			await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDto>>("/api/categories/");

		public async Task<CategoryDto?> GetCategoryByIdAsync(Guid categoryId) =>
			 await _httpClient.GetFromJsonAsync<CategoryDto>($"/api/categories/{categoryId}");

		public async Task<HttpResponseMessage> CreateCategoryAsync(CategoryForCreationDto category)
			=> await _httpClient.PostAsJsonAsync($"/api/categories/", category);

		public async Task<HttpResponseMessage> UpdateCategoryAsync(Guid categoryId, CategoryForEditDto category)
			=> await _httpClient.PutAsJsonAsync($"/api/categories/{categoryId}", category);

		public async Task<HttpResponseMessage> DeleteCategoryAsync(Guid categoryId)
			=> await _httpClient.DeleteAsync($"/api/categories/{categoryId}");

		// Brands API
		public async Task<IEnumerable<BrandDto>?> GetBrandsAsync() =>
			await _httpClient.GetFromJsonAsync<IEnumerable<BrandDto>>("/api/brands/");

		public async Task<BrandDto?> GetBrandByIdAsync(Guid brandId) =>
			await _httpClient.GetFromJsonAsync<BrandDto>($"/api/brands/{brandId}");

		public async Task<HttpResponseMessage> CreateBrandAsync(BrandForCreationDto brand)
			=> await _httpClient.PostAsJsonAsync($"/api/brands/", brand);

		public async Task<HttpResponseMessage> UpdateBrandAsync(Guid brandId, BrandForEditDto brand)
			=> await _httpClient.PutAsJsonAsync($"/api/brands/{brandId}", brand);

		public async Task<HttpResponseMessage> DeleteBrandAsync(Guid brandId)
			=> await _httpClient.DeleteAsync($"/api/brands/{brandId}");

		// Roles API
		public async Task<IEnumerable<RoleDto>?> GetRolesAsync() =>
			await _httpClient.GetFromJsonAsync<IEnumerable<RoleDto>>("/api/roles/");
		public async Task<RoleDto?> GetRoleByIdAsync(Guid roleId) =>
			await _httpClient.GetFromJsonAsync<RoleDto>($"/api/roles/{roleId}");
		public async Task<HttpResponseMessage> CreateRoleAsync(RoleForCreationDto role)
			=> await _httpClient.PostAsJsonAsync($"/api/roles/", role);

		public async Task<HttpResponseMessage> UpdateRoleAsync(Guid roleId, RoleForEditDto role)
			=> await _httpClient.PutAsJsonAsync($"/api/roles/{roleId}", role);

		public async Task<HttpResponseMessage> DeleteRoleAsync(Guid roleId)
			=> await _httpClient.DeleteAsync($"/api/roles/{roleId}");

		// Users API
		public async Task<IEnumerable<UserDto>?> GetUsersAsync() =>
			await _httpClient.GetFromJsonAsync<IEnumerable<UserDto>>("/api/users/");

        public async Task<UserDto?> GetUserByIdAsync(Guid userId) =>
			await _httpClient.GetFromJsonAsync<UserDto>($"/api/users/{userId}");
        public async Task<UserDto?> GetUserByIdAsync(Guid userId, string? claimType) =>
			await _httpClient.GetFromJsonAsync<UserDto>($"/api/users/{userId}?type={claimType}");
		public async Task<HttpResponseMessage> CreateUserAsync(UserForCreationDto user)
			=> await _httpClient.PostAsJsonAsync($"/api/users/", user);

		public async Task<HttpResponseMessage> ActiveUserAsync(string statusCode)
			 => await _httpClient.PostAsJsonAsync($"/api/users/activeAccount?statusCode={statusCode}", statusCode);

        public async Task<HttpResponseMessage> UpdateUserAsync(Guid userId, UserForEditDto user)
			 => await _httpClient.PutAsJsonAsync($"/api/users/{userId}", user);

        public async Task<HttpResponseMessage> DeleteUserAsync(Guid userId)
            => await _httpClient.DeleteAsync($"/api/users/{userId}");

		//Ratings API
		public async Task<HttpResponseMessage> CreateRatingAsync(RatingForCreationDto rating)
			=> await _httpClient.PostAsJsonAsync($"/api/ratings/", rating);

		//Orders API
		public async Task<IEnumerable<OrderDto>?> GetOrdersAsync() =>
			await _httpClient.GetFromJsonAsync<IEnumerable<OrderDto>>("/api/orders/");

		public async Task<OrderDto?> GetOrderByOrderCodeAsync(string orderCode) =>
			await _httpClient.GetFromJsonAsync<OrderDto>($"/api/orders/orderByOrderCode?orderCode={orderCode}");
		public async Task<HttpResponseMessage> CreateOrderAsync(OrderForCreationDto order)
			=> await _httpClient.PostAsJsonAsync($"/api/orders/", order);

        public async Task<HttpResponseMessage> UpdateOrderAsync(string orderCode, int status)
			=> await _httpClient.PutAsJsonAsync($"/api/orders", new { orderCode = orderCode, status = status });

        public async Task<HttpResponseMessage> DeleteOrderAsync(Guid orderId)
			=> await _httpClient.DeleteAsync($"/api/orders/{orderId}");

        //OrderDetail API
        public async Task<IEnumerable<OrderDetailDto>?> GetOrderDetailsByOrderCodeAsync(string orderCode) =>
			await _httpClient.GetFromJsonAsync<IEnumerable<OrderDetailDto>>($"/api/orders/orderDetails?orderCode={orderCode}");

		public async Task<HttpResponseMessage> CreateOrderDetailsAsync(IEnumerable<OrderDetailForCreationDto> orderDetails)
			=> await _httpClient.PostAsJsonAsync($"/api/orders/orderDetails", orderDetails);
	}
}
