using MediatR;

using Microsoft.AspNetCore.Mvc;

using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase 
    {
        private readonly IMediator _mediator;
        public RestaurantsController(IMediator mediator) 
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id) 
        {
            var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));

            if (restaurant == null) return NotFound("Restaurant doesn't exists");

            return Ok(restaurant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var isDeleted = await _mediator.Send(new DeleteRestaurantCommand(id));

            if (isDeleted) return NoContent();

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRestaurantCommand command)
        {
            int id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(UpdateRestaurantCommand command, [FromRoute]int id)
        {
            command.Id = id;
            var isUpdated = await _mediator.Send(command);

            if (isUpdated) return NoContent();

            return NotFound();
        }
    }
}
