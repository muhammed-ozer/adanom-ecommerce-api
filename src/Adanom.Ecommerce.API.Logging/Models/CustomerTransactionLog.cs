using System.ComponentModel.DataAnnotations;
using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Logging.Models
{
    public class CustomerTransactionLog : BaseLogEntity<long>
    {
        public LogLevel LogLevel { get; set; }

        public EntityType EntityType { get; set; }

        public TransactionType TransactionType { get; set; }

        public Guid UserId { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }
    }
}
