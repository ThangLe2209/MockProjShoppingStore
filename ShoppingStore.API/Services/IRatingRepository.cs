using ShoppingStore.Model;
using ShoppingStore.Model.Entities;

namespace ShoppingStore.API.Services
{
    public interface IRatingRepository
	{
		Task<RatingModel> GetRatingByIdAsync(Guid Id);

		void AddRating(RatingModel rating);

        Task<bool> SaveChangesAsync();
    }
}