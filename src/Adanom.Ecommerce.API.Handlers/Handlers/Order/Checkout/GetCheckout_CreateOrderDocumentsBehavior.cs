using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetCheckout_CreateOrderDocumentsBehavior : IPipelineBehavior<GetCheckout, CheckoutResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetCheckout_CreateOrderDocumentsBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<CheckoutResponse?> Handle(GetCheckout command, RequestHandlerDelegate<CheckoutResponse?> next, CancellationToken cancellationToken)
        {
            var checkoutResponse = await next();

            if (checkoutResponse == null)
            {
                return null;
            }

            var userId = command.Identity.GetUserId();
            var user = await _mediator.Send(new GetUser(userId));

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var orderDocumentsResponse = await _mediator.Send(
                new Checkout_CreateOrderDocuments(
                    user,
                    checkoutResponse,
                    command.OrderPaymentType,
                    command.ShippingAddressId,
                    command.BillingAddressId));

            checkoutResponse.DistanceSellingContractHtmlContent = orderDocumentsResponse.DistanceSellingContractHtmlContent;
            checkoutResponse.PreliminaryInformationFormHtmlContent = orderDocumentsResponse.PreliminaryInformationFormHtmlContent;

            return checkoutResponse;
        }

        #endregion
    }
}
