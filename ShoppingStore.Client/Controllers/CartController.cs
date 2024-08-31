using Microsoft.AspNetCore.Mvc;
using ShoppingStore.Client.Models.ViewModels;
using ShoppingStore.Client.Repository;
using ShoppingStore.Model.Dtos;
using ShoppingStore.Models;

namespace ShoppingStore.Client.Controllers
{
	public class CartController : Controller
	{
        private readonly ShoppingStoreService _shoppingStoreService;

        public CartController(ShoppingStoreService shoppingStoreService)
        {
            _shoppingStoreService = shoppingStoreService;
        }
        public IActionResult Index()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemViewModel cartVM = new()
			{
				CartItems = cartItems,
				GrandTotal = cartItems.Sum(x => x.Quantity * x.Price),
			};
			return View(cartVM);
		}

		public IActionResult Checkout()
		{
			return View("~/Views/Checkout/Index.cshtml");
		}

		public async Task<IActionResult> Add(Guid Id)
		{
			ProductDto product = await _shoppingStoreService.GetProductByIdAsync(Id);
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItem = cart.Where(c =>c.ProductId.CompareTo(Id) == 0).FirstOrDefault();

			if(cartItem == null)
			{
				cart.Add(new CartItemModel(product));
			}
			else
			{
				cartItem.Quantity += 1;
			}

			HttpContext.Session.SetJson("Cart", cart);

			TempData["success"] = "Add item to cart successfully";
            return Redirect(Request.Headers["Referer"].ToString()); // back to previous page before access this action
		}

		public async Task<IActionResult> Decrease(Guid Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if(cartItem.Quantity > 1)
			{
				cartItem.Quantity -= 1;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId.CompareTo(Id) == 0); // Remove that product from cart
			}
			if (cart.Count == 0) 
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}

            TempData["success"] = "Decrease item quantity to cart successfully";
            return RedirectToAction("Index");
		}

		public async Task<IActionResult> Increase(Guid Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (cartItem.Quantity >= 1)
			{
				cartItem.Quantity += 1;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId.CompareTo(Id) == 0); // Remove that product from cart
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}

            TempData["success"] = "Increase item quantity to cart successfully";
            return RedirectToAction("Index");
		}

		public async Task<IActionResult> Remove(Guid Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			cart.RemoveAll(p => p.ProductId.CompareTo(Id) == 0);
			if(cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}

            TempData["success"] = "Remove item of cart successfully";
            return RedirectToAction("Index");
		}

		public async Task<IActionResult> Clear()
		{
			HttpContext.Session.Remove("Cart");

            TempData["success"] = "Clear all item from cart successfully";
            return RedirectToAction("Index");
		}

        public IActionResult ThangTestPage()
        {
            return View();
        }
    }
}
