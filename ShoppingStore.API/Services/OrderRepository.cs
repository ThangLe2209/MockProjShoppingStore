using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingStore.API.DbContexts;
using ShoppingStore.Model;
using ShoppingStore.Model.Dtos;
using ShoppingStore.Model.Entities;
using System.Security.Cryptography;

namespace ShoppingStore.API.Services
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ShoppingStoreContext _context;
        public OrderRepository(ShoppingStoreContext context)
		{
			_context = context ?? throw new ArgumentException(nameof(context));
		}

		public async Task<IEnumerable<OrderModel>> GetOrdersAsync()
		{
			return await _context.Orders.ToListAsync();
		}
		public void AddOrder(OrderModel order)
		{
			_context.Orders.Add(order);
		}

		public void AddOrderDetails(IEnumerable<OrderDetails> orderDetails)
		{
			_context.OrderDetails.AddRange(orderDetails);
		}

		public async Task<OrderModel> GetOrderByIdAsync(Guid id)
		{
			return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
		}
		public async Task<OrderModel> GetOrderByOrderCodeAsync(string orderCode)
		{
			return await _context.Orders.FirstOrDefaultAsync(o => o.OrderCode == orderCode);
		}

		public async Task<IEnumerable<OrderDetails>> GetOrderDetailsByOrderCodeAsync(string orderCode)
		{
			return await _context.OrderDetails.Include(o => o.Product).Where(o => o.OrderCode == orderCode).ToListAsync();
		}

		public void DeleteOrder(OrderModel order)
		{
			_context.Orders.Remove(order);
		}

        public async Task DeleteOrderDetails(string orderCode)
        {
            var orderDetails = await _context.OrderDetails.Where(o => o.OrderCode == orderCode).ToListAsync();
            _context.OrderDetails.RemoveRange(orderDetails);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
