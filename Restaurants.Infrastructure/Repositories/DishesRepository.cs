
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    public class DishesRepository : IDishesRepository
    {
        private readonly RestaurantsDbContext _context;

        public DishesRepository(RestaurantsDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Dish entity)
        {
            _context.Dishes.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteForRestaurantAsync(Dish[] dishes)
        {
            _context.Dishes.RemoveRange(dishes);
            await _context.SaveChangesAsync();
        }
    }
}
