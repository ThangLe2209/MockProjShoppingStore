using Microsoft.EntityFrameworkCore;
using ShoppingStore.API.DbContexts;
using ShoppingStore.Model;
using ShoppingStore.Model.Entities;

namespace ShoppingStore.API.Services
{
	public class RatingRepository : IRatingRepository
	{
		private readonly ShoppingStoreContext _context;
		public RatingRepository(ShoppingStoreContext context)
		{
			_context = context ?? throw new ArgumentException(nameof(context));
		}

		public async Task<RatingModel> GetRatingByIdAsync(Guid Id)
		{
			return await _context.Ratings.FirstOrDefaultAsync(r => r.Id == Id);
		}

		public void AddRating(RatingModel rating)
		{
			_context.Ratings.Add(rating);
		}

		public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
