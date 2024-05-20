namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class AnonymousShoppingCartItemProfile : Profile
    {
        public AnonymousShoppingCartItemProfile()
        {
            CreateMap<AnonymousShoppingCartItem, AnonymousShoppingCartItemResponse>();

            CreateMap<AnonymousShoppingCartItemResponse, AnonymousShoppingCartItem>();
        }
    }
}