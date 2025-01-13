using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetShoppingCart : IRequest<ShoppingCartResponse?>
    {
        #region Ctor

        public GetShoppingCart(long id, bool updateItems)
        {
            Id = id;
            UpdateItems = updateItems;
        }

        public GetShoppingCart(Guid userId, bool updateItems)
        {
            UserId = userId;
            UpdateItems = updateItems;
        }

        public GetShoppingCart(ClaimsPrincipal identity, bool includeItems, bool includeSummary, bool updateItems, OrderPaymentType? orderPaymentType = null)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            IncludeItems = includeItems;
            IncludeSummary = includeSummary;
            UpdateItems = updateItems;
            OrderPaymentType = orderPaymentType;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public Guid? UserId { get; set; }

        public ClaimsPrincipal? Identity { get; }

        public bool IncludeItems { get; }

        public bool IncludeSummary { get; }

        public bool UpdateItems { get; }

        public OrderPaymentType? OrderPaymentType { get; set; }

        #endregion
    }
}
