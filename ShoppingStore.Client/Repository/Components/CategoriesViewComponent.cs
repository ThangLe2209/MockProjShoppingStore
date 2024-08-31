using Microsoft.AspNetCore.Mvc;
using ShoppingStore.Client.Controllers;
using ShoppingStore.Client.Repository;
namespace ShoppingStore.Client.Repository.Components
{
	public class CategoriesViewComponent: ViewComponent
	{
		private readonly ShoppingStoreService _shoppingStoreService;
		public CategoriesViewComponent(ShoppingStoreService shoppingStoreService)
		{
			_shoppingStoreService = shoppingStoreService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = await _shoppingStoreService.GetCategoriesAsync();
			return View(categories);
		}
	}
}
