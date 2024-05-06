namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ProductTagProfile : Profile
    {
        public ProductTagProfile()
        {
            CreateMap<ProductTag, ProductTagResponse>();

            CreateMap<ProductTagResponse, ProductTag>();

            CreateMap<CreateProductTagRequest, CreateProductTag>();

            CreateMap<CreateProductTag, ProductTag>();

            CreateMap<UpdateProductTagRequest, UpdateProductTag>();

            CreateMap<UpdateProductTag, ProductTag>();

            CreateMap<DeleteProductTagRequest, DeleteProductTag>();
        }
    }
}