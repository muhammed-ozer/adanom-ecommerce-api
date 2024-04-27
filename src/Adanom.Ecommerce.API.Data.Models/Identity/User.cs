using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            FavoriteItems = new List<FavoriteItem>();
            StockNotificationItems = new List<StockNotificationItem>();
            ShippingAddresses = new List<ShippingAddress>();
            BillingAddresses = new List<BillingAddress>();
            Orders = new List<Order>();
            ProductReviews = new List<ProductReview>();
            ReturnRequests = new List<ReturnRequest>();
        }

        [ProtectedPersonalData]
        public override string UserName { get; set; } = null!;

        [ProtectedPersonalData]
        public override string Email { get; set; } = null!;

        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [StringLength(100)]
        public string LastName { get; set; } = null!;

        [StringLength(10)]
        [ProtectedPersonalData]
        public new string PhoneNumber { get; set; } = null!;

        public byte DefaultDiscountRate { get; set; }

        public bool AllowCommercialEmails { get; set; }

        public DateTime? AllowCommercialEmailsUpdatedAtUtc { get; set; }

        public bool AllowCommercialSMS { get; set; }

        public DateTime? AllowCommercialSMSUpdatedAtUtc { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public ICollection<FavoriteItem> FavoriteItems { get; set; }

        public ICollection<StockNotificationItem> StockNotificationItems { get; set; }

        public ICollection<ShippingAddress> ShippingAddresses { get; set; }

        public ICollection<BillingAddress> BillingAddresses { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<ProductReview> ProductReviews { get; set; }

        public ICollection<ReturnRequest> ReturnRequests { get; set; }
    }
}
