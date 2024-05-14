namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateMetaInformation_EntityRequest
    {
        public long MetaInformationId { get; set; }

        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }
    }
}