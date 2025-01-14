using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateShippingProviderLogoHandler : IRequestHandler<UpdateShippingProviderLogo, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateShippingProviderLogoHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateShippingProviderLogo command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var shippingProvider = await applicationDbContext.ShippingProviders
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            var currentLogoImage = await _mediator.Send(new GetEntityImage(shippingProvider.Id, EntityType.SHIPPINGPROVIDER, ImageType.LOGO));

            if (currentLogoImage != null)
            {
                var deleteImageRequest = new DeleteImageRequest()
                {
                    Id = currentLogoImage.Id,
                };

                var deleteImageCommand = _mapper.Map(deleteImageRequest, new DeleteImage(command.Identity));

                var deleteImageResult = await _mediator.Send(deleteImageCommand);

                if (!deleteImageResult)
                {
                    return false;
                }
            }

            var createImageRequest = new CreateImageRequest()
            {
                File = command.File,
                EntityId = shippingProvider.Id,
                EntityType = EntityType.SHIPPINGPROVIDER,
                ImageType = ImageType.LOGO,
                EntityNameAsUrlSlug = shippingProvider.DisplayName.ConvertToUrlSlug()
            };

            var createImageCommand = _mapper
                    .Map(createImageRequest, new CreateImage(command.Identity));

            var createImageResponse = await _mediator.Send(createImageCommand);

            if (createImageResponse == null)
            {
                return false;
            }

            await _mediator.Publish(new ClearEntityCache<ShippingProviderResponse>());

            return true;
        }

        #endregion
    }
}
