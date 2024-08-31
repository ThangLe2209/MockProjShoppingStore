using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ShoppingStore.Client.Repository;
using ShoppingStore.Model;
using ShoppingStore.Model.Dtos;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ShoppingStore.Client.Controllers
{
	public class BrandController : Controller
	{
        private readonly ShoppingStoreService _shoppingStoreService;

        public BrandController(ShoppingStoreService shoppingStoreService)
        {
            _shoppingStoreService = shoppingStoreService;
        }
        public async Task<IActionResult> Index(string Slug = "")
        {
            //BrandModel brand = _dataContext.Brands.Where(b => b.Slug == Slug).FirstOrDefault();
            //if (brand == null) return RedirectToAction("Index", "Home");

            //var productsByBrand = _dataContext.Products.Where(p => p.BrandId == brand.Id);
            try // try catch here only execute if use httpResponseMessage.EnsureSuccessStatusCode(); to throw catch error https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage?view=net-8.0
            {
				using HttpResponseMessage httpResponseMessage = await _shoppingStoreService.GetProductsByBrandSlug(Slug);
				if (httpResponseMessage.IsSuccessStatusCode) // can use httpResponseMessage.EnsureSuccessStatusCode(); to treat statusCode as error data response
                {
					using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
					var options = new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					};
					var products = await JsonSerializer.DeserializeAsync<IEnumerable<ProductDto>>(contentStream, options);
					if (products.Count() == 0) return RedirectToAction("Index", "Home");
					return View(products);
				}
				else
				{
					var errMsg = JsonConvert.DeserializeObject<dynamic>(httpResponseMessage.Content.ReadAsStringAsync().Result);
					TempData["error"] = errMsg;
					return View(new List<ProductDto>());
				}
			}
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
				return RedirectToAction("Index", "Home");
            }
        }
    }
}
