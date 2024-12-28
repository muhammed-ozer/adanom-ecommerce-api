using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class ProductSKUByCodeDataLoader : BatchDataLoader<string, ProductSKU>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductSKUByCodeDataLoader(
            ApplicationDbContext applicationDbContext,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        protected override async Task<IReadOnlyDictionary<string, ProductSKU>> LoadBatchAsync(
            IReadOnlyList<string> keys,
            CancellationToken cancellationToken)
        {
            var productSKUs = await _applicationDbContext.ProductSKUs
                .Where(e => keys.Contains(e.Code) && e.DeletedAtUtc == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return productSKUs.ToDictionary(e => e.Code);
        }
    }
}
