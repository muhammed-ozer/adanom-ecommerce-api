namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ShoppingCartItemProfile : Profile
    {
        public ShoppingCartItemProfile()
        {
            CreateMap<ShoppingCartItem, ShoppingCartItemResponse>();

            CreateMap<ShoppingCartItemResponse, ShoppingCartItem>();

            CreateMap<CreateShoppingCartItemRequest, CreateShoppingCartItem>();

            CreateMap<CreateShoppingCartItem, ShoppingCartItem>();

            CreateMap<AnonymousShoppingCartItemResponse, CreateShoppingCartItemRequest>();

            CreateMap<UpdateShoppingCartItemRequest, UpdateShoppingCartItem>();

            CreateMap<UpdateShoppingCartItem, ShoppingCartItem>();

            CreateMap<DeleteShoppingCartItemRequest, DeleteShoppingCartItem>();
        }
    }
}