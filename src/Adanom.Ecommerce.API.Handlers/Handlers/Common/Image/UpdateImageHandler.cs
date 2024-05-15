using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateImageHandler : IRequestHandler<UpdateImage, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateImageHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateImage command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var image = await _applicationDbContext.Images
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            if (command.IsDefault && !image.IsDefault)
            {
                var cuurentDefaultImage = await _applicationDbContext.Images
                    .Where(e => e.DeletedAtUtc == null &&
                                e.EntityType == image.EntityType &&
                                e.EntityId == image.EntityId &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (cuurentDefaultImage != null)
                {
                    cuurentDefaultImage.IsDefault = false;
                }
            }
            else
            {
                command.IsDefault = true;
            }

            image = _mapper.Map(command, image);

            _applicationDbContext.Update(image);

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
                    TransactionType = TransactionType.UPDATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));
                return false;
            }

            return true;
        }

        #endregion
    }
}
