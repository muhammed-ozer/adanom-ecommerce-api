using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using OpenIddict.Validation.AspNetCore;

namespace Adanom.Ecommerce.API.Backend.Graphql
{
    internal sealed class HttpRequestInterceptor : DefaultHttpRequestInterceptor
    {
        public override ValueTask OnCreateAsync(HttpContext context,
            IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder,
            CancellationToken cancellationToken)
        {
            try
            {
                var claimsPrincipal = AsyncHelper.RunSync(() => context.AuthenticateAsync(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)).Principal;

                if (claimsPrincipal is null)
                {
                    return base.OnCreateAsync(context, requestExecutor, requestBuilder,
                        cancellationToken);
                }

                context.User = claimsPrincipal;

                requestBuilder.SetGlobalState("Identity", claimsPrincipal);
            }
            catch { }

            return base.OnCreateAsync(context, requestExecutor, requestBuilder,
                cancellationToken);
        }
    }
}
