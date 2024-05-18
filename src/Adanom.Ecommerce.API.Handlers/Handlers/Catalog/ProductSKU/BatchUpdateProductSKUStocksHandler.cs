using System.Security.Claims;
using NPOI.XSSF.UserModel;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class BatchUpdateProductSKUStocksHandler : IRequestHandler<BatchUpdateProductSKUStocks, bool>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public BatchUpdateProductSKUStocksHandler(
            IMapper mapper,
            IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(BatchUpdateProductSKUStocks command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            using var stream = new MemoryStream(command.File.Content);

            var workbook = new XSSFWorkbook(stream);
            var worksheet = workbook.GetSheetAt(0);
            var rowCount = worksheet.LastRowNum;

            if (rowCount == 0)
            {
                return false;
            }

            for (int i = 1; i <= rowCount; i++)
            {
                var row = worksheet.GetRow(i);
                var productSKUCode = row.Cells[0].ToString();

                if (string.IsNullOrEmpty(productSKUCode))
                {
                    continue;
                }

                var productSKU = await _mediator.Send(new GetProductSKU(productSKUCode));

                if (productSKU == null)
                {
                    await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                    {
                        UserId = userId,
                        EntityType = EntityType.PRODUCTSKU,
                        TransactionType = TransactionType.UPDATE,
                        Description = string.Format(LogMessages.AdminTransaction.BatchUpdateProductSKUNotFound, productSKUCode),
                    }));

                    continue;
                }

                var stockQuantityAsString = row.Cells[1].ToString();
                _ = int.TryParse(stockQuantityAsString!, out int stockQuantity);

                var updateProductSKUStockRequest = new UpdateProductSKUStockRequest()
                {
                    Id = productSKU.Id,
                    StockQuantity = stockQuantity,
                    StockUnitType = productSKU.StockUnitType!.Key
                };

                var updateProductSKUStockCommand = _mapper.Map(updateProductSKUStockRequest, new UpdateProductSKUStock(command.Identity));

                try
                {
                    await _mediator.Send(updateProductSKUStockCommand);
                }
                catch
                {
                    await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                    {
                        UserId = userId,
                        EntityType = EntityType.PRODUCTSKU,
                        TransactionType = TransactionType.UPDATE,
                        Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed, productSKUCode),
                    }));

                    continue;
                }
            }

            return true;
        }

        #endregion
    }
}
