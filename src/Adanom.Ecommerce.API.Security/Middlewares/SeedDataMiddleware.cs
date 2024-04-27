using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Security.Middlewares
{
    internal sealed class SeedDataMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;
        private readonly SemaphoreSlim _lockSlim = new SemaphoreSlim(1, 1);
        private static bool _hasDataSeed = false;

        #endregion

        #region Ctor

        public SeedDataMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region InvokeAsync

        public async Task InvokeAsync(HttpContext httpContext,
                                      ApplicationDbContext dbContext,
                                      UserManager<User> userManager,
                                      RoleManager<Role> roleManager)
        {
            if (dbContext is null)
            {
                throw new NullReferenceException(nameof(dbContext));
            }

            if (userManager is null)
            {
                throw new NullReferenceException(nameof(userManager));
            }

            await SeedDataAsync(dbContext, userManager, roleManager);

            await _next(httpContext);
        }

        #endregion

        #region SeedDataAsync

        private async Task SeedDataAsync(ApplicationDbContext dbContext,
                                         UserManager<User> userManager,
                                         RoleManager<Role> roleManager)
        {
            if (_hasDataSeed)
            {
                return;
            }

            await _lockSlim.WaitAsync();

            if (_hasDataSeed)
            {
                _lockSlim.Release(1);

                return;
            }

            foreach (var role in SeedData.Roles)
            {
                var dbRole = await roleManager.FindByNameAsync(role.Name!);

                if (dbRole is null)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            await dbContext.SaveChangesAsync();

            foreach (var user in SeedData.Users)
            {
                var dbUser = await userManager.FindByEmailAsync(user.Email);

                if (dbUser is null)
                {
                    user.EmailConfirmed = true;

                    await userManager.CreateAsync(user, "P@ssw0rd");
                    var adminRole = await dbContext.Roles.SingleOrDefaultAsync(e => e.Name == SecurityConstants.Roles.Admin);

                    await userManager.AddToRoleAsync(user, adminRole!.Name!);
                }
            }

            _hasDataSeed = true;
            _lockSlim.Release(1);
        }

        #endregion
    }
}
