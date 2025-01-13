using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class DefaultEntityImageDataLoader : BatchDataLoader<(long EntityId, EntityType EntityType), Image?>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        public DefaultEntityImageDataLoader(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<(long EntityId, EntityType EntityType), Image?>> LoadBatchAsync(
            IReadOnlyList<(long EntityId, EntityType EntityType)> keys,
            CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var entityIds = keys.Select(e=> e.EntityId).Distinct();
            var entityTypes = keys.Select(e => e.EntityType).Distinct();

            var images = await applicationDbContext.Images
                .AsNoTracking()
                .Where(e => entityIds.Contains(e.EntityId) &&
                            entityTypes.Contains(e.EntityType) &&
                            e.IsDefault)
                .ToListAsync(cancellationToken);

            var imageDictionary = images
                .ToDictionary(
                    e => (e.EntityId, e.EntityType),
                    e => (Image?)e                         
                );

            foreach (var key in keys)
            {
                if (!imageDictionary.ContainsKey(key))
                {
                    imageDictionary[key] = null;
                }
            }

            return imageDictionary;
        }
    }
}
