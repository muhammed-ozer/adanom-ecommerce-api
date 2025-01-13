namespace Adanom.Ecommerce.API.Services
{
    public interface ICalculationService
    {
        decimal CalculateTaxTotal(decimal taxIncludedPrice, decimal taxRate);

        decimal CalculateTaxExcludedPrice(decimal taxIncludedPrice, decimal taxRate);

        decimal CalculateTaxIncludedPrice(decimal taxExcludedPrice, decimal taxRate);

        decimal CalculateDiscountedPriceByDiscountRate(decimal originalPrice, decimal discountRate);

        decimal CalculateDiscountByDiscountRate(decimal value, decimal discountRate);
    }
}
