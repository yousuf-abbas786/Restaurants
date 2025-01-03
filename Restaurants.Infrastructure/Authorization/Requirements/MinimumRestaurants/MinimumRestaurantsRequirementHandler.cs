using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

using Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurants
{
    public class MinimumRestaurantsRequirementHandler : AuthorizationHandler<MinimumRestaurantsRequirement>
    {
        private readonly ILogger<MinimumRestaurantsRequirementHandler> _logger;
        private readonly IUserContext _userContext;

        public MinimumRestaurantsRequirementHandler(ILogger<MinimumRestaurantsRequirementHandler> logger, IUserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsRequirement requirement)
        {
            var currentUser = _userContext.GetCurrentUser();
            _logger.LogInformation("User: {Email}, RestaurantsOwned: {row} - Handling MinimumRestaurantsRequirement", currentUser!.Email, currentUser.RestaurantsOwned);

            if (currentUser.RestaurantsOwned == null)
            {
                _logger.LogWarning("User RestaurantsOwned is null");
                context.Fail();
                return Task.CompletedTask;
            }

            if (currentUser.RestaurantsOwned.Value >= requirement.MinimumRestaurants)
            {
                _logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
