using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductReviewHandler : IRequestHandler<UpdateProductReview, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductReviewHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateProductReview command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productReview = await _applicationDbContext.ProductReviews
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            productReview = _mapper.Map(command, productReview, options =>
            {
                options.BeforeMap((source, target) =>
                {
                    if (source.IsApproved && !target.IsApproved)
                    {
                        target.ApprovedByUserId = userId;
                        target.ApprovedAtUtc = DateTime.UtcNow;
                    }
                });
            });

            _applicationDbContext.Update(productReview);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.PRODUCTREVIEW,
                    TransactionType = TransactionType.UPDATE,
                    Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, productReview.Id),
                }));
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.PRODUCTREVIEW,
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
