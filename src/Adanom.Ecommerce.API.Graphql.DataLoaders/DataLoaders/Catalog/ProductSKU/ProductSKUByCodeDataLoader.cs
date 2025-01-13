using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class ProductSKUByCodeDataLoader : BatchDataLoader<string, ProductSKU>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        public ProductSKUByCodeDataLoader(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<string, ProductSKU>> LoadBatchAsync(
            IReadOnlyList<string> keys,
            CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productSKUs = await applicationDbContext.ProductSKUs
                .Where(e => keys.Contains(e.Code) && e.DeletedAtUtc == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return productSKUs.ToDictionary(e => e.Code);
        }
    }
}
