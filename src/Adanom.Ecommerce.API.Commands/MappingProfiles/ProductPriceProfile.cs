namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ProductPriceProfile : Profile
    {
        public ProductPriceProfile()
        {
            CreateMap<ProductPrice, ProductPriceResponse>();

            CreateMap<ProductPriceResponse, ProductPrice>();

            CreateMap<CreateProductPriceRequest, CreateProductPrice>();

            CreateMap<CreateProductPrice, ProductPrice>();;
        }
    }
}