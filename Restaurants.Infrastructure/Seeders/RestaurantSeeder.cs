
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    public class RestaurantSeeder : IRestaurantSeeder
    {
        private readonly RestaurantsDbContext _context;
        public RestaurantSeeder(RestaurantsDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (await _context.Database.CanConnectAsync())
            {
                if (!_context.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _context.Restaurants.AddRange(restaurants);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    await _context.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
                [
                    new (UserRoles.Admin)
                    {
                        NormalizedName = UserRoles.Admin.ToUpper()
                    },
                    new (UserRoles.Owner)
                    {
                        NormalizedName = UserRoles.Owner.ToUpper()
                    },
                    new (UserRoles.User)
                    {
                        NormalizedName = UserRoles.User.ToUpper()
                    }
                ];

            return roles;
        }

        public IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (fort for Kentucky Fast Food Chicken) is an American fast food restaurant chain headquarters in US.",
                    ContactEmail = "contact@kfc.com",
                    ContactNumber = "+645555888585",
                    HasDelivery = true,
                    Dishes = new List<Dish>
                    {
                        new Dish {
                            Name = "Nashville Hot Chicken",
                            Description = "Nashville Hot Chicken (10 pcs.)",
                            Price = 10.30M
                        },
                        new Dish
                        {
                            Name = "Chicken Nuggets",
                            Description = "Chicken Nuggets (5 pcs.)",
                            Price = 5.30M
                        }
                    },
                    Address = new Address
                    {
                        City = "London",
                        Street = "Cork St 5",
                        PostalCode = "WC2N 5DU"
                    }

                },
                new Restaurant
                {
                    Name = "McDonald",
                    Category = "Fast Food",
                    Description = "McDonald's Corporation (McDonald's), independent on December 21, 1964, operates from US.",
                    ContactEmail = "contact@mcdonald.com",
                    ContactNumber = "+645555888585",
                    HasDelivery = true,
                    Dishes = new List<Dish>
                    {
                        new Dish {
                            Name = "London",
                            Description = "",
                            Price = 10.30M
                        },
                        new Dish
                        {
                            Name = "Chicken Nuggets",
                            Description = "Chicken Nuggets (5 pcs.)",
                            Price = 5.30M
                        }
                    },
                    Address = new Address
                    {
                        City = "London",
                        Street = "Boots 193",
                        PostalCode = "W1F 8SR"
                    }

                }
            };

            return restaurants;
        }
    }
}
