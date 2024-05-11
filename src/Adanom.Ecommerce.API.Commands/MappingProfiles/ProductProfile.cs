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

            CreateMap<UpdateProductDescriptionRequest, UpdateProductDescription>();

            CreateMap<UpdateProductDescription, Product>();

            CreateMap<UpdateProductBrandRequest, UpdateProductBrand>();

            CreateMap<UpdateProductBrand, Product>();

            CreateMap<UpdateProductIsActiveRequest, UpdateProductIsActive>();

            CreateMap<UpdateProductIsActive, Product>();

            CreateMap<UpdateProductIsNewRequest, UpdateProductIsNew>();

            CreateMap<UpdateProductIsNew, Product>();

            CreateMap<UpdateProductDisplayOrderRequest, UpdateProductDisplayOrder>();

            CreateMap<UpdateProductDisplayOrder, Product>();
        }
    }
}