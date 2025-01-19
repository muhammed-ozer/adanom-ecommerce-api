namespace Adanom.Ecommerce.API.Services
{
    public interface IPdfGeneratorService
    {
        byte[] GeneratePdf(string htmlContent, string fileName);
    }
}
