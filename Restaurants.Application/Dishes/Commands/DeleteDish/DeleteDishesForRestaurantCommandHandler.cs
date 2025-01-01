using MediatR;

using Microsoft.Extensions.Logging;

using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishesForRestaurantCommandHandler : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        private readonly ILogger<DeleteDishesForRestaurantCommand> _logger;
        private readonly IDishesRepository _dishesRepository;
        private readonly IRestaurantsRepository _restaurantsRepository;

        public DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommand> logger, IDishesRepository dishesRepository, IRestaurantsRepository restaurantsRepository)
        {
            _logger = logger;
            _dishesRepository = dishesRepository;
            _restaurantsRepository = restaurantsRepository;
        }

        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting all dishes for restaurant with id: {request.RestaurantId}");
            var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            await _dishesRepository.DeleteForRestaurantAsync(restaurant.Dishes.ToArray());
        }
    }
}
