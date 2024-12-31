using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _repository;
        private readonly IMapper _mapper;

        public UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IRestaurantsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating restaurant by id: {request.Id}");
            var restaurant = await _repository.GetByIdAsync(request.Id);
            if (restaurant == null)
            {
                return false;
            }

            restaurant = _mapper.Map(request, restaurant);

            return await _repository.UpdateAsync(restaurant);

        }
    }
}
