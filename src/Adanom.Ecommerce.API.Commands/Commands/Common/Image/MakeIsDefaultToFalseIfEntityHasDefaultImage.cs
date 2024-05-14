namespace Adanom.Ecommerce.API.Commands
{
    public class MakeIsDefaultToFalseIfEntityHasDefaultImage : IRequest<bool>
    {
        #region Ctor

        public MakeIsDefaultToFalseIfEntityHasDefaultImage(long entityId, EntityType entityType)
        {
            EntityId = entityId;
            EntityType = entityType;
        }

        #endregion

        #region Properties

        public long EntityId { get; }

        public EntityType EntityType { get; }

        #endregion
    }
}