using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateSliderItem : IRequest<SliderItemResponse?>
    {
        #region Ctor

        public CreateSliderItem(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public UploadedFile File { get; set; } = null!;

        public SliderItemType SliderItemType { get; set; }

        public string Name { get; set; } = null!;

        public string? Url { get; set; }

        #endregion
    }
}