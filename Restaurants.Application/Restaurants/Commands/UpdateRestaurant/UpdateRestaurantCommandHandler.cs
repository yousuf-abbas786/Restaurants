using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
    {
        private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

        public UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IRestaurantsRepository repository, IMapper mapper, IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _restaurantAuthorizationService = restaurantAuthorizationService;
        }

        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating restaurant by id: {request.Id}");
            var restaurant = await _repository.GetByIdAsync(request.Id);
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            restaurant = _mapper.Map(request, restaurant);

            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update)) throw new ForbiddenException();

            await _repository.UpdateAsync(restaurant);

        }
    }
}
