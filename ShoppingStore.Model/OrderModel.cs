namespace ShoppingStore.Model
{
	public class OrderModel
	{
		public Guid Id { get; set; }
		public string OrderCode { get; set; }
		public string UserName { get; set; }
		public DateTime CreatedDate { get; set; }
		public int Status { get; set; }
	}
}
