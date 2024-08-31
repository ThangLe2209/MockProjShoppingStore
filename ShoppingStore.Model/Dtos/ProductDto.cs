namespace ShoppingStore.Model.Dtos
{
	public class ProductDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;

		public string? Slug { get; set; }
		public string? Description { get; set; }

		public decimal Price { get; set; }

		public Guid? CategoryId { get; set; }

		public CategoryModel? Category { get; set; }

		public Guid? BrandId { get; set; }

		public BrandModel? Brand { get; set; }

        //public RatingDto Ratings { get; set; }

        public string Image { get; set; } = "noimage.jpg";

		public ICollection<RatingDto> Ratings { get; set; } = new List<RatingDto>();

	}
}
