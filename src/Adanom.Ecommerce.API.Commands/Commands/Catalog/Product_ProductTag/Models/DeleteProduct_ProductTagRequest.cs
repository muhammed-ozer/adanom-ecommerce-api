namespace Adanom.Ecommerce.API.Commands.Models
{
    public class DeleteProduct_ProductTagRequest
    {
        public long ProductId { get; set; }

        public long ProductTagId { get; set; }
    }
}