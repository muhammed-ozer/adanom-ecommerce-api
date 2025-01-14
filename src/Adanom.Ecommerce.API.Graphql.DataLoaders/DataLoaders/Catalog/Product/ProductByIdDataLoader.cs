using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class ProductByIdDataLoader : BatchDataLoader<long, Product>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        public ProductByIdDataLoader(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<long, Product>> LoadBatchAsync(
            IReadOnlyList<long> keys,
            CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var products = await applicationDbContext.Products
                .Where(e => keys.Contains(e.Id) && e.DeletedAtUtc == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return products.ToDictionary(e => e.Id);
        }
    }
}
