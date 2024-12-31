
using Microsoft.EntityFrameworkCore;

using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    public class RestaurantsRepository : IRestaurantsRepository
    {
        private readonly RestaurantsDbContext _context;

        public RestaurantsRepository(RestaurantsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants
                .Include(x => x.Dishes)
                .ToListAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            return await _context.Restaurants
                .Include(x => x.Dishes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> CreateAsync(Restaurant entity)
        {
            _context.Restaurants.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        public async Task UpdateAsync(Restaurant entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            _context.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}
