namespace Adanom.Ecommerce.API.Commands.Models
{
    public class DeleteProduct_ProductSpecificationAttributeRequest
    {
        public long ProductId { get; set; }

        public long ProductSpecificationAttributeId { get; set; }
    }
}