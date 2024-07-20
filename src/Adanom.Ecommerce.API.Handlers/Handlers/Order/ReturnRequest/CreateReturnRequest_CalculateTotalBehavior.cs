namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateReturnRequest_CalculateTotalBehavior : IPipelineBehavior<CreateReturnRequest, ReturnRequestResponse?>
    {
        #region Fields

        #endregion

        #region Ctor

        public CreateReturnRequest_CalculateTotalBehavior()
        {
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<ReturnRequestResponse?> Handle(CreateReturnRequest command, RequestHandlerDelegate<ReturnRequestResponse?> next, CancellationToken cancellationToken)
        {
            var returnRequestResponse = await next();

            if (returnRequestResponse == null)
            {
                return null;
            }

            returnRequestResponse.GrandTotal = returnRequestResponse.Items.Sum(e => e.Total);

            return returnRequestResponse;
        }

        #endregion
    }
}
