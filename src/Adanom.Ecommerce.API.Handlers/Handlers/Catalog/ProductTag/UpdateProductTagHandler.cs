using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductTagHandler : IRequestHandler<UpdateProductTag, ProductTagResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductTagHandler(
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

        public async Task<ProductTagResponse?> Handle(UpdateProductTag command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productTag = await _applicationDbContext.ProductTags
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            command.Value = command.Value.ToLower(CultureInfoConstants.TurkishCulture);

            productTag = _mapper.Map(command, productTag);

            productTag.UpdatedAtUtc = DateTime.UtcNow;
            productTag.UpdatedByUserId = userId;

            _applicationDbContext.Update(productTag);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.PRODUCTTAG,
                    TransactionType = TransactionType.UPDATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            var productTagResponse = _mapper.Map<ProductTagResponse>(productTag);

            await _mediator.Publish(new UpdateFromCache<ProductTagResponse>(productTagResponse));

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.PRODUCTTAG,
                TransactionType = TransactionType.UPDATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, productTag.Id),
            }));

            return productTagResponse;
        }

        #endregion
    }
}
