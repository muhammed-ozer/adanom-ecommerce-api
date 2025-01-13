using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_ProductCategoryHandler : IRequestHandler<CreateProduct_ProductCategory, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProduct_ProductCategoryHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateProduct_ProductCategory command, CancellationToken cancellationToken)
        {
            var product_ProductCategory = new Product_ProductCategory_Mapping()
            {
                ProductId = command.ProductId,
                ProductCategoryId = command.ProductCategoryId
            };

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(product_ProductCategory);

            try
            {
                await applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.PRODUCT_PRODUCTCATEGORY,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = command.Identity.GetUserId(),
                EntityType = EntityType.PRODUCT_PRODUCTCATEGORY,
                TransactionType = TransactionType.CREATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, $"{product_ProductCategory.ProductId}-{product_ProductCategory.ProductCategoryId}"),
            }));

            return true;
        }

        #endregion
    }
}
