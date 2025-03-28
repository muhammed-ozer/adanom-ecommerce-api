﻿using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(AnonymousShoppingCartId), nameof(ProductId), IsUnique = true)]
    public class AnonymousShoppingCartItem : BaseEntity<long>
    {
        public Guid AnonymousShoppingCartId { get; set; }

        public long ProductId { get; set; }

        public int Amount { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal? DiscountedPrice { get; set; }

        public Product Product { get; set; } = null!;

        public AnonymousShoppingCart AnonymousShoppingCart { get; set; } = null!;
    }
}
