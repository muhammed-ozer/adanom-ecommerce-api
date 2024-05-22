namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationResponse>()
                .ForMember(member => member.NotificationType, options =>
                    options.MapFrom(e => new NotificationTypeResponse(e.NotificationType)));

            CreateMap<NotificationResponse, Notification>()
                .ForMember(member => member.NotificationType, options =>
                    options.MapFrom(e => e.NotificationType.Key));
        }
    }
}