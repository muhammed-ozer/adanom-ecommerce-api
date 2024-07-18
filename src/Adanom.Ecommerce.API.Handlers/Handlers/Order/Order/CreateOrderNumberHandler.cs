namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrderNumberHandler : IRequestHandler<CreateOrderNumber, string>

    {
        #region Fields

        private readonly Random _random;

        #endregion

        #region Ctor

        public CreateOrderNumberHandler()
        {
            _random = new Random();
        }

        #endregion

        #region IRequestHandler Members

        public Task<string> Handle(CreateOrderNumber command, CancellationToken cancellationToken)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");

            var randomNumber = _random.Next(10000, 99999);

            return Task.FromResult($"{OrderConstants.OrderNumberPrefix}{timestamp}{randomNumber}");
        }

        #endregion
    }
}
