using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using ShoppingStore.Client.Models;
using ShoppingStore.Client.Repository;
using ShoppingStore.Model.Dtos;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ShoppingStore.Client.Controllers
{
    public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ShoppingStoreService _shoppingStoreService;

		public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, ShoppingStoreService shoppingStoreService)
		{
			_logger = logger;
			_httpClientFactory = httpClientFactory;
			_shoppingStoreService = shoppingStoreService;
		}

		public async Task<IActionResult> Index()
		{
            await LogIdentityInformation();

            //var httpClient = _httpClientFactory.CreateClient("APIClient");
            //var httpResponseMessage = await httpClient.GetAsync("/api/products/");


            //if (httpResponseMessage.IsSuccessStatusCode)
            //{
            //	using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            //	var options = new JsonSerializerOptions
            //	{
            //		PropertyNameCaseInsensitive = true
            //	};
            //	var products = await JsonSerializer.DeserializeAsync<IEnumerable<ProductDto>>(contentStream, options);
            //	return View(products);
            //}

            try
			{
				var products = await _shoppingStoreService.GetProductsAsync("");
				return View(products);
			}
			catch (HttpRequestException ex)
			{
				TempData["error"] = ex.StatusCode.ToString();
				return View(new List<ProductDto>());
			}
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error(int statuscode)
		{
			if (statuscode == 404)
			{
				return View("NotFound");
			}
			else
			{
				return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
			}
		}

		public async Task LogIdentityInformation()
        {
            // get the saved identity token
            var identityToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            // get the saved access token
            var accessToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            // get the refresh token
            var refreshToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            var userClaimsStringBuilder = new StringBuilder();
            foreach (var claim in User.Claims)
            {
                userClaimsStringBuilder.AppendLine(
                    $"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }

            // log token & claims
            _logger.LogInformation($"Identity token & user claims: " +
                $"\n{identityToken} \n{userClaimsStringBuilder}");
            _logger.LogInformation($"Access token: " +
                $"\n{accessToken}");
            _logger.LogInformation($"Refresh token: " +
                $"\n{refreshToken}");
        }
    }
}
