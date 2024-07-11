using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Middlewares
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

        public async Task InvokeAsync(HttpContext httpContext, ApplicationDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new NullReferenceException(nameof(dbContext));
            }

            await SeedDataAsync(dbContext);

            await _next(httpContext);
        }

        #endregion

        #region SeedDataAsync

        private async Task SeedDataAsync(ApplicationDbContext dbContext)
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

            var dbHasAddressCities = await dbContext.AddressCities.AnyAsync();

            if (!dbHasAddressCities)
            {
                await dbContext.Database.ExecuteSqlRawAsync(AddressDataScripts.AddressCitiesScript);

                Thread.Sleep(1000);

                await dbContext.Database.ExecuteSqlRawAsync(AddressDataScripts.AddressDistrictsScript);
            }

            foreach (var mailTemplate in SeedMailTemplateData.MailTemplates)
            {
                var mailTemplateExists = await dbContext.MailTemplates.AnyAsync(e => e.Key == mailTemplate.Key);

                if (mailTemplateExists)
                {
                    continue;
                }

                await dbContext.AddAsync(mailTemplate);

                await dbContext.SaveChangesAsync();
            }

            //var companyExists = await dbContext.Companies.AnyAsync();

            //if (!companyExists)
            //{
            //    await dbContext.Companies.AddAsync(SeedCompanyData.Company);

            //    await dbContext.SaveChangesAsync();
            //}

            _hasDataSeed = true;
            _lockSlim.Release(1);
        }

        #endregion
    }
}
