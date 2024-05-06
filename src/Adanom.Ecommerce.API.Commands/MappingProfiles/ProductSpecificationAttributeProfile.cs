namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ProductSpecificationAttributeProfile : Profile
    {
        public ProductSpecificationAttributeProfile()
        {
            CreateMap<ProductSpecificationAttribute, ProductSpecificationAttributeResponse>();

            CreateMap<ProductSpecificationAttributeResponse, ProductSpecificationAttribute>();

            CreateMap<CreateProductSpecificationAttributeRequest, CreateProductSpecificationAttribute>();

            CreateMap<CreateProductSpecificationAttribute, ProductSpecificationAttribute>();

            CreateMap<UpdateProductSpecificationAttributeRequest, UpdateProductSpecificationAttribute>();

            CreateMap<UpdateProductSpecificationAttribute, ProductSpecificationAttribute>();

            CreateMap<DeleteProductSpecificationAttributeRequest, DeleteProductSpecificationAttribute>();
        }
    }
}