using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateImageHandler : IRequestHandler<UpdateImage, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateImageHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateImage command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var image = await applicationDbContext.Images
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            if (command.IsDefault && !image.IsDefault)
            {
                var cuurentDefaultImage = await applicationDbContext.Images
                    .Where(e => e.EntityType == image.EntityType &&
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

            applicationDbContext.Update(image);

            try
            {
                await applicationDbContext.SaveChangesAsync();
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
