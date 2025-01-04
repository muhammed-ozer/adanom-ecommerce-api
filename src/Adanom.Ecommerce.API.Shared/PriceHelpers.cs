namespace System
{
    public static class PriceHelpers 
    {
        public static decimal Round(decimal value, int decimals = 2)
        {
            return decimal.Round(value, decimals, MidpointRounding.AwayFromZero);
        }

        public static decimal TruncateToDecimalPlaces(decimal value, int decimalPlaces = 2)
        {
            decimal multiplier = (decimal)Math.Pow(10, decimalPlaces);
            return Math.Floor(value * multiplier) / multiplier;
        }
    }
}
