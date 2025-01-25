using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class ProductSKUByProductIdDataLoader : BatchDataLoader<long, ProductSKU>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        public ProductSKUByProductIdDataLoader(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<long, ProductSKU>> LoadBatchAsync(
            IReadOnlyList<long> keys,
            CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var stockReservations = await applicationDbContext.StockReservations
                .Where(e => keys.Contains(e.ProductId))
                .GroupBy(e => e.ProductId)
                .Select(g => new { ProductId = g.Key, ReservedAmount = g.Sum(e => e.Amount) })
                .ToDictionaryAsync(x => x.ProductId, x => x.ReservedAmount, cancellationToken);

            var productSKUs = await applicationDbContext.Products
                .Where(e => keys.Contains(e.Id) && e.DeletedAtUtc == null)
                .AsNoTracking()
                .Include(e => e.Product_ProductSKU_Mappings)
                    .ThenInclude(m => m.ProductSKU)
                        .ThenInclude(s => s.Product_ProductSKU_Mappings)
                .SelectMany(e => e.Product_ProductSKU_Mappings
                    .Select(m => new { ProductId = e.Id, SKU = m.ProductSKU }))
                .ToDictionaryAsync(e => e.ProductId, e => e.SKU, cancellationToken);

            foreach (var kvp in productSKUs)
            {
                if (stockReservations.TryGetValue(kvp.Key, out var reservedAmount))
                {
                    kvp.Value.StockQuantity -= reservedAmount;
                }
            }

            return productSKUs;
        }
    }
}
