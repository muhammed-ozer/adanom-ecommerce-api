namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class SliderItemProfile : Profile
    {
        public SliderItemProfile()
        {
            CreateMap<SliderItem, SliderItemResponse>()
                .ForMember(member => member.SliderItemType, options =>
                    options.MapFrom(e => new SliderItemTypeResponse(e.SliderItemType)));

            CreateMap<SliderItemResponse, SliderItem>()
                .ForMember(member => member.SliderItemType, options =>
                    options.MapFrom(e => e.SliderItemType.Key));

            CreateMap<CreateSliderItemRequest, CreateSliderItem>();

            CreateMap<CreateSliderItem, SliderItem>();

            CreateMap<UpdateSliderItemRequest, UpdateSliderItem>();

            CreateMap<UpdateSliderItem, SliderItem>();

            CreateMap<DeleteSliderItemRequest, DeleteSliderItem>();
        }
    }
}