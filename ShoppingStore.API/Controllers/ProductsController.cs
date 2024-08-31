using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.API.Services;
using ShoppingStore.Model;
using ShoppingStore.Model.Dtos;
using System.Text.Json;

namespace ShoppingStore.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
    //[Authorize]
    public class ProductsController : Controller
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;
		private readonly IImageUploadService _imageUploadService;
		private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment, IImageUploadService imageUploadService)
		{
			_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
			this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentException(nameof(webHostEnvironment));
            _imageUploadService = imageUploadService ?? throw new ArgumentException(nameof(imageUploadService));
        }

		[HttpGet]
        //[Authorize(Policy = "ClientApplicationCanWrite")]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(string? searchTerm)
		{
			var productEntities = await _productRepository.GetProductsAsync(searchTerm);
			var result = _mapper.Map<IEnumerable<ProductDto>>(productEntities);
			return Ok(result);
		}

		[HttpGet("productByBrandSlug")]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByBrandSlug([FromQuery] string? slug)
		{
			var productEntities = await _productRepository.GetProductsByBrandSlugAsync(slug);
			var result = _mapper.Map<IEnumerable<ProductDto>>(productEntities);
			return Ok(result);
			//return StatusCode(500, "result");
			//return BadRequest("haa");
		}

		[HttpGet("productByCategorySlug")]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategorySlug([FromQuery] string? slug)
		{
			var productEntities = await _productRepository.GetProductsByCategorySlugAsync(slug);
			var result = _mapper.Map<IEnumerable<ProductDto>>(productEntities);
			return Ok(result);
		}


        [HttpGet("productSlug")]
        public async Task<ActionResult<ProductDto>> GetProductBySlug([FromQuery] string? slug)
        {
            var productEntities = await _productRepository.GetProductBySlugAsync(slug);
            var result = _mapper.Map<ProductDto>(productEntities);
            return Ok(result);
        }


        [HttpGet("{productId}", Name = "GetProductById")]
		public async Task<IActionResult> GetProduct(Guid productId)
		{
			var product = await _productRepository.GetProductAsync(productId);
			if (product == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<ProductDto>(product));

		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct([FromForm] ProductForCreationDto product) // because data create is complex type (PointOfInterestForCreationDto) so by using [ApiController] in line 8 system will automatically know this data from body instead we must specify it like this [FromBody] PointOfInterestForCreationDto pointOfInterest
        {
			var checkSlug = await _productRepository.GetProductBySlugAsync(product.Slug);

			if (checkSlug != null)
			{
				return BadRequest("Product already exist in database");
			}

			var finalProduct = _mapper.Map<ProductModel>(product);

			await _productRepository.AddProductAsync(finalProduct);
			await _productRepository.SaveChangesAsync(); // after this line execute we will have new Id, foregin key data for variable finalPointOfInterest which auto generated from database (can set breakpoint at line 75, 95, 99 to see) and also update to database
			var createdProductToReturn = _mapper.Map<ProductDto>(finalProduct);
			return CreatedAtRoute("GetProductById", // Name of Api Get from line 55 - to set location header in postman when we successfully created - location header will be api get in line 24 Ex: view cap1 image in folder 04 
				new
				{
					productId = createdProductToReturn.Id,
				} // value API Get line 24 need - Api get specific pointOfInterest
				, createdProductToReturn); // final Data (include in response body)
		}

        [HttpPut("{productId}")]
        public async Task<ActionResult> UpdateProduct(Guid productId, [FromForm] ProductForEditDto updatedProduct)
        {
            ProductModel currentProduct = await _productRepository.GetProductAsync(productId);
            if (currentProduct == null)
            {
                //_logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound("Product not existed");
            }

            var productWithSlug = await _productRepository.GetProductBySlugAsync(updatedProduct.Slug);
            if (productWithSlug != null && productWithSlug.Id != productId)
            {
                return BadRequest("Product already exist in database");
            }

            if (updatedProduct.ImageUpload != null)
            {
     //           string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
     //           string imageName = Guid.NewGuid().ToString() + "_" + updatedProduct.ImageUpload.FileName;
     //           string filePath = Path.Combine(uploadsDir, imageName);
     //           FileStream fs = new FileStream(filePath, FileMode.Create);
     //           await updatedProduct.ImageUpload.CopyToAsync(fs); // copy image fo file by mode create
     //           fs.Close();

     //           //delete old picture
     //           string oldfilePath = Path.Combine(uploadsDir, currentProduct.Image);

     //           try
     //           {
     //               if (System.IO.File.Exists(oldfilePath))
     //               {
     //                   System.IO.File.Delete(oldfilePath);
     //               }
     //               currentProduct.Image = imageName;
     //           }
     //           catch (Exception ex)
     //           {
					//return BadRequest("An error occurred while deleting the product image.");
     //           }
                var urlResponse = await _imageUploadService.Upload(updatedProduct.ImageUpload);
				string toBeSearched = "upload/";
				currentProduct.Image = urlResponse.Substring(urlResponse.IndexOf(toBeSearched) + toBeSearched.Length);
            }

            _mapper.Map(updatedProduct, currentProduct); // source, dest => use mapper like this will override data from source to dest
            await _productRepository.SaveChangesAsync();
			return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct(Guid productId)
        {
            try
            {
                ProductModel currentProduct = await _productRepository.GetProductAsync(productId);
                var productImagePath = currentProduct.Image;
                if (currentProduct == null)
                {
                    //_logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound("Product not existed");
                }

                _productRepository.DeleteProduct(currentProduct);
                await _productRepository.SaveChangesAsync();
                //_productRepository.DeleteProductImage(productImagePath); // xóa image ở đây sau savechange vì bắt lỗi foreign key constraint phải xóa product thành công thì mới xóa ảnh

                //_mailService.Send("Point of interest deleted.",
                //    $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.ToString().Contains("foreign key constraint"))
                {
                    return BadRequest("Delete OrderDetail first!");
                }
                return BadRequest(ex.GetType().Name);
            }
        }
    }
}
