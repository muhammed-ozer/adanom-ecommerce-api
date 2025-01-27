using System.Security.Claims;
using NPOI.XSSF.UserModel;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class BatchUpdateProductPricesHandler : IRequestHandler<BatchUpdateProductPrices, bool>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public BatchUpdateProductPricesHandler(
            IMapper mapper,
            IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(BatchUpdateProductPrices command, CancellationToken cancellationToken)
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
                    continue;
                }

                var OriginalPriceAsString = row.Cells[1].ToString();
                _ = decimal.TryParse(OriginalPriceAsString!, out decimal originalPrice);

                var updateProductPrice_PriceRequest = new UpdateProductPrice_PriceRequest()
                {
                    Id = productSKU.ProductPriceId,
                    OriginalPrice = originalPrice
                };

                var updateProductPrice_PriceCommand = _mapper.Map(updateProductPrice_PriceRequest, new UpdateProductPrice_Price(command.Identity));

                try
                {
                    await _mediator.Send(updateProductPrice_PriceCommand);
                }
                catch
                {
                    continue;
                }

                command.AddCacheKey(CacheKeyConstants.ProductPrice.CacheKeyById(productSKU.ProductPriceId));
            }

            return true;
        }

        #endregion
    }
}
