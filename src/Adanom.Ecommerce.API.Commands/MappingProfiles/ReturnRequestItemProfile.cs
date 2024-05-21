namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ReturnRequestItemProfile : Profile
    {
        public ReturnRequestItemProfile()
        {
            CreateMap<ReturnRequestItem, ReturnRequestItemResponse>();

            CreateMap<ReturnRequestItemResponse, ReturnRequestItem>();
        }
    }
}