namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ProductSpecificationAttributeGroupProfile : Profile
    {
        public ProductSpecificationAttributeGroupProfile()
        {
            CreateMap<ProductSpecificationAttributeGroup, ProductSpecificationAttributeGroupResponse>();

            CreateMap<ProductSpecificationAttributeGroupResponse, ProductSpecificationAttributeGroup>();

            CreateMap<CreateProductSpecificationAttributeGroupRequest, CreateProductSpecificationAttributeGroup>();

            CreateMap<CreateProductSpecificationAttributeGroup, ProductSpecificationAttributeGroup>();

            CreateMap<UpdateProductSpecificationAttributeGroupRequest, UpdateProductSpecificationAttributeGroup>();

            CreateMap<UpdateProductSpecificationAttributeGroup, ProductSpecificationAttributeGroup>();

            CreateMap<DeleteProductSpecificationAttributeGroupRequest, DeleteProductSpecificationAttributeGroup>();
        }
    }
}