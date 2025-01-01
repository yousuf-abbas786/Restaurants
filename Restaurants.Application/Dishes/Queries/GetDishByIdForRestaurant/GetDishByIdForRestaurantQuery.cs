using MediatR;

using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQuery : IRequest<DishDto>
    {
        public GetDishByIdForRestaurantQuery(int restaurantId, int dishId)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
        }

        public int RestaurantId { get; set; }
        public int DishId { get; set; }
    }
}
