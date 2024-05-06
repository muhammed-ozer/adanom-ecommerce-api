namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryResponse>();

            CreateMap<ProductCategoryResponse, ProductCategory>();

            CreateMap<CreateProductCategoryRequest, CreateProductCategory>();

            CreateMap<CreateProductCategory, ProductCategory>();

            CreateMap<UpdateProductCategoryRequest, UpdateProductCategory>();

            CreateMap<UpdateProductCategory, ProductCategory>();

            CreateMap<DeleteProductCategoryRequest, DeleteProductCategory>();
        }
    }
}