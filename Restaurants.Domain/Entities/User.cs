

using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace Restaurants.Domain.Entities
{
    public class User : IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }

    }
}
