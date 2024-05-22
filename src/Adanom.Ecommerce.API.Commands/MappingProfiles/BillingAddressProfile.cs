namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class BillingAddressProfile : Profile
    {
        public BillingAddressProfile()
        {
            CreateMap<BillingAddress, BillingAddressResponse>();

            CreateMap<BillingAddressResponse, BillingAddress>();
        }
    }
}