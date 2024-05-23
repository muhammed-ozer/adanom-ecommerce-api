namespace Adanom.Ecommerce.API.Commands.Models
{
    public class BrandResponse : BaseResponseEntity<long>
    {
        public BrandResponse()
        {
            Images = new List<ImageResponse>();
        }

        public string Name { get; set; } = null!;

        public string UrlSlug { get; set; } = null!;

        public int DisplayOrder { get; set; }

        public ICollection<ImageResponse> Images { get; set; }

        public ImageResponse? Logo { get; set; }
    }
}