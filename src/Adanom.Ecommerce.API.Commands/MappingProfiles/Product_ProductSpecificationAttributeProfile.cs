namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class Product_ProductSpecificationAttributeProfile : Profile
    {
        public Product_ProductSpecificationAttributeProfile()
        {
            CreateMap<CreateProduct_ProductSpecificationAttributeRequest, CreateProduct_ProductSpecificationAttribute>();

            CreateMap<DeleteProduct_ProductSpecificationAttributeRequest, DeleteProduct_ProductSpecificationAttribute>();
        }
    }
}