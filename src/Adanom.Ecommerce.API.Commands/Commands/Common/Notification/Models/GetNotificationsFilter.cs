namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetNotificationsFilter
    {
        public string? Query { get; set; }

        public bool? UnRead { get; set; }

        public NotificationType? NotificationType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [DefaultValue(GetNotificationsOrderByEnum.CREATED_AT_DESC)]
        public GetNotificationsOrderByEnum? OrderBy { get; set; }
    }
}