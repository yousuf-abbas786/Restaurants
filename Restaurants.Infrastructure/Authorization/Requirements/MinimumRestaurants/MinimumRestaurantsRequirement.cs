using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurants
{
    public class MinimumRestaurantsRequirement : IAuthorizationRequirement
    {
        public MinimumRestaurantsRequirement(int minimumRestaurants)
        {
            MinimumRestaurants = minimumRestaurants;
        }

        public int MinimumRestaurants { get; }
    }
}
