namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class AnonymousShoppingCartProfile : Profile
    {
        public AnonymousShoppingCartProfile()
        {
            CreateMap<AnonymousShoppingCart, AnonymousShoppingCartResponse>();

            CreateMap<AnonymousShoppingCartResponse, AnonymousShoppingCart>();
        }
    }
}