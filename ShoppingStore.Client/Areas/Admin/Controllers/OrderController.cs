using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingStore.Client.Repository;

namespace ShoppingStore.Client.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize]
    public class OrderController : Controller
	{
		private readonly ShoppingStoreService _shoppingStoreService;

		public OrderController(ShoppingStoreService shoppingStoreService)
		{
			_shoppingStoreService = shoppingStoreService;
		}
		public async Task<IActionResult> Index()
		{
			return View(await _shoppingStoreService.GetOrdersAsync());
		}

		public async Task<IActionResult> ViewOrder(string ordercode)
		{
			var order = await _shoppingStoreService.GetOrderByOrderCodeAsync(ordercode);
			ViewBag.orderStatus = order?.Status;
			var orderDetails = await _shoppingStoreService.GetOrderDetailsByOrderCodeAsync(ordercode);
			return View(orderDetails);
		}

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(string ordercode, int status)
        {
            using HttpResponseMessage response = await _shoppingStoreService.UpdateOrderAsync(ordercode,status);
            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode) // can use httpResponseMessage.EnsureSuccessStatusCode(); to treat statusCode as error data response
            {
                //var responseContent = await response.Content.ReadAsStringAsync();
                //Console.WriteLine("UploadFileInModel_WithHttpClientAsync response :" + responseContent);
                TempData["success"] = "Update Order successfully";
                return Ok(new { success = true, message = "Order status updated successfully", redirectToUrl = Url.Action("Index", "Order") });
            }
            else
            {
                var errMsg = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
                TempData["error"] = errMsg;
                return StatusCode(500, new { message= errMsg, redirectToUrl = Url.Action("Index", "Order") });
                //return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            //var response1 = await _shoppingStoreService.GetCategoryByIdAsync(Id);
            //TempData["success"] = response1;
            //return View();
            using HttpResponseMessage response = await _shoppingStoreService.DeleteOrderAsync(Id);
            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode) // can use httpResponseMessage.EnsureSuccessStatusCode(); to treat statusCode as error data response
            {
                //var responseContent = await response.Content.ReadAsStringAsync();
                //Console.WriteLine("UploadFileInModel_WithHttpClientAsync response :" + responseContent);
                TempData["success"] = "Remove Order successfully";
                return RedirectToAction("Index");
            }
            else
            {
                var errMsg = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
                TempData["error"] = errMsg;
                return RedirectToAction("Index");
            }
        }
    }
}
