using ShoppingStore.Model;
using ShoppingStore.Model.Entities;

namespace ShoppingStore.API.Services
{
    public interface IOrderRepository
	{
		Task<IEnumerable<OrderModel>> GetOrdersAsync();
		void AddOrder(OrderModel order);

		void AddOrderDetails(IEnumerable<OrderDetails> orderDetails);

		Task<OrderModel> GetOrderByIdAsync(Guid id);
		Task<OrderModel> GetOrderByOrderCodeAsync(string orderCode);

		Task<IEnumerable<OrderDetails>> GetOrderDetailsByOrderCodeAsync(string orderCode);
		void DeleteOrder(OrderModel order);

		Task DeleteOrderDetails(string orderCode);
        Task<bool> SaveChangesAsync();
    }
}