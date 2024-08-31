using Microsoft.AspNetCore.Mvc;
using ShoppingStore.Models;

namespace ShoppingStore.Client.Repository.Components
{
	public class HeadersViewComponent : ViewComponent
	{
		private readonly ShoppingStoreService _shoppingStoreService;

		public HeadersViewComponent()
		{
		}

		public IViewComponentResult Invoke()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			return View(cartItems.Count());
		}
	}
}
