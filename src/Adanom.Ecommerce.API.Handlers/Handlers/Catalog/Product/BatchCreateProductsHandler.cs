using System.Security.Claims;
using NPOI.XSSF.UserModel;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class BatchCreateProductsHandler : IRequestHandler<BatchCreateProducts, bool>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public BatchCreateProductsHandler(
            IMapper mapper,
            IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(BatchCreateProducts command, CancellationToken cancellationToken)
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

                var productCode = row.Cells[0].ToString();
                var productName = row.Cells[1].ToString()!;
                var productDescription = row.Cells[2].ToString();

                if (CheckStringValueIsNull(productDescription))
                {
                    productDescription = null;
                }

                _ = int.TryParse(row.Cells[3].ToString(), out int productCategoryId);
                _ = int.TryParse(row.Cells[4].ToString(), out int brandId);
                _ = int.TryParse(row.Cells[5].ToString(), out int stockQuantity);

                _ = Enum.TryParse(row.Cells[6].ToString(), out StockUnitType stockUnitType);

                var barcodes = row.Cells[7].ToString();

                if (CheckStringValueIsNull(barcodes))
                {
                    barcodes = null;
                }

                _ = bool.TryParse(row.Cells[8].ToString(), out bool isEligibleToInstallment);
                _ = byte.TryParse(row.Cells[9].ToString(), out byte maximumInstallmentCount);
                _ = decimal.TryParse(row.Cells[10].ToString(), out decimal originalPrice);
                _ = int.TryParse(row.Cells[11].ToString(), out int taxCategoryId);

                var createProductRequest = new CreateProductRequest()
                {
                    Name = productName,
                    Description = productDescription,
                    BrandId = brandId > 0 ? brandId : null,
                    ProductCategoryId = productCategoryId,
                    IsActive = false,
                    IsNew = false,
                    DisplayOrder = 1,
                    CreateProductSKURequest = new CreateProductSKURequest()
                    {
                        Code = productCode!,
                        StockQuantity = stockQuantity,
                        StockUnitType = stockUnitType,
                        Barcodes = barcodes,
                        IsEligibleToInstallment = isEligibleToInstallment,
                        MaximumInstallmentCount = maximumInstallmentCount,
                        CreateProductPriceRequest = new CreateProductPriceRequest()
                        {
                            OriginalPrice = originalPrice,
                            TaxCategoryId = taxCategoryId,
                        }
                    }
                };

                var createProductCommand = _mapper.Map(createProductRequest, new CreateProduct(command.Identity));

                try
                {
                    var createProductResponse = await _mediator.Send(createProductCommand);
                } catch
                {
                    continue;
                }
            }

            return true;
        }

        #endregion

        #region CheckStringEqualsNullValue

        private bool CheckStringValueIsNull(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            if (value.Equals("NULL", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
