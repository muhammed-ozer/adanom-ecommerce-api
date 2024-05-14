using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateMetaInformationRequest
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Keywords { get; set; } = null!;
    }
}