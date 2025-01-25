namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class Product_ProductSKUProfile : Profile
    {
        public Product_ProductSKUProfile()
        {
            CreateMap<CreateProduct_ProductSKURequest, CreateProduct_ProductSKU>();

            CreateMap<DeleteProduct_ProductSKURequest, DeleteProduct_ProductSKU>();

            CreateMap<Product_ProductSKU_Mapping, DeleteProduct_ProductSKURequest>();
        }
    }
}