using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetMetaInformation : IRequest<MetaInformationResponse?>
    {
        #region Ctor

        public GetMetaInformation(long id)
        {
            Id = id;
        }

        public GetMetaInformation(long entityId, EntityType entityType)
        {
            EntityId = entityId;
            EntityType = entityType;
        }

        #endregion

        #region Properties


        public long Id { get; set; }

        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }

        #endregion
    }
}