namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductSKUBarcodesRequest
    {
        public long Id { get; set; }

        public string? Barcodes { get; set; }
    }
}