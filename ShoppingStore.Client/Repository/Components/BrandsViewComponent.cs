using Microsoft.AspNetCore.Mvc;

namespace ShoppingStore.Client.Repository.Components
{
	public class BrandsViewComponent: ViewComponent
	{
		private readonly ShoppingStoreService _shoppingStoreService;

		public BrandsViewComponent(ShoppingStoreService shoppingStoreService)
		{
			_shoppingStoreService = shoppingStoreService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
			=> View(await _shoppingStoreService.GetBrandsAsync());
	}
}
