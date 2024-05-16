using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateSliderItem : IRequest<bool>
    {
        #region Ctor

        public UpdateSliderItem(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public string? Url { get; set; }

        #endregion
    }
}