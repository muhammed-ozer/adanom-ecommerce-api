namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetReturnRequestsFilter
    {
        public string? Query { get; set; }

        public Guid? UserId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ReturnRequestStatusType? ReturnRequestStatusType { get; set; }

        [DefaultValue(GetReturnRequestsOrderByEnum.CREATED_AT_DESC)]
        public GetReturnRequestsOrderByEnum? OrderBy { get; set; }
    }
}