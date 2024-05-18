namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetUsersFilter
    {
        public string? Query { get; set; }

        public bool? EmailConfirmed { get; set; }

        public bool? AllowCommercialEmails { get; set; }

        public bool? AllowCommercialSMS { get; set; }

        [DefaultValue(GetUsersOrderByEnum.CREATED_AT_DESC)]
        public GetUsersOrderByEnum? OrderBy { get; set; }
    }
}