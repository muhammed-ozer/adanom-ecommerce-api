using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateFavoriteItemHandler : IRequestHandler<CreateFavoriteItem, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateFavoriteItemHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateFavoriteItem command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var favoriteItem = _mapper.Map<CreateFavoriteItem, FavoriteItem>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await _applicationDbContext.AddAsync(favoriteItem);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new CustomerTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.FAVORITEITEM,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.CustomerTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            return true;
        }

        #endregion
    }
}
