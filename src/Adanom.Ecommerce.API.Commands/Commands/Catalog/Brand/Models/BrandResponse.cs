namespace Adanom.Ecommerce.API.Commands.Models
{
    public class BrandResponse : BaseResponseEntity<long>
    {
        public string Name { get; set; } = null!;

        public string UrlSlug { get; set; } = null!;

        public string? LogoPath { get; set; }

        public int DisplayOrder { get; set; }

        public MetaInformation? MetaInformation { get; set; }
    }
}