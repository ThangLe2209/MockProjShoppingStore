using Microsoft.AspNetCore.Mvc;
using ShoppingStore.Client.Repository;

namespace ShoppingStore.Client.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ShoppingStoreService _shoppingStoreService;

        public CategoryController(ShoppingStoreService shoppingStoreService)
        {
            _shoppingStoreService = shoppingStoreService;
        }
        public async Task<IActionResult> Index(string Slug = "")
        {
            //BrandModel brand = _dataContext.Brands.Where(b => b.Slug == Slug).FirstOrDefault();
            //if (brand == null) return RedirectToAction("Index", "Home");

            //var productsByBrand = _dataContext.Products.Where(p => p.BrandId == brand.Id);
            var result = await _shoppingStoreService.GetProductsByCategorySlugAsync(Slug);
            if (result.Count() == 0) return RedirectToAction("Index", "Home");
            return View(result);
        }
    }
}
