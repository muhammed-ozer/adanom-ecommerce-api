namespace Adanom.Ecommerce.API.Logging
{
    public class TransactionLogRequest : BaseLogRequest
    {
        public Guid? UserId { get; set; }

        public string CommandName { get; set; } = null!;

        public string CommandValues { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }
    }
}
