using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class EntityImageDataLoader : BatchDataLoader<(long EntityId, EntityType EntityType), Image?>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EntityImageDataLoader(
            ApplicationDbContext applicationDbContext,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        protected override async Task<IReadOnlyDictionary<(long EntityId, EntityType EntityType), Image?>> LoadBatchAsync(
            IReadOnlyList<(long EntityId, EntityType EntityType)> keys,
            CancellationToken cancellationToken)
        {
            var entityIds = keys.Select(e=> e.EntityId).Distinct();
            var entityTypes = keys.Select(e => e.EntityType).Distinct();

            var images = await _applicationDbContext.Images
                .AsNoTracking()
                .Where(e => entityIds.Contains(e.EntityId) &&
                            entityTypes.Contains(e.EntityType))
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
