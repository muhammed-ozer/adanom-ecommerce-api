using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductCategoryHandler : IRequestHandler<CreateProductCategory, ProductCategoryResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProductCategoryHandler(
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

        public async Task<ProductCategoryResponse?> Handle(CreateProductCategory command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await _applicationDbContext.Database.BeginTransactionAsync();

            var productCategory = _mapper.Map<CreateProductCategory, ProductCategory>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UrlSlug = command.Name.ConvertToUrlSlug();
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await _applicationDbContext.AddAsync(productCategory);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.PRODUCTCATEGORY,
                    TransactionType = TransactionType.CREATE,
                    Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, productCategory.Id),
                }));
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.PRODUCTCATEGORY,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            var productCategoryResponse = _mapper.Map<ProductCategoryResponse>(productCategory);

            await _mediator.Publish(new AddToCache<ProductCategoryResponse>(productCategoryResponse));

            return productCategoryResponse;
        }

        #endregion
    }
}
