namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ProductPriceProfile : Profile
    {
        public ProductPriceProfile()
        {
            CreateMap<ProductPrice, ProductPriceResponse>();

            CreateMap<ProductPriceResponse, ProductPrice>();

            CreateMap<CreateProductPriceRequest, CreateProductPrice>();

            CreateMap<CreateProductPrice, ProductPrice>();

            CreateMap<UpdateProductPrice_PriceRequest, UpdateProductPrice_Price>();

            CreateMap<UpdateProductPrice_Price, ProductPrice>();

            CreateMap<UpdateProductPriceTaxCategoryRequest, UpdateProductPriceTaxCategory>();

            CreateMap<UpdateProductPriceTaxCategory, ProductPrice>();
        }
    }
}