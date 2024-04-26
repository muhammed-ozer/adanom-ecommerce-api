using System.Security.Claims;
using Adanom.Ecommerce.API.Commands;
using Adanom.Ecommerce.API.Commands.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Serilog;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Adanom.Ecommerce.API.Site.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthorizationController : ControllerBase
    {
        #region Fields

        private readonly IOpenIddictApplicationManager _applicationManager;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public AuthorizationController(
            IOpenIddictApplicationManager applicationManager, 
            IMediator mediator,
            IMapper mapper)
        {
            _applicationManager = applicationManager ?? throw new ArgumentNullException(nameof(applicationManager));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        } 

        #endregion

        #region ExchangeAsync

        [HttpPost("connect/token"), Produces("application/json")]
        public async Task<IActionResult> ExchangeAsync()
        {
            var request = HttpContext.GetOpenIddictServerRequest();

            if (request is null)
            {
                throw new NullReferenceException(nameof(request));
            }

            var application = await _applicationManager.FindByClientIdAsync(request.ClientId!);

            if (application is null)
            {
                Log.Error("Openiddict: Application Not Found!");

                throw new InvalidOperationException("The application cannot be found.");
            }

            if (request.IsPasswordGrantType())
            {
                var loginCommand = _mapper.Map<Login>(new LoginRequest()
                {
                    Email = request.Username!,
                    Password = request.Password!
                });

                var userResponse = await _mediator.Send(loginCommand);

                if (userResponse is null)
                {
                    return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }

                var identity = new ClaimsIdentity(
                    TokenValidationParameters.DefaultAuthenticationType,
                    Claims.Name,
                    Claims.Role);

                identity.AddClaim(Claims.Subject, userResponse.Id.ToString(), Destinations.AccessToken);
                identity.AddClaim(Claims.Email, userResponse.Email, Destinations.AccessToken);
                identity.AddClaim(Claims.Name, userResponse.FirstName, Destinations.AccessToken);
                identity.AddClaim(Claims.FamilyName, userResponse.LastName, Destinations.AccessToken);
                identity.AddClaim(Claims.EmailVerified, userResponse.EmailConfirmed.ToString(), Destinations.AccessToken);
                identity.AddClaim(Claims.PhoneNumber, userResponse.PhoneNumber, Destinations.AccessToken);

                if (userResponse.Roles.Any())
                {
                    foreach (var role in userResponse.Roles)
                    {
                        identity.AddClaim(Claims.Role, role, Destinations.AccessToken);
                    }
                }

                identity.SetDestinations(static claim => claim.Type switch
                {
                    Claims.Name when claim.Subject!.HasScope(Scopes.Profile)
                        => new[] { Destinations.AccessToken, Destinations.IdentityToken },

                    _ => new[] { Destinations.AccessToken }
                });

                var claimsPrincipal = new ClaimsPrincipal(identity);

                claimsPrincipal.SetScopes(request.GetScopes());

                return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }
            else if (request.IsRefreshTokenGrantType())
            {
                var claimsPrincipal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;

                if (claimsPrincipal == null)
                {
                    return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }

                var userId = claimsPrincipal.GetUserId();

                var canUserLogin = await _mediator.Send(new CanUserLogin(userId));

                if (!canUserLogin)
                {
                    return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }

                return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            throw new NotImplementedException("The specified grant type is not implemented.");
        }

        #endregion

        #region GetUserInfoAsync

        [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
        [HttpGet("connect/userinfo")]
        public async Task<IActionResult> GetUserInfoAsync()
        {
            var claimsPrincipal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;

            if (claimsPrincipal is null) 
            {
                return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            return Ok(new
            {
                Email = claimsPrincipal.GetClaim(Claims.Email),
                FirstName = claimsPrincipal.GetClaim(Claims.Name),
                LastName = claimsPrincipal.GetClaim(Claims.FamilyName),
                EmailConfirmed = claimsPrincipal.GetClaim(Claims.EmailVerified),
                PhoneNumber = claimsPrincipal.GetClaim(Claims.PhoneNumber),
                Roles = claimsPrincipal.GetClaims(Claims.Role)
            });
        }

        #endregion

        #region Logout

        [HttpPost("connect/logout")]
        public IActionResult Logout()
        {
            return SignOut(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        #endregion
    }
}
