namespace Adanom.Ecommerce.API.Commands.Models
{
    public class MetaInformationResponse : BaseResponseEntity<long>
    {
        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Keywords { get; set; } = null!;
    }
}