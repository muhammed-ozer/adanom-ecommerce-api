using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateLocalDeliveryProviderLogoHandler : IRequestHandler<UpdateLocalDeliveryProviderLogo, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateLocalDeliveryProviderLogoHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateLocalDeliveryProviderLogo command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var localDeliveryProvider = await _applicationDbContext.LocalDeliveryProviders
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            var currentLogoImage = await _mediator.Send(new GetEntityImage(localDeliveryProvider.Id, EntityType.LOCALDELIVERYPROVIDER, ImageType.LOGO));
            
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
                EntityId = localDeliveryProvider.Id,
                EntityType = EntityType.LOCALDELIVERYPROVIDER,
                ImageType = ImageType.LOGO,
                EntityNameAsUrlSlug = localDeliveryProvider.DisplayName.ConvertToUrlSlug()
            };

            var createImageCommand = _mapper
                    .Map(createImageRequest, new CreateImage(command.Identity));

            var createImageResponse = await _mediator.Send(createImageCommand);

            if (createImageResponse == null)
            {
                return false;
            }

            await _mediator.Publish(new ClearEntityCache<LocalDeliveryProviderResponse>());

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.LOCALDELIVERYPROVIDER,
                TransactionType = TransactionType.UPDATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, localDeliveryProvider.Id),
            }));

            return true;
        }

        #endregion
    }
}
