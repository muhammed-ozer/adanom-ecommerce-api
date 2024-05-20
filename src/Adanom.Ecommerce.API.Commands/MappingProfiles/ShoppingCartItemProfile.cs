namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ShoppingCartItemProfile : Profile
    {
        public ShoppingCartItemProfile()
        {
            CreateMap<ShoppingCartItem, ShoppingCartItemResponse>();

            CreateMap<ShoppingCartItemResponse, ShoppingCartItem>();
        }
    }
}