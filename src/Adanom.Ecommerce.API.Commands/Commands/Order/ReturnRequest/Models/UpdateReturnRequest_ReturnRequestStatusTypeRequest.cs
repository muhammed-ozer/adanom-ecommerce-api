namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateReturnRequest_ReturnRequestStatusTypeRequest
    {
        public long Id { get; set; }

        public ReturnRequestStatusType NewReturnRequestStatusType { get; set; }

        public string? ShippingTrackingCode { get; set; }

        public string? DisapprovedReasonMessage { get; set; }
    }
}