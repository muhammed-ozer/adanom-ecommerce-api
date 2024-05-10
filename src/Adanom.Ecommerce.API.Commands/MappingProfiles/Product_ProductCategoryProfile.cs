namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class Product_ProductCategoryProfile : Profile
    {
        public Product_ProductCategoryProfile()
        {
            CreateMap<CreateProduct_ProductCategoryRequest, CreateProduct_ProductCategory>();

            CreateMap<DeleteProduct_ProductCategoryRequest, DeleteProduct_ProductCategory>();
        }
    }
}