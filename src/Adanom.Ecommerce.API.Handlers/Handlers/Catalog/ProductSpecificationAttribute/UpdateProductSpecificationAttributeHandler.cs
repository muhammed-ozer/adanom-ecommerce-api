using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductSpecificationAttributeHandler : IRequestHandler<UpdateProductSpecificationAttribute, ProductSpecificationAttributeResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductSpecificationAttributeHandler(
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

        public async Task<ProductSpecificationAttributeResponse?> Handle(UpdateProductSpecificationAttribute command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productSpecificationAttribute = await _applicationDbContext.ProductSpecificationAttributes
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productSpecificationAttribute = _mapper.Map(command, productSpecificationAttribute);

            productSpecificationAttribute.UpdatedAtUtc = DateTime.UtcNow;
            productSpecificationAttribute.UpdatedByUserId = userId;

            _applicationDbContext.Update(productSpecificationAttribute);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.PRODUCTSPECIFICATIONATTRIBUTE,
                    TransactionType = TransactionType.UPDATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            var productSpecificationAttributeResponse = _mapper.Map<ProductSpecificationAttributeResponse>(productSpecificationAttribute);

            await _mediator.Publish(new UpdateFromCache<ProductSpecificationAttributeResponse>(productSpecificationAttributeResponse));

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.PRODUCTSPECIFICATIONATTRIBUTE,
                TransactionType = TransactionType.UPDATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, productSpecificationAttribute.Id),
            }));

            return productSpecificationAttributeResponse;
        }

        #endregion
    }
}
