using System.Security.Claims;
using Adanom.Ecommerce.API.Services.Azure;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteImageHandler : IRequestHandler<DeleteImage, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IBlobStorageService _blobStorageService;

        #endregion

        #region Ctor

        public DeleteImageHandler(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper, 
            IMediator mediator,
            IBlobStorageService blobStorageService)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _blobStorageService = blobStorageService ?? throw new ArgumentNullException(nameof(blobStorageService));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteImage command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var image = await _applicationDbContext.Images
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            if (image.IsDefault)
            {
                var randomEntityImage = await _applicationDbContext.Images
                    .Where(e => e.EntityType == image.EntityType &&
                                e.EntityId == image.EntityId)
                    .OrderBy(e => e.DisplayOrder)
                    .FirstOrDefaultAsync();

                if (randomEntityImage != null)
                {
                    randomEntityImage.IsDefault = true;
                }

                image.IsDefault = false;
            }

            var deleteImageResponse = await _blobStorageService.DeleteFileAsync(image.Path);

            _applicationDbContext.Remove(image);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.IMAGE,
                    TransactionType = TransactionType.DELETE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.IMAGE,
                TransactionType = TransactionType.DELETE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, image.Id),
            }));

            return true;
        }

        #endregion
    }
}
