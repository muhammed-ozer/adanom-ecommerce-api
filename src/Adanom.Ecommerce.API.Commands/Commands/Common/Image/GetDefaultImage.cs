namespace Adanom.Ecommerce.API.Commands
{
    public class GetEntityDefaultImage : IRequest<ImageResponse?>
    {
        #region Ctor

        public GetEntityDefaultImage(long entityId, EntityType entityType)
        {
            EntityId = entityId;
            EntityType = entityType;
        }

        #endregion

        #region Properties

        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }

        #endregion
    }
}