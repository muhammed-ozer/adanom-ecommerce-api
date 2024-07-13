namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class BillingAddressProfile : Profile
    {
        public BillingAddressProfile()
        {
            CreateMap<BillingAddress, BillingAddressResponse>();

            CreateMap<BillingAddressResponse, BillingAddress>();

            CreateMap<CreateBillingAddressRequest, CreateBillingAddress>();

            CreateMap<CreateBillingAddress, BillingAddress>();

            CreateMap<UpdateBillingAddressRequest, UpdateBillingAddress>();

            CreateMap<UpdateBillingAddress, BillingAddress>();

            CreateMap<DeleteBillingAddressRequest, DeleteBillingAddress>();
        }
    }
}