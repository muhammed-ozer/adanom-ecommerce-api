using DinkToPdf;
using DinkToPdf.Contracts;

namespace Adanom.Ecommerce.API.Services.Implementations
{
    internal sealed class PdfGeneratorService : IPdfGeneratorService
    {
        #region Fields

        private readonly IConverter _converter;

        #endregion

        #region Ctor

        public PdfGeneratorService(IConverter converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        #endregion

        #region IPdfGeneratorService Members

        public byte[] GeneratePdf(string htmlContent, string fileName)
        {
            var document = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
            },
                Objects =
                {
                    new ObjectSettings()
                    {
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            return _converter.Convert(document);
        }

        #endregion
    }
}
