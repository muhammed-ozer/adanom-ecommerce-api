namespace Adanom.Ecommerce.API.Logging
{
    public interface ILogService
    {
        /// <summary>
        /// Create log for auth
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task CreateAsync(AuthLogRequest request);
    }
}
