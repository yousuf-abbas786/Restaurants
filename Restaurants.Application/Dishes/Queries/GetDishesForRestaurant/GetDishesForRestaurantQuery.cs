

using MediatR;

using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant
{
    public class GetDishesForRestaurantQuery : IRequest<IEnumerable<DishDto>>
    {
        public GetDishesForRestaurantQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public int RestaurantId { get; set; }
    }
}
