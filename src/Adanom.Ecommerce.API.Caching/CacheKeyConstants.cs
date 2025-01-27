namespace Adanom.Ecommerce.API.Caching
{
    public static class CacheKeyConstants
    {
        public static class Product
        {
            public static string Region = "ProductRegion";

            public static string CacheKeyById(long id) => $"Product-Id:{id}";

            public static string CacheKeyByUrlSlug(string urlSlug) => $"Product-UrlSlug:{urlSlug}";
        }
    }
}