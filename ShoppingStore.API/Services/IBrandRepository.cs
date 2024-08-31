using ShoppingStore.Model;

namespace ShoppingStore.API.Services
{
	public interface IBrandRepository
	{
		Task<IEnumerable<BrandModel>> GetBrandsAsync();

        void AddBrand(BrandModel brand);

        void DeleteBrand(BrandModel brand);

        Task<BrandModel> GetBrandById(Guid brandId);
        Task<BrandModel> GetBrandBySlug(string? slug);

        Task<bool> SaveChangesAsync();
    }
}
