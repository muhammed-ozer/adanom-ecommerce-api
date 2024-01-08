using Microsoft.IdentityModel.Tokens;

namespace Adanom.Ecommerce.API.Security
{
    public static class SecurityConstants
    {
        public static SymmetricSecurityKey SymmetricSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY="));

        public static class AdanomClientApplication
        {
            public const string Id = "adanom-ecommerce-api";

            public const string Secret = "388D45FA-B36B-4988-BA59-B187D329C207";

            public const string DisplayName = "Adanom ecommerce API";
        }

        public static class Policies
        {
            public static class Admin
            {
                public const string Name = "AdminPolicy";

                public static readonly string[] RequiredRoles = { Roles.Admin };
            }

            public static class Customer
            {
                public const string Name = "CustomerPolicy";

                public static readonly string[] RequiredRoles = { Roles.Customer };
            }
        }

        public static class Roles
        {
            public const string Admin = "Admin";

            public const string Customer = "Customer";
        }
    }
}
