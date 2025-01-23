namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ReturnRequestProfile : Profile
    {
        public ReturnRequestProfile()
        {
            CreateMap<ReturnRequest, ReturnRequestResponse>()
                .ForMember(member => member.ReturnRequestStatusType, options =>
                    options.MapFrom(e => new ReturnRequestStatusTypeResponse(e.ReturnRequestStatusType)))
                .ForMember(member => member.DeliveryType, options =>
                    options.MapFrom(e => new DeliveryTypeResponse(e.DeliveryType)));

            CreateMap<ReturnRequestResponse, ReturnRequest>()
                .ForMember(member => member.ReturnRequestStatusType, options =>
                    options.MapFrom(e => e.ReturnRequestStatusType.Key))
                .ForMember(member => member.DeliveryType, options =>
                    options.MapFrom(e => e.DeliveryType.Key));

            CreateMap<CreateReturnRequestRequest, CreateReturnRequest>();

            CreateMap<CreateReturnRequest, ReturnRequest>();

            CreateMap<UpdateReturnRequest_ReturnRequestStatusTypeRequest, UpdateReturnRequest_ReturnRequestStatusType>();

            CreateMap<UpdateReturnRequest_ReturnRequestStatusType, ReturnRequest>()
                .ForMember(member => member.ReturnRequestStatusType, options =>
                    options.MapFrom(e => e.NewReturnRequestStatusType));

            CreateMap<CancelReturnRequestRequest, CancelReturnRequest>();
        }
    }
}