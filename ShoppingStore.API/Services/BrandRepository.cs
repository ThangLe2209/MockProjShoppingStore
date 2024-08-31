using Microsoft.EntityFrameworkCore;
using ShoppingStore.API.DbContexts;
using ShoppingStore.Model;

namespace ShoppingStore.API.Services
{
	public class BrandRepository : IBrandRepository
	{
		private readonly ShoppingStoreContext _context;
		public BrandRepository(ShoppingStoreContext context)
		{
			_context = context ?? throw new ArgumentException(nameof(context));
		}

        public void AddBrand(BrandModel brand)
        {
            _context.Brands.Add(brand);
        }

        public void DeleteBrand(BrandModel brand)
        {
            _context.Brands.Remove(brand);
        }

        public async Task<BrandModel> GetBrandById(Guid brandId)
        {
            return await _context.Brands.FirstOrDefaultAsync(b => b.Id == brandId);
        }

        public Task<BrandModel> GetBrandBySlug(string? slug)
        {
            return _context.Brands.FirstOrDefaultAsync(b => b.Slug == slug);
        }

        public async Task<IEnumerable<BrandModel>> GetBrandsAsync()
		{
			return await _context.Brands.ToListAsync();
		}

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
