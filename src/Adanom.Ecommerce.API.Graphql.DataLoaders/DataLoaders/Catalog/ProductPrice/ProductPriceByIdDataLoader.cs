using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class ProductPriceByIdDataLoader : BatchDataLoader<long, ProductPrice>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        public ProductPriceByIdDataLoader(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<long, ProductPrice>> LoadBatchAsync(
            IReadOnlyList<long> keys,
            CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productPrices = await applicationDbContext.ProductPrices
                .Where(e => keys.Contains(e.Id) && e.DeletedAtUtc == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return productPrices.ToDictionary(e => e.Id);
        }
    }
}
