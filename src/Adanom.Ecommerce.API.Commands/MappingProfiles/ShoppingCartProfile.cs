namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartResponse>();

            CreateMap<ShoppingCartResponse, ShoppingCart>();

            CreateMap<UpdateShoppingCartItemsResponse, ShoppingCartResponse>();

            CreateMap<MigrateAnonymousShoppingCartToShoppingCartRequest, MigrateAnonymousShoppingCartToShoppingCart>();

            CreateMap<ShoppingCartSummaryResponse, CheckoutResponse>();
        }
    }
}