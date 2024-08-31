using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingStore.Client.Models.ViewModels;
using ShoppingStore.Client.Repository;
using ShoppingStore.Model.Dtos;
using System.Text.Json;

namespace ShoppingStore.Client.Controllers
{
	public class ProductController : Controller
	{
        private readonly ShoppingStoreService _shoppingStoreService;

        public ProductController(ShoppingStoreService shoppingStoreService)
        {
            _shoppingStoreService = shoppingStoreService;
        }

        public IActionResult Index()
		{
			return View(); 
		}

		public async Task<IActionResult> Search(string searchTerm)
		{
			var products = await _shoppingStoreService.GetProductsAsync(searchTerm);
			ViewBag.Keyword = searchTerm;
			return View(products);
		}

		public async Task<IActionResult> Details(Guid Id)
		{
			if (Id == null) return RedirectToAction("Index");
			var productsById = await _shoppingStoreService.GetProductByIdAsync(Id);

			var relatedProductsData = await _shoppingStoreService.GetProductsByCategorySlugAsync(productsById.Category.Slug);
			//related Product
			var relatedProducts = relatedProductsData
                .Where(p => p.Id != productsById.Id)
				.Take(4).ToList();
			ViewBag.RelatedProducts = relatedProducts;
			var viewModel = new ProductDetailsViewModel
			{
				ProductDetails = productsById,
				//RatingDetails = new RatingModel()
			};
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CommentProduct(RatingForCreationDto rating)
		{
			if (ModelState.IsValid)
			{
				using HttpResponseMessage httpResponseMessage = await _shoppingStoreService.CreateRatingAsync(rating);
				if (httpResponseMessage.IsSuccessStatusCode) // can use httpResponseMessage.EnsureSuccessStatusCode(); to treat statusCode as error data response
				{
					TempData["success"] = "Them danh gia thanh cong!";
					return Redirect(Request.Headers["Referer"]);
				}
				else
				{
					var errMsg = JsonConvert.DeserializeObject<dynamic>(httpResponseMessage.Content.ReadAsStringAsync().Result);
					TempData["error"] = errMsg;
					return Redirect(Request.Headers["Referer"]);
				}
			}
			else
			{
				TempData["error"] = "Model co 1 vai thu dang bi loi";
				List<string> errors = new List<string>();
				foreach (var value in ModelState.Values)
				{
					foreach (var error in value.Errors)
					{
						errors.Add(error.ErrorMessage);
					}
				}
				string errorMessage = string.Join("\n", errors);
				//return BadRequest(errorMessage);
				return RedirectToAction("Detail", new { id = rating.ProductId });
			}
		}
	}
}
