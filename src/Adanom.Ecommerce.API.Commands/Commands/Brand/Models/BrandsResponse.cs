namespace Adanom.Ecommerce.API.Commands.Models
{
    public class BrandResponse
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string UrlSlug { get; set; } = null!;

        public string? LogoPath { get; set; }

        public int DisplayOrder { get; set; }
    }
}