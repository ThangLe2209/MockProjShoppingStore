using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.API.Services;
using ShoppingStore.Model;
using ShoppingStore.Model.Dtos;

namespace ShoppingStore.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BrandsController : Controller
	{
		private readonly IBrandRepository _brandRepository;
		private readonly IMapper _mapper;
		public BrandsController(IBrandRepository brandRepository, IMapper mapper)
		{
			_brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
			this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
		{
			var brandEntities = await _brandRepository.GetBrandsAsync();
			var result = _mapper.Map<IEnumerable<BrandDto>>(brandEntities);
			return Ok(result);

		}

        [HttpGet("{brandId}", Name = "GetBrandById")]
        public async Task<IActionResult> GetBrand(Guid brandId)
        {
            var brand = await _brandRepository.GetBrandById(brandId);
            if (brand == null)
            {
                return NotFound("Brand Not Found");
            }

            return Ok(_mapper.Map<BrandDto>(brand));

        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] BrandForCreationDto brand) // because data create is complex type (PointOfInterestForCreationDto) so by using [ApiController] in line 8 system will automatically know this data from body instead we must specify it like this [FromBody] PointOfInterestForCreationDto pointOfInterest
        {
            brand.Slug = brand.Name.Replace(" ", "-");
            var checkSlug = await _brandRepository.GetBrandBySlug(brand.Slug);

            if (checkSlug != null)
            {
                return BadRequest("Brand already exist in database");
            }

            var finalBrand = _mapper.Map<BrandModel>(brand);

            _brandRepository.AddBrand(finalBrand);

            await _brandRepository.SaveChangesAsync(); // after this line execute we will have new Id, foregin key data for variable finalPointOfInterest which auto generated from database (can set breakpoint at line 75, 95, 99 to see) and also update to database
            var createdBrandToReturn = _mapper.Map<BrandDto>(finalBrand);
            return CreatedAtRoute("GetBrandById", // Name of Api Get from line 55 - to set location header in postman when we successfully created - location header will be api get in line 24 Ex: view cap1 image in folder 04 
                new
                {
                    brandId = createdBrandToReturn.Id,
                } // value API Get line 24 need - Api get specific pointOfInterest
                , createdBrandToReturn); // final Data (include in response body)
        }

        [HttpPut("{brandId}")]
        public async Task<ActionResult> UpdateBrand(Guid brandId, [FromBody] BrandForEditDto updatedBrand)
        {
            updatedBrand.Slug = updatedBrand.Name.Replace(" ", "-");
            BrandModel currentBrand = await _brandRepository.GetBrandById(brandId);
            if (currentBrand == null)
            {
                //_logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound("Brand not existed");
            }

            var brandWithSlug = await _brandRepository.GetBrandBySlug(updatedBrand.Slug);
            if (brandWithSlug != null && brandWithSlug.Id != brandId)
            {
                return BadRequest("Brand already existed in database");
            }

            _mapper.Map(updatedBrand, currentBrand); // source, dest => use mapper like this will override data from source to dest
            await _brandRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{brandId}")]
        public async Task<ActionResult> DeleteBrand(Guid brandId)
        {
            try
            {
                BrandModel currentBrand = await _brandRepository.GetBrandById(brandId);
                if (currentBrand == null)
                {
                    //_logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound("Brand not existed");
                }

                _brandRepository.DeleteBrand(currentBrand);
                await _brandRepository.SaveChangesAsync();

                //_mailService.Send("Point of interest deleted.",
                //    $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.ToString().Contains("FOREIGN KEY constraint"))
                {
                    return BadRequest("Delete Product first!");
                }
                return BadRequest(ex.GetType().Name);
            }
        }
    }
}
