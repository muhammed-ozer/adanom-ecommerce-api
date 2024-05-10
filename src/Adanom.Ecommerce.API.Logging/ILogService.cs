namespace Adanom.Ecommerce.API.Logging
{
    public interface ILogService
    {
        Task CreateAsync(BaseLogRequest request);
    }
}
