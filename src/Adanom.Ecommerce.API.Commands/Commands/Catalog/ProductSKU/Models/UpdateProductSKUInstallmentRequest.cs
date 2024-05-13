namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductSKUInstallmentRequest
    {
        public long Id { get; set; }

        public bool IsEligibleToInstallment { get; set; }

        public byte MaximumInstallmentCount { get; set; }
    }
}