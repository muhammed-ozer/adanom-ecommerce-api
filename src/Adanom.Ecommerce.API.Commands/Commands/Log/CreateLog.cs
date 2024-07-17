using Adanom.Ecommerce.API.Logging;

namespace Adanom.Ecommerce.API.Commands
{
    public sealed class CreateLog : INotification
    {
        public CreateLog(BaseLogRequest request)
        {
            Request = request;
        }

        public BaseLogRequest Request { get; set; }
    }
}
