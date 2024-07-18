using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrder : IRequest<OrderResponse?>
    {
        #region Ctor

        public GetOrder(long id)
        {
            Id = id;
        }

        public GetOrder(string orderNumber)
        {
            OrderNumber = orderNumber;
        }

        public GetOrder(ClaimsPrincipal identity, long id)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            Id = id;
        }

        public GetOrder(ClaimsPrincipal identity, string orderNumber)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            OrderNumber = orderNumber;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public string? OrderNumber { get; set; }

        public ClaimsPrincipal? Identity { get; }

        #endregion
    }
}
