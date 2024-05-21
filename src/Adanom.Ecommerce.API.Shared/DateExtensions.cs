namespace System
{
    public static class DateExtensions
    {
        #region StartOfDate

        public static DateTime StartOfDate(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0);
        }

        #endregion

        #region EndOfDate

        public static DateTime EndOfDate(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 999);
        }

        #endregion
    }
}
