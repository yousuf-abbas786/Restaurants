using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
    {
        private readonly ILogger<CreateDishCommandHandler> _logger;
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;

        public CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository, IMapper mapper)
        {
            _logger = logger;
            _restaurantsRepository = restaurantsRepository;
            _dishesRepository = dishesRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new dish {@DishRequest}", request);

            var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = _mapper.Map<Dish>(request);
            await _dishesRepository.CreateAsync(dish);
            return dish.Id;
        }
    }
}
