using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {
        #region ConvertToUrlSlug

        public static string ConvertToUrlSlug(this string text)
        {
            text = text.ToLower();
            text = text.Replace("ö", "o");
            text = text.Replace("ğ", "g");
            text = text.Replace("ı", "i");
            text = text.Replace("ü", "u");
            text = text.Replace("ş", "s");
            text = text.Replace("ç", "c");
            text = text.NonAlphaNumeraticCharReplacer("-");

            return text;
        }

        #endregion

        #region NonAlphaNumeraticCharReplacer

        public static string NonAlphaNumeraticCharReplacer(this string text, string replacement)
        {
            var regex = new Regex("[^a-zA-Z0-9+]");

            text = regex.Replace(text, replacement);

            return text;
        }

        #endregion

        #region IsNotNullOrEmpty

        public static bool IsNotNullOrEmpty(this string? text)
        {
            return !string.IsNullOrEmpty(text);
        }

        #endregion
    }
}
