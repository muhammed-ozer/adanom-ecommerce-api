using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdatePickUpStoreLogoHandler : IRequestHandler<UpdatePickUpStoreLogo, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdatePickUpStoreLogoHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdatePickUpStoreLogo command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var pickUpStore = await _applicationDbContext.PickUpStores
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            var currentLogoImage = await _mediator.Send(new GetEntityImage(pickUpStore.Id, EntityType.PICKUPSTORE, ImageType.LOGO));
            
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
                EntityId = pickUpStore.Id,
                EntityType = EntityType.PICKUPSTORE,
                ImageType = ImageType.LOGO,
                EntityNameAsUrlSlug = pickUpStore.DisplayName.ConvertToUrlSlug()
            };

            var createImageCommand = _mapper
                    .Map(createImageRequest, new CreateImage(command.Identity));

            var createImageResponse = await _mediator.Send(createImageCommand);

            if (createImageResponse == null)
            {
                return false;
            }

            await _mediator.Publish(new ClearEntityCache<PickUpStoreResponse>());

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.PICKUPSTORE,
                TransactionType = TransactionType.UPDATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, pickUpStore.Id),
            }));

            return true;
        }

        #endregion
    }
}
