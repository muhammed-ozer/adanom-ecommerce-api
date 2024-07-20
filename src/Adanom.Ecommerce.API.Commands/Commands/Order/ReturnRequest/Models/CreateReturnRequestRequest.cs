namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateReturnRequestRequest
    {
        public CreateReturnRequestRequest()
        {
            CreateReturnRequest_ItemRequests = new List<CreateReturnRequest_ItemRequest>();
        }

        public long OrderId { get; set; }

        public ICollection<CreateReturnRequest_ItemRequest> CreateReturnRequest_ItemRequests { get; set; }
    }
}