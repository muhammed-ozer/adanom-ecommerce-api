using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class BrandByIdDataLoader : BatchDataLoader<long, Brand>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BrandByIdDataLoader(
            ApplicationDbContext applicationDbContext,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        protected override async Task<IReadOnlyDictionary<long, Brand>> LoadBatchAsync(
            IReadOnlyList<long> keys,
            CancellationToken cancellationToken)
        {
            var brands = await _applicationDbContext.Brands
                .Where(e => keys.Contains(e.Id) && e.DeletedAtUtc == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return brands.ToDictionary(e => e.Id);
        }
    }
}
