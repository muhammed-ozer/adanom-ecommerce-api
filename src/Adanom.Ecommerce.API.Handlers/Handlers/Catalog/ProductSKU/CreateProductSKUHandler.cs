using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductSKUHandler : IRequestHandler<CreateProductSKU, ProductSKUResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProductSKUHandler(
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

        public async Task<ProductSKUResponse?> Handle(CreateProductSKU command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productSKU = _mapper.Map<CreateProductSKU, ProductSKU>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.Code = source.Code.ToUpper();
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            var createProductPriceCommand = _mapper.Map(command.CreateProductPriceRequest, new CreateProductPrice(command.Identity));

            var productPriceResponse = await _mediator.Send(createProductPriceCommand);

            if (productPriceResponse == null)
            {
                return null;
            }

            productSKU.ProductPriceId = productPriceResponse.Id;

            await _applicationDbContext.AddAsync(productSKU);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.PRODUCTSKU,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.PRODUCTSKU,
                TransactionType = TransactionType.CREATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, productSKU.Id),
            }));

            var productSKUResponse = _mapper.Map<ProductSKUResponse>(productSKU);

            return productSKUResponse;
        }

        #endregion
    }
}
