namespace Adanom.Ecommerce.API.Commands
{
    public class GetEntityImages : IRequest<IEnumerable<ImageResponse>>
    {
        #region Ctor

        public GetEntityImages(long entityId, EntityType entityType)
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