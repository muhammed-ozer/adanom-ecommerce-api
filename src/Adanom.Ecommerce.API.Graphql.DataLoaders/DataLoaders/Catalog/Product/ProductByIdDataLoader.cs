using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class ProductByIdDataLoader : BatchDataLoader<long, Product>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductByIdDataLoader(
            ApplicationDbContext applicationDbContext,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        protected override async Task<IReadOnlyDictionary<long, Product>> LoadBatchAsync(
            IReadOnlyList<long> keys,
            CancellationToken cancellationToken)
        {
            var products = await _applicationDbContext.Products
                .Where(e => keys.Contains(e.Id) && e.DeletedAtUtc == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return products.ToDictionary(e => e.Id);
        }
    }
}
