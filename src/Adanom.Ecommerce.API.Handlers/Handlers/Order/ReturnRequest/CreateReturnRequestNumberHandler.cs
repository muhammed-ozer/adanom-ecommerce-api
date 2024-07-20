namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateReturnRequestNumberHandler : IRequestHandler<CreateReturnRequestNumber, string>

    {
        #region Fields

        private readonly Random _random;

        #endregion

        #region Ctor

        public CreateReturnRequestNumberHandler()
        {
            _random = new Random();
        }

        #endregion

        #region IRequestHandler Members

        public Task<string> Handle(CreateReturnRequestNumber command, CancellationToken cancellationToken)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");

            var randomNumber = _random.Next(10000, 99999);

            return Task.FromResult($"{ReturnRequestConstants.ReturnRequestNumberPrefix}{timestamp}{randomNumber}");
        }

        #endregion
    }
}
