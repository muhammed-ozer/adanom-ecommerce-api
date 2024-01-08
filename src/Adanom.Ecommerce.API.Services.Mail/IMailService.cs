namespace Adanom.Ecommerce.API.Services.Mail
{
    public interface IMailService
    {
        #region SendAsync

        Task SendAsync(MailRequest request);

        #endregion
    }
}
