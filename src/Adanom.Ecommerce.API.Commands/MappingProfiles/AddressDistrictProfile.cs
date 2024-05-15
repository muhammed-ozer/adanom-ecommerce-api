namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class AddressDistrictProfile : Profile
    {
        public AddressDistrictProfile()
        {
            CreateMap<AddressDistrict, AddressDistrictResponse>();

            CreateMap<AddressDistrictResponse, AddressDistrict>();
        }
    }
}