namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class Product_ProductTagProfile : Profile
    {
        public Product_ProductTagProfile()
        {
            CreateMap<CreateProduct_ProductTagRequest, CreateProduct_ProductTag>();

            CreateMap<DeleteProduct_ProductTagRequest, DeleteProduct_ProductTag>();

            CreateMap<Product_ProductTag_Mapping, DeleteProduct_ProductTagRequest>();
        }
    }
}