namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ProductAttributeProfile : Profile
    {
        public ProductAttributeProfile()
        {
            CreateMap<ProductAttribute, ProductAttributeResponse>();

            CreateMap<ProductAttributeResponse, ProductAttribute>();

            CreateMap<CreateProductAttributeRequest, CreateProductAttribute>();

            CreateMap<CreateProductAttribute, ProductAttribute>();;
        }
    }
}