using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [StringLength(50)]
        public string LastName { get; set; } = null!;
    }
}
