namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class PickUpStoreProfile : Profile
    {
        public PickUpStoreProfile()
        {
            CreateMap<PickUpStore, PickUpStoreResponse>();

            CreateMap<PickUpStoreResponse, PickUpStore>();

            CreateMap<CreatePickUpStoreRequest, CreatePickUpStore>();

            CreateMap<CreatePickUpStore, PickUpStore>();

            CreateMap<UpdatePickUpStoreRequest, UpdatePickUpStore>();

            CreateMap<UpdatePickUpStore, PickUpStore>();

            CreateMap<UpdatePickUpStoreLogoRequest, UpdatePickUpStoreLogo>();

            CreateMap<DeletePickUpStoreRequest, DeletePickUpStore>();
        }
    }
}