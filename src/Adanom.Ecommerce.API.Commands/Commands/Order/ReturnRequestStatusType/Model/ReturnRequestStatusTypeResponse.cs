namespace Adanom.Ecommerce.API.Commands.Models
{
    public sealed class ReturnRequestStatusTypeResponse
    {
        public ReturnRequestStatusTypeResponse(ReturnRequestStatusType key)
        {
            Key = key;
        }

        public ReturnRequestStatusType Key { get; }

        public string Name { get; set; } = null!;
    }
}