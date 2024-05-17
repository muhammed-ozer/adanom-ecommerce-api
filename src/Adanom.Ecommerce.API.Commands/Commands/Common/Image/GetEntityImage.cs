using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetEntityImage : IRequest<ImageResponse?>
    {
        #region Ctor

        public GetEntityImage(long entityId, EntityType entityType)
        {
            EntityId = entityId;
            EntityType = entityType;
        }

        public GetEntityImage(long entityId, EntityType entityType, ImageType? imageType)
        {
            EntityId = entityId;
            EntityType = entityType;
            ImageType = imageType;
        }

        public GetEntityImage(long entityId, EntityType entityType, bool? isDefault)
        {
            EntityId = entityId;
            EntityType = entityType;
            IsDefault = isDefault;
        }

        #endregion

        #region Properties

        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }

        public ImageType? ImageType { get; set; }

        public bool? IsDefault { get; set; }

        #endregion
    }
}