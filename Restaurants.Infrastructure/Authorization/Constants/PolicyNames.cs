using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Constants
{
    public static class PolicyNames
    {
        public const string HasNationality = "HasNationality";
        public const string Atleast20 = "Atleast20";
    }

    public static class AppClaimTypes
    {
        public const string Nationality = "Nationality";
        public const string DateOfBirth = "DateOfBirth";
    }
}
