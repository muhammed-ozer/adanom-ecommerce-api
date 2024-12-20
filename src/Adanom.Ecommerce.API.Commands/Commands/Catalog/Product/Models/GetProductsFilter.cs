namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetProductsFilter
    {
        public bool? IncludeFilterResponse { get; set; } = false;

        public bool? IsRequestFromStoreClient { get; set; } = false;

        public string? Query { get; set; }

        public bool? OutOfStock { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsNew { get; set; }

        public bool? IsInHighlights { get; set; }

        public bool? IsInProductsOfTheWeek { get; set; }

        [GraphQLName("productCategories")]
        public ICollection<string>? ProductCategoryUrlSlugs { get; set; }

        [GraphQLName("brands")]
        public ICollection<string>? BrandUrlSlugs { get; set; }

        [GraphQLName("productSpecificationAttributes")]
        public ICollection<long>? ProductSpecificationAttributeIds { get; set; }

        public decimal? MinimumPrice { get; set; }

        public decimal? MaximumPrice { get; set; }

        public byte? ReviewPoint { get; set; }

        [DefaultValue(GetProductsOrderByEnum.DISPLAY_ORDER_ASC)]
        public GetProductsOrderByEnum? OrderBy { get; set; }
    }
}