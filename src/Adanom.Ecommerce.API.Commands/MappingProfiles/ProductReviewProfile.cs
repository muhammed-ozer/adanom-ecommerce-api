namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ProductReviewProfile : Profile
    {
        public ProductReviewProfile()
        {
            CreateMap<ProductReview, ProductReviewResponse>();

            CreateMap<ProductReviewResponse, ProductReview>();

            CreateMap<UpdateProductReviewRequest, UpdateProductReview>();

            CreateMap<UpdateProductReview, ProductReview>();
        }
    }
}