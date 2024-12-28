using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class ProductSKUByIdDataLoader : BatchDataLoader<long, ProductSKU>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductSKUByIdDataLoader(
            ApplicationDbContext applicationDbContext,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        protected override async Task<IReadOnlyDictionary<long, ProductSKU>> LoadBatchAsync(
            IReadOnlyList<long> keys,
            CancellationToken cancellationToken)
        {
            var productSKUs = await _applicationDbContext.ProductSKUs
                .Where(e => keys.Contains(e.Id) && e.DeletedAtUtc == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return productSKUs.ToDictionary(e => e.Id);
        }
    }
}
