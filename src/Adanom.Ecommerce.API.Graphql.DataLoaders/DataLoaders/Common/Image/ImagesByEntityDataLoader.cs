using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class ImagesByEntityDataLoader : BatchDataLoader<(long EntityId, EntityType EntityType), List<Image>>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ImagesByEntityDataLoader(
            ApplicationDbContext applicationDbContext,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        protected override async Task<IReadOnlyDictionary<(long EntityId, EntityType EntityType), List<Image>>> LoadBatchAsync(
            IReadOnlyList<(long EntityId, EntityType EntityType)> keys,
            CancellationToken cancellationToken)
        {
            var entityIds = keys.Select(k => k.EntityId).Distinct();
            var entityTypes = keys.Select(k => k.EntityType).Distinct();

            var images = await _applicationDbContext.Images
                .AsNoTracking()
                .Where(e => entityIds.Contains(e.EntityId) && entityTypes.Contains(e.EntityType))
                .OrderBy(e => e.EntityId)
                .ThenBy(e => e.EntityType)
                .ThenBy(e => e.IsDefault)
                .ThenBy(e => e.DisplayOrder)
                .ToListAsync(cancellationToken);

            var groupedImages = images
                .GroupBy(img => (img.EntityId, img.EntityType))
                .ToDictionary(
                    g => g.Key,
                    g => g.ToList()
                );

            foreach (var key in keys)
            {
                if (!groupedImages.ContainsKey(key))
                {
                    groupedImages[key] = new List<Image>();
                }
            }

            return groupedImages;
        }
    }
}
