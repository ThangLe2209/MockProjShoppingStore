using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingStore.Model
{
	public class RatingModel
	{
		[Key]
		public Guid Id { get; set; }
		[ForeignKey("ProductId")]
		public Guid ProductId { get; set; }
		[Required(ErrorMessage = "Input Comment!")]
		public string Comment { get; set; }

		[Required(ErrorMessage = "Input Name!")]
		public string Name { get; set; }
		
		[Required(ErrorMessage = "Input Email!")]
		public string Email { get; set; }

		public string Star { get; set; }
		public ProductModel? Product { get; set; }
	}
}
