using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductSKU_ProductAttributeHandler : IRequestHandler<UpdateProductSKU_ProductAttribute, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public UpdateProductSKU_ProductAttributeHandler(
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

        public async Task<bool> Handle(UpdateProductSKU_ProductAttribute command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productSKU = await _applicationDbContext.ProductSKUs
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .Include(e => e.ProductAttribute)
                .SingleAsync();

            if (productSKU.ProductAttribute == null && productSKU.ProductAttributeId == null)
            {
                var createProductAttributeRequest = new CreateProductAttributeRequest()
                {
                    Name = command.Name,
                    Value = command.Value,
                    DisplayOrder = command.DisplayOrder
                };

                var createProductAttributeCommand = _mapper.Map(createProductAttributeRequest, new CreateProductAttribute(command.Identity));

                var productAttributeResponse = await _mediator.Send(createProductAttributeCommand);

                if (productAttributeResponse == null)
                {
                    return false;
                }

                productSKU.ProductAttributeId = productAttributeResponse.Id;

                _applicationDbContext.Update(productSKU);
            }
            else
            {
                var updateProductAttributeRequest = new UpdateProductAttributeRequest()
                {
                    Id = productSKU.ProductAttributeId!.Value,
                    Name = command.Name,
                    Value = command.Value,
                    DisplayOrder = command.DisplayOrder
                };

                var updateProductAttributeCommand = _mapper.Map(updateProductAttributeRequest, new UpdateProductAttribute(command.Identity));

                var updateProductAttributeResponse = await _mediator.Send(updateProductAttributeCommand);

                if (!updateProductAttributeResponse)
                {
                    return false;
                }
            }

            productSKU.UpdatedByUserId = userId;
            productSKU.UpdatedAtUtc = DateTime.UtcNow;

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.PRODUCTSKU,
                    TransactionType = TransactionType.UPDATE,
                    Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, productSKU.Id),
                }));
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.PRODUCTSKU,
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
