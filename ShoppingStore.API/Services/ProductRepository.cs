using Microsoft.EntityFrameworkCore;
using ShoppingStore.API.DbContexts;
using ShoppingStore.Model;

namespace ShoppingStore.API.Services
{
	public class ProductRepository: IProductRepository
	{
		private readonly ShoppingStoreContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IImageUploadService _imageUploadService;
		public ProductRepository(ShoppingStoreContext context, IWebHostEnvironment webHostEnvironment, IImageUploadService imageUploadService)
		{
			_context = context ?? throw new ArgumentException(nameof(context));
			_webHostEnvironment = webHostEnvironment ?? throw new ArgumentException(nameof(webHostEnvironment));
			_imageUploadService = imageUploadService ?? throw new ArgumentException(nameof(imageUploadService));
		}

		public async Task<ProductModel?> GetProductAsync(Guid productId)
        {
            return await _context.Products.Include(p => p.Ratings).Include("Category").Include("Brand").FirstOrDefaultAsync(p => p.Id.CompareTo(productId) == 0);
        }

		public async Task<ProductModel?> GetProductBySlugAsync(string? slug)
		{
			return await _context.Products.FirstOrDefaultAsync(p => p.Slug == slug);
		}

		public async Task<IEnumerable<ProductModel>> GetProductsAsync()
		{
			return await _context.Products.Include("Category").Include("Brand").ToListAsync();
		}
		public async Task<IEnumerable<ProductModel>> GetProductsAsync(string? searchTerm)
		{
            if (searchTerm == null) return await GetProductsAsync();
            var products = await _context.Products
                .Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()) 
                || p.Description.ToLower().Contains(searchTerm.ToLower()))
				.Include(p => p.Ratings).Include("Category").Include("Brand")
                .ToListAsync();
            return products;
		}

        public async Task<IEnumerable<ProductModel>> GetProductsByBrandSlugAsync(string? slug)
        {
            BrandModel brand = _context.Brands.Where(b => b.Slug == slug).FirstOrDefault();
            if (brand == null) return new List<ProductModel>();
            return await _context.Products.Where(p => p.BrandId == brand.Id).Include("Category").Include("Brand").ToListAsync();
        }  
        public async Task<IEnumerable<ProductModel>> GetProductsByCategorySlugAsync(string? slug)
        {
            CategoryModel category = _context.Categories.Where(c => c.Slug == slug).FirstOrDefault();
            if (category == null) return new List<ProductModel>();
            return await _context.Products.Where(p => p.CategoryId == category.Id).Include("Category").Include("Brand").ToListAsync();
        }

        public async Task AddProductAsync(ProductModel product)
        {
            if (product.ImageUpload != null)
            {
				//string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
				//string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
				//string filePath = Path.Combine(uploadsDir, imageName);
				//FileStream fs = new FileStream(filePath, FileMode.Create);
				//await product.ImageUpload.CopyToAsync(fs); // copy image fo file by mode create
				//fs.Close();
				//product.Image = imageName;
				var urlResponse = await _imageUploadService.Upload(product.ImageUpload);
				string toBeSearched = "upload/";
				product.Image = urlResponse.Substring(urlResponse.IndexOf(toBeSearched) + toBeSearched.Length);
			}
            _context.Products.Add(product);
        }

        public void DeleteProduct(ProductModel product)
        {
            _context.Products.Remove(product);
        }

        public void DeleteProductImage(string productImagePath)
        {
            if (!string.Equals(productImagePath, "noname.jpg"))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                string oldfileImage = Path.Combine(uploadsDir, productImagePath);
                if (System.IO.File.Exists(oldfileImage))
                {
                    System.IO.File.Delete(oldfileImage);
                }
            }
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
