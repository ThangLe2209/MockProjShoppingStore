using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShoppingStore.Model.Repository.Validation;
using Microsoft.AspNetCore.Http;
using ShoppingStore.Model.Dtos;

namespace ShoppingStore.Model
{
	public class ProductModel
	{
		[Key]
		public Guid Id { get; set; }

		[Required, MinLength(4, ErrorMessage = "Input Product Name")]
		public required string Name { get; set; }
		public string? Slug { get; set; }

		public string? Description { get; set; }

		[Required(ErrorMessage = "Input Product Price")]
		[Range(0.01, double.MaxValue)]
		[Column(TypeName = "decimal(8,2)")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Choose one Brand")]
		public Guid BrandId { get; set; }

		[Required(ErrorMessage = "Choose one Category")]
		public Guid CategoryId { get; set; }

		public CategoryModel? Category { get; set; }


		public BrandModel? Brand { get; set; }

		//public RatingModel Ratings { get; set; }
		public string Image { get; set; } = "noimage.jpg";

		[NotMapped] // not store to database
		[FileExtension] // Repository/Validation/FileExtensionAttribute.cs
		public IFormFile? ImageUpload { get; set; }

		public ICollection<RatingModel> Ratings { get; set; } = new List<RatingModel>();
	}
}
