using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_ProductTagHandler : IRequestHandler<CreateProduct_ProductTag, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateProduct_ProductTagHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateProduct_ProductTag command, CancellationToken cancellationToken)
        {
            var productTagResponse = await _mediator.Send(new GetProductTag(command.ProductTag_Value));

            if (productTagResponse == null)
            {
                var createProductTagRequest = new CreateProductTagRequest()
                {
                    Value = command.ProductTag_Value
                };

                var createProductTagCommand = _mapper.Map(createProductTagRequest, new CreateProductTag(command.Identity));

                productTagResponse = await _mediator.Send(createProductTagCommand);

                if (productTagResponse == null)
                {
                    return false;
                }
            }

            var product_ProductTag = new Product_ProductTag_Mapping()
            {
                ProductId = command.ProductId,
                ProductTagId = productTagResponse.Id
            };

            await _applicationDbContext.AddAsync(product_ProductTag);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.PRODUCT_PRODUCTTAG,
                    TransactionType = TransactionType.CREATE,
                    Description = string.Format(
                        LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful,
                        $"{product_ProductTag.ProductId}-{product_ProductTag.ProductTagId}"),
                }));
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.PRODUCT_PRODUCTTAG,
                    TransactionType = TransactionType.CREATE,
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
