using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingStore.Model
{
	public class OrderDetails
	{
		public Guid Id { get; set; }
		public string OrderCode { get; set; }
		public string UserName { get; set; }

		[ForeignKey("ProductId")]
		public Guid ProductId { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }

		public ProductModel Product { get; set; }
	}
}
