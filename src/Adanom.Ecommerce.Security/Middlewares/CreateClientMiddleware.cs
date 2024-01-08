using Microsoft.AspNetCore.Http;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Adanom.Ecommerce.Security.Middlewares
{
    internal sealed class CreateClientMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;
        private readonly SemaphoreSlim _lockSlim = new SemaphoreSlim(1, 1);
        private bool _clientsHasCreated = false;

        #endregion

        #region Ctor

        public CreateClientMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region InvokeAsync

        public async Task InvokeAsync(HttpContext httpContext, IOpenIddictApplicationManager openIddictApplicationManager)
        {
            if (openIddictApplicationManager is null)
            {
                throw new NullReferenceException(nameof(openIddictApplicationManager));
            }

            await CreateClientAsync(openIddictApplicationManager);

            await _next(httpContext);
        }

        #endregion

        #region CreateClientAsync

        private async Task CreateClientAsync(IOpenIddictApplicationManager openIddictApplicationManager)
        {
            if (_clientsHasCreated)
            {
                return;
            }

            await _lockSlim.WaitAsync();

            if (_clientsHasCreated)
            {
                _lockSlim.Release(1);

                return;
            }

            var clientApplication = await openIddictApplicationManager.FindByClientIdAsync(SecurityConstants.AdanomClientApplication.Id);

            if (clientApplication is null)
            {
                await openIddictApplicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = SecurityConstants.AdanomClientApplication.Id,
                    ClientSecret = SecurityConstants.AdanomClientApplication.Secret,
                    DisplayName = SecurityConstants.AdanomClientApplication.DisplayName,
                    Permissions =
                    {
                        Permissions.Endpoints.Token,
                        Permissions.Endpoints.Logout,

                        Permissions.GrantTypes.Password,
                        Permissions.GrantTypes.RefreshToken,

                        Permissions.Scopes.Profile,
                        Permissions.Prefixes.Scope + "graphql"
                    }
                });
            }

            _clientsHasCreated = true;
            _lockSlim.Release(1);
        }

        #endregion
    }
}
