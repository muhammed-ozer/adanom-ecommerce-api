using System.ComponentModel.DataAnnotations;
using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Logging.Models
{
    public class AuthLog : BaseLogEntity<long>
    {
        public string? UserEmail { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }
    }
}
