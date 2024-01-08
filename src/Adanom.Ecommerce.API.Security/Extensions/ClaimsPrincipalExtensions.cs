using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;

namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirstValue(OpenIddictConstants.Claims.Subject);

            if (userId.IsNullOrEmpty())
            {
                return Guid.Empty;
            }

            return new Guid(userId!);
        }

        public static string? GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(OpenIddictConstants.Claims.Email);

            return email;
        }
    }
}
