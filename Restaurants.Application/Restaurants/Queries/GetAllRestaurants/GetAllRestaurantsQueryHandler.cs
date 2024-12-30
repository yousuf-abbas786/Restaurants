using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        private readonly ILogger<GetAllRestaurantsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantsRepository _repository;

        public GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantsRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all restaurants");
            var restaurants = await _repository.GetAllAsync();
            var restaurantDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            return restaurantDto!;
        }
    }
}
