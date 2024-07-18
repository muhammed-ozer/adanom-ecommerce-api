namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class CheckoutProfile : Profile
    {
        public CheckoutProfile()
        {
            CreateMap<CheckoutRequest, GetCheckout>();
        }
    }
}