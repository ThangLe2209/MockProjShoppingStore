using ShoppingStore.Model;
using System.Threading.Tasks;

namespace ShoppingStore.API.Services
{
	public interface IProductRepository
	{
		Task<IEnumerable<ProductModel>> GetProductsAsync();
		Task<IEnumerable<ProductModel>> GetProductsAsync(string? searchTerm);

        Task<IEnumerable<ProductModel>> GetProductsByBrandSlugAsync(string? slug);
		Task<IEnumerable<ProductModel>> GetProductsByCategorySlugAsync(string? slug);

        Task<ProductModel?> GetProductAsync(Guid productId);
		Task<ProductModel?> GetProductBySlugAsync(string? slug);

        Task AddProductAsync(ProductModel product);

        void DeleteProduct(ProductModel product);
        void DeleteProductImage(string productImagePath);
        Task<bool> SaveChangesAsync();
    }
}
