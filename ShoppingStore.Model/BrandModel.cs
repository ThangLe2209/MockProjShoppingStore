using System.ComponentModel.DataAnnotations;

namespace ShoppingStore.Model
{
	public class BrandModel
	{
		[Key]
		public Guid Id { get; set; }

		[Required, MinLength(4, ErrorMessage = "Input brand name")]
		public required string Name { get; set; }

		public string? Description { get; set; }

		public string? Slug { get; set; }

		public int Status { get; set; }

		//public ProductModel? Product { get; set; }
	}
}