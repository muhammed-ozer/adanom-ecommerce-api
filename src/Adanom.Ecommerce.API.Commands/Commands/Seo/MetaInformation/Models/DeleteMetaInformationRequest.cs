using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Commands.Models
{
    public class DeleteMetaInformationRequest
    {
        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }
    }
}