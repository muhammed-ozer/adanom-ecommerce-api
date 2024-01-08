using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Security
{
    internal static class SeedData
    {
        internal static ICollection<Role> Roles { get; set; }

        internal static ICollection<User> Users { get; set; }

        static SeedData()
        {
            Roles = new List<Role>()
            {
                new()
                {
                    Name = SecurityConstants.Roles.Admin,
                    NormalizedName = SecurityConstants.Roles.Admin,
                },
                new()
                {
                    Name = SecurityConstants.Roles.Customer,
                    NormalizedName = SecurityConstants.Roles.Customer,
                }
            };

            Users = new List<User>()
            {
                new()
                {
                    FirstName = "Muhammed",
                    LastName = "Ozer",
                    Email = "mhmdozer@gmail.com",
                    UserName = "mhmdozer@gmail.com",
                    PhoneNumber = "5300000000"
                }
            };
        }
    }
}
