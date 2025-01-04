namespace Adanom.Ecommerce.API.Services.Implementations
{
    internal sealed class CalculationService : ICalculationService
    {
        #region CalculateTaxTotal

        public decimal CalculateTaxTotal(decimal taxIncludedPrice, decimal taxRate)
        {
            if (taxRate == 0)
            {
                return 0;
            }

            var result = taxIncludedPrice - (taxIncludedPrice / (1 + (taxRate / 100)));

            return PriceHelpers.Round(result);
        }

        #endregion

        #region CalculateTaxExcludedPrice

        public decimal CalculateTaxExcludedPrice(decimal taxIncludedPrice, decimal taxRate)
        {
            if (taxRate == 0)
            {
                return taxIncludedPrice;
            }

            var result = taxIncludedPrice / (1 + (taxRate / 100));

            return PriceHelpers.Round(result);
        }

        #endregion

        #region CalculateTaxIncludedPrice

        public decimal CalculateTaxIncludedPrice(decimal taxExcludedPrice, decimal taxRate)
        {
            if (taxRate == 0)
            {
                return taxExcludedPrice;
            }

            var result = taxExcludedPrice * (1 + (taxRate / 100));

            return PriceHelpers.Round(result);
        }

        #endregion

        #region CalculateDiscountedPriceByDiscountRate

        public decimal CalculateDiscountedPriceByDiscountRate(decimal originalPrice, decimal discountRate)
        {
            var result = originalPrice - (originalPrice * discountRate / 100);

            return PriceHelpers.Round(result);
        }

        #endregion
    }
}
