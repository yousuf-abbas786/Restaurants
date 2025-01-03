using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization.Constants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization
{
    public class RestaurantsUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        private readonly UserManager<User> _userManager;
        public RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            _userManager = userManager;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var id = await GenerateClaimsAsync(user);

            if (user.Nationality != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
            }

            if (user.DateOfBirth != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
            }

            var dbUser = await _userManager.Users.Include(x => x.OwnedRestaurants).FirstOrDefaultAsync(u => u.Email == user.Email);
            if (dbUser != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.RestaurantsOwned, dbUser.OwnedRestaurants.Count.ToString()));
            }

            return new ClaimsPrincipal(id);
        }
    }
}
