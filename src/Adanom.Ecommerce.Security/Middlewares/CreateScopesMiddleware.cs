using Microsoft.AspNetCore.Http;
using OpenIddict.Abstractions;

namespace Adanom.Ecommerce.Security.Middlewares
{
    internal sealed class CreateScopesMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;
        private readonly SemaphoreSlim _lockSlim = new SemaphoreSlim(1, 1);
        private bool _scopesHasCreated = false;

        #endregion

        #region Ctor

        public CreateScopesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region InvokeAsync

        public async Task InvokeAsync(HttpContext httpContext, IOpenIddictScopeManager openIddictScopeManager)
        {
            if (openIddictScopeManager is null)
            {
                throw new NullReferenceException(nameof(openIddictScopeManager));
            }

            await CreateScopesAsync(openIddictScopeManager);

            await _next(httpContext);
        }

        #endregion

        #region CreateScopesAsync

        private async Task CreateScopesAsync(IOpenIddictScopeManager openIddictScopeManager)
        {
            if (_scopesHasCreated)
            {
                return;
            }

            await _lockSlim.WaitAsync();

            if (_scopesHasCreated)
            {
                _lockSlim.Release(1);

                return;
            }

            if (await openIddictScopeManager.FindByNameAsync("profile") is null)
            {
                await openIddictScopeManager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = "profile",
                    DisplayName = "profile scope"
                });
            }

            if (await openIddictScopeManager.FindByNameAsync("offline_access") is null)
            {
                await openIddictScopeManager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = "offline_access",
                    DisplayName = "offline_access scope"
                });
            }

            if (await openIddictScopeManager.FindByNameAsync("graphql") is null)
            {
                await openIddictScopeManager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = "graphql",
                    DisplayName = "graphql scope"
                });
            }

            _scopesHasCreated = true;
            _lockSlim.Release(1);
        }

        #endregion
    }
}
