using Adanom.Ecommerce.Data;
using Adanom.Ecommerce.Data.Models;
using Adanom.Ecommerce.Security;
using Adanom.Ecommerce.Security.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OpenIddict.Validation.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;

                options.ClaimsIdentity.UserNameClaimType = Claims.Email;
                options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = Claims.Role;
            })
                .AddSignInManager()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>();

            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                            .UseDbContext<ApplicationDbContext>();

                    options.UseQuartz();
                })
                .AddServer(options =>
                {
                    options.SetTokenEndpointUris("auth/connect/token");
                    options.SetUserinfoEndpointUris("auth/connect/userinfo");
                    options.SetLogoutEndpointUris("auth/connect/logout");

                    options.AllowPasswordFlow();
                    options.AllowRefreshTokenFlow();

                    options.RegisterScopes(
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles,
                            Permissions.Prefixes.Scope + "graphql");

                    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(30));
                    options.SetRefreshTokenLifetime(TimeSpan.FromDays(1));

                    options.AddEphemeralEncryptionKey();
                    options.AddEphemeralSigningKey();
                    options.AddSigningKey(SecurityConstants.SymmetricSecurityKey);
                    options.AddEncryptionKey(SecurityConstants.SymmetricSecurityKey);

                    options.DisableAccessTokenEncryption();

                    options.UseAspNetCore()
                           .EnableTokenEndpointPassthrough()
                           .EnableUserinfoEndpointPassthrough()
                           .EnableLogoutEndpointPassthrough();
                })
                .AddValidation(options =>
                {
                    options.UseSystemNetHttp();
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });

            services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(SecurityConstants.Policies.Admin.Name, policy =>
                {
                    foreach (var role in SecurityConstants.Policies.Admin.RequiredRoles)
                    {
                        policy.RequireRole(Claims.Role, role);
                    }
                });

                options.AddPolicy(SecurityConstants.Policies.Customer.Name, policy =>
                {
                    foreach (var role in SecurityConstants.Policies.Customer.RequiredRoles)
                    {
                        policy.RequireRole(Claims.Role, role);
                    }
                });
            });

            return services;
        }
    }
}
