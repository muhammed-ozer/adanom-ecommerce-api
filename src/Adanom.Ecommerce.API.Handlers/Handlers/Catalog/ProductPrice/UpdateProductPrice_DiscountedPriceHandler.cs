using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductPrice_DiscountedPriceHandler : IRequestHandler<UpdateProductPrice_DiscountedPrice, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductPrice_DiscountedPriceHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateProductPrice_DiscountedPrice command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productPrice = await _applicationDbContext.ProductPrices
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productPrice = _mapper.Map(command, productPrice, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedByUserId = userId;
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            _applicationDbContext.Update(productPrice);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.PRODUCTPRICE,
                    TransactionType = TransactionType.UPDATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.PRODUCTPRICE,
                TransactionType = TransactionType.UPDATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, productPrice.Id),
            }));

            return true;
        }

        #endregion
    }
}
