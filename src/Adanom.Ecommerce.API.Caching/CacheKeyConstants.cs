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

        public static class ProductSKU
        {
            public static string Region = "ProductSKURegion";

            public static string ByProductIdPattern = "ProductSKU-ProductId:*";

            public static string CacheKeyById(long id) => $"ProductSKU-Id:{id}";

            public static string CacheKeyByCode(string code) => $"ProductSKU-Code:{code}";

            public static string CacheKeyByProductId(long productId) => $"ProductSKU-ProductId:{productId}";
        }

        public static class ProductPrice
        {
            public static string Region = "ProductPriceRegion";

            public static string ByProductIdPattern = "ProductPrice-ProductId:*";

            public static string CacheKeyById(long id) => $"ProductPrice-Id:{id}";

            public static string CacheKeyByProductId(long productId) => $"ProductPrice-ProductId:{productId}";
        }
    }
}