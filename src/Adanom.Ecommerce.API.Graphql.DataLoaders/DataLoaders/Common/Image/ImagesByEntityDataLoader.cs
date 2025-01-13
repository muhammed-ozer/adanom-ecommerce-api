using Adanom.Ecommerce.API.Data;
using Adanom.Ecommerce.API.Data.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Graphql.DataLoaders
{
    public class ImagesByEntityDataLoader : BatchDataLoader<(long EntityId, EntityType EntityType), List<Image>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        public ImagesByEntityDataLoader(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<(long EntityId, EntityType EntityType), List<Image>>> LoadBatchAsync(
            IReadOnlyList<(long EntityId, EntityType EntityType)> keys,
            CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var entityIds = keys.Select(k => k.EntityId).Distinct();
            var entityTypes = keys.Select(k => k.EntityType).Distinct();

            var images = await applicationDbContext.Images
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
