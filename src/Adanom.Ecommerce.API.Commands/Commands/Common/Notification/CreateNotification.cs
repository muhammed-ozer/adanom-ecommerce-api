using Adanom.Ecommerce.API.Data.Models;
using MediatR;

namespace Adanom.Ecommerce.API.Commands
{
    public sealed class CreateNotification : INotification
    {
        #region Ctor

        public CreateNotification(NotificationType notificationType, string content)
        {
            NotificationType = notificationType;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        #endregion

        #region Properties

        public NotificationType NotificationType { get; }

        public string Content { get; }

        #endregion
    }
}
