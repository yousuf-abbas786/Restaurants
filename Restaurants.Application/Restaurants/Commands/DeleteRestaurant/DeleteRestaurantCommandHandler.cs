using MediatR;

using Microsoft.Extensions.Logging;

using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;


namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _repository;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

        public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantsRepository repository, IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            _logger = logger;
            _repository = repository;
            _restaurantAuthorizationService = restaurantAuthorizationService;
        }

        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting restaurant with id: {request.Id}");
            var restaurant = await _repository.GetByIdAsync(request.Id );
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete)) throw new ForbiddenException();

            await _repository.DeleteAsync(restaurant);

        }
    }
}
