namespace System
{
    public static class Calculations
    {
        #region CalculateTaxFromIncludedTaxTotal

        public static decimal CalculateTaxFromIncludedTaxTotal(decimal price, decimal taxRate)
        {
            return decimal.Round(price - (price / (1 + taxRate)), 2, MidpointRounding.AwayFromZero);
        }

        #endregion
    }
}
