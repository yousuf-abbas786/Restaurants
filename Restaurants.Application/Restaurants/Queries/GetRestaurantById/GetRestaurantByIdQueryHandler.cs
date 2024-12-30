using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
    {
        private readonly ILogger<GetRestaurantByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantsRepository _repository;

        public GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger, IMapper mapper, IRestaurantsRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting restaurant by Id: {request.Id}");
            var restaurant = await _repository.GetByIdAsync(request.Id);

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }
    
    }
}
