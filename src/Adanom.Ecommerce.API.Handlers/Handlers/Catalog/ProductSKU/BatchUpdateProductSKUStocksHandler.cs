using System.Security.Claims;
using NPOI.XSSF.UserModel;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class BatchUpdateProductSKUStocksHandler : IRequestHandler<BatchUpdateProductSKUStocks, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public BatchUpdateProductSKUStocksHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
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
                return true;
            }

            for (int i = 0; i <= rowCount; i++)
            {
                var row = worksheet.GetRow(i);
                var productSKUCode = row.Cells[0].ToString();
                var stockQuantityAsString = row.Cells[1].ToString();
                var stockQuantity = int.Parse(stockQuantityAsString!);

                var productSKU = await _applicationDbContext.ProductSKUs
                    .Where(e => e.DeletedAtUtc == null &&
                                e.Code == productSKUCode)
                    .SingleOrDefaultAsync();

                if (productSKU == null)
                {
                    // TODO: Log productSKU not found
                    continue;
                }

                productSKU.StockQuantity = stockQuantity;
                productSKU.UpdatedByUserId = userId;
                productSKU.UpdatedAtUtc = DateTime.UtcNow;

                try
                {
                    await _applicationDbContext.SaveChangesAsync();
                }
                catch (Exception exception)
                {
                    // TODO: Log exception to database
                    Log.Warning($"ProductSKU_Update_Failed: {exception.Message}");
                }
            }

            return true;
        }

        #endregion
    }
}
