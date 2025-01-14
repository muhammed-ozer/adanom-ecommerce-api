using System.ComponentModel.DataAnnotations;
using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Logging.Models
{
    public class TransactionLog : BaseLogEntity<long>
    {
        public Guid? UserId { get; set; }

        [StringLength(500)]
        public string CommandName { get; set; } = null!;

        [StringLength(5000)]
        public string CommandValues { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }
    }
}
