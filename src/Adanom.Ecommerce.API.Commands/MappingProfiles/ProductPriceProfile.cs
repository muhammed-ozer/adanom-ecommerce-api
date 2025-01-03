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

            //CreateMap<UpdateProductPrice_DiscountedPriceRequest, UpdateProductPrice_DiscountedPrice>();

            //CreateMap<UpdateProductPrice_DiscountedPrice, ProductPrice>();

            CreateMap<UpdateProductPriceTaxCategoryRequest, UpdateProductPriceTaxCategory>();

            CreateMap<UpdateProductPriceTaxCategory, ProductPrice>();

            CreateMap<DeleteProductPriceRequest, DeleteProductPrice>();

            CreateMap<BatchUpdateProductPricesRequest, BatchUpdateProductPrices>();
        }
    }
}