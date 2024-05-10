namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>();

            CreateMap<ProductResponse, Product>();

            CreateMap<CreateProductRequest, CreateProduct>();

            CreateMap<CreateProduct, Product>();

            CreateMap<UpdateProductNameRequest, UpdateProductName>();

            CreateMap<UpdateProductName, Product>();
        }
    }
}