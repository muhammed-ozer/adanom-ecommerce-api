using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_ProductSpecificationAttributeHandler : IRequestHandler<CreateProduct_ProductSpecificationAttribute, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProduct_ProductSpecificationAttributeHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateProduct_ProductSpecificationAttribute command, CancellationToken cancellationToken)
        {
            var product_ProductSpecificationAttribute = new Product_ProductSpecificationAttribute_Mapping()
            {
                ProductId = command.ProductId,
                ProductSpecificationAttributeId = command.ProductSpecificationAttributeId
            };

            await _applicationDbContext.AddAsync(product_ProductSpecificationAttribute);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.PRODUCT_PRODUCTSPECIFICATIONATTRIBUTE,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = command.Identity.GetUserId(),
                EntityType = EntityType.PRODUCT_PRODUCTSPECIFICATIONATTRIBUTE,
                TransactionType = TransactionType.CREATE,
                Description = string.Format(
                    LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful,
                    $"{product_ProductSpecificationAttribute.ProductId}-{product_ProductSpecificationAttribute.ProductSpecificationAttributeId}"),
            }));

            return true;
        }

        #endregion
    }
}
