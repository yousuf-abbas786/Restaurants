using MediatR;

using Microsoft.Extensions.Logging;

using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _repository;

        public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting restaurant with id: {request.Id}");
            var restaurant = await _repository.GetByIdAsync(request.Id );
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            await _repository.DeleteAsync(restaurant);

        }
    }
}
