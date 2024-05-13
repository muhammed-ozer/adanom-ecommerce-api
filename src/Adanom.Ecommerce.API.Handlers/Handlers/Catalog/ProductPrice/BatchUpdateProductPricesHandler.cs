using System.Security.Claims;
using NPOI.XSSF.UserModel;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class BatchUpdateProductPricesHandler : IRequestHandler<BatchUpdateProductPrices, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public BatchUpdateProductPricesHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
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
                return true;
            }

            for (int i = 0; i <= rowCount; i++)
            {
                var row = worksheet.GetRow(i);
                var productSKUCode = row.Cells[0].ToString();
                var priceAsString = row.Cells[1].ToString();
                var price = decimal.Parse(priceAsString!);

                var productSKU = await _applicationDbContext.ProductSKUs
                    .Where(e => e.DeletedAtUtc == null &&
                                e.Code == productSKUCode)
                    .SingleOrDefaultAsync();

                if (productSKU == null)
                {
                    // TODO: Log productSKU not found
                    continue;
                }

                var updateProductPrice_PriceRequest = new UpdateProductPrice_PriceRequest()
                {
                    Id = productSKU.ProductPriceId,
                    OriginalPrice = price
                };

                var updateProductPrice_PriceCommand = _mapper.Map(updateProductPrice_PriceRequest, new UpdateProductPrice_Price(command.Identity));

                try
                {
                    await _mediator.Send(updateProductPrice_PriceCommand);
                }
                catch (Exception exception)
                {
                    // TODO: Log exception
                    continue;
                }
            }

            return true;
        }

        #endregion
    }
}
