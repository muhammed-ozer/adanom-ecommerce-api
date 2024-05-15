namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class AddressCityProfile : Profile
    {
        public AddressCityProfile()
        {
            CreateMap<AddressCity, AddressCityResponse>();

            CreateMap<AddressCityResponse, AddressCity>();
        }
    }
}