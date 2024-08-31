using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.Client.Repository;
using ShoppingStore.Model.Dtos;
using ShoppingStore.Models;
using System.Security.Claims;

namespace ShoppingStore.Controllers
{
	[Authorize]
	public class CheckoutController: Controller
	{
		private readonly ShoppingStoreService _shoppingStoreService;
		//private readonly IEmailSender _emailSender;

		public CheckoutController(ShoppingStoreService shoppingStoreService)
		{
			_shoppingStoreService = shoppingStoreService ?? throw new ArgumentNullException(nameof(shoppingStoreService));
			//_emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
		}

		public IActionResult Index()
		{
			return RedirectToAction("Index", "Cart");
		}

		public async Task<IActionResult> Checkout()
		{
			var userEmail = User.FindFirstValue("email");
			if (userEmail == null)
			{
				TempData["error"] = "User don't have email claim!";
				return RedirectToAction("Index", "Cart");
			}
			else
			{
				var orderCode = Guid.NewGuid().ToString();
				var orderItem = new OrderForCreationDto()
				{
					OrderCode = orderCode,
					UserName = userEmail,
					Status = 1,
					CreatedDate = DateTime.Now,
				};
				using var createOrderResponse = await _shoppingStoreService.CreateOrderAsync(orderItem);
				createOrderResponse.EnsureSuccessStatusCode();
				var orderDetailsCreate = new List<OrderDetailForCreationDto>();
				List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
				foreach (var cartItem in cartItems)
				{
					var orderDetails = new OrderDetailForCreationDto()
					{
						UserName = userEmail,
						OrderCode = orderCode,
						ProductId = cartItem.ProductId,
						Price = cartItem.Price,
						Quantity = cartItem.Quantity,
					};
					orderDetailsCreate.Add(orderDetails);
				}
				using var createOrderDetailsResponse = await _shoppingStoreService.CreateOrderDetailsAsync(orderDetailsCreate);
				createOrderResponse.EnsureSuccessStatusCode();
				HttpContext.Session.Remove("Cart");

				//Send mail order success
				var receiver = "demologin979@gmail.com";
				var subject = "Dang nhap tren thiet bi thanh cong!";
				var message = "Dang nhap thanh cong, trai nghiem dich vu nhe!";
				//await _emailSender.SendEmailAsync(receiver, subject, message);

				TempData["success"] = "Checkout successfully! Please wait for reviewing process";
				return RedirectToAction("Index","Cart");
			}
		}
	}
}
