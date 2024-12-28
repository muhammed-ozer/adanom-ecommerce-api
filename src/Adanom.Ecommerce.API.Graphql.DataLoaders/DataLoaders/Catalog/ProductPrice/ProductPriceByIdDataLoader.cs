using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class ProductPriceByIdDataLoader : BatchDataLoader<long, ProductPrice>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductPriceByIdDataLoader(
            ApplicationDbContext applicationDbContext,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        protected override async Task<IReadOnlyDictionary<long, ProductPrice>> LoadBatchAsync(
            IReadOnlyList<long> keys,
            CancellationToken cancellationToken)
        {
            var productPrices = await _applicationDbContext.ProductPrices
                .Where(e => keys.Contains(e.Id) && e.DeletedAtUtc == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return productPrices.ToDictionary(e => e.Id);
        }
    }
}
