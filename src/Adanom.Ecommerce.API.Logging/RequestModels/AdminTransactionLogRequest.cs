using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Logging
{
    public class AdminTransactionLogRequest : BaseLogRequest
    {
        public EntityType EntityType { get; set; }

        public TransactionType TransactionType { get; set; }

        public Guid UserId { get; set; }
    }
}
