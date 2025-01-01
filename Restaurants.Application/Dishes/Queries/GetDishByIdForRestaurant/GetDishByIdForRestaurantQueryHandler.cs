using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        private readonly ILogger<GetDishByIdForRestaurantQueryHandler> _logger;
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;
        

        public GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger, IRestaurantsRepository restaurantsRepository, IMapper mapper)
        {
            _logger = logger;
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
        }

        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving dish with id {dishId} for restaurant with id {restaurantId}", request.DishId, request.RestaurantId);

            var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
            if (dish == null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());

            var result = _mapper.Map<DishDto>(dish);

            return result;
        }
    }
}
