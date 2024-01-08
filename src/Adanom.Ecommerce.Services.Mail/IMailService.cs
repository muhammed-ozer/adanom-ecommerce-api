namespace Adanom.Ecommerce.Services.Mail
{
    public interface IMailService
    {
        #region SendAsync

        Task SendAsync(MailRequest request);

        #endregion
    }
}
