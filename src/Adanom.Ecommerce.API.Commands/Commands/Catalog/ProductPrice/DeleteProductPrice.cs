using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteProductPrice : IRequest<bool>, ICacheInvalidator
    {
        #region Ctor

        public DeleteProductPrice(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        #endregion

        #region ICacheInvalidator Properties

        public string[] CacheKeys => [$"{CacheKeyConstants.ProductPrice.CacheKeyById(Id)}"];

        public string Region => CacheKeyConstants.ProductPrice.Region;

        public bool InvalidateRegion => false;

        public string? Pattern => $"{CacheKeyConstants.ProductPrice.ByProductIdPattern}";

        #endregion
    }
}