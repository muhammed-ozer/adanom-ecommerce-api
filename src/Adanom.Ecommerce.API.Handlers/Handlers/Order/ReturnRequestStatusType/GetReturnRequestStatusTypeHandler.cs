namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetReturnRequestStatusTypeHandler : IRequestHandler<GetReturnRequestStatusType, ReturnRequestStatusTypeResponse>

    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetReturnRequestStatusTypeHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ReturnRequestStatusTypeResponse> Handle(GetReturnRequestStatusType command, CancellationToken cancellationToken)
        {
            var returnRequestStatusTypes = await _mediator.Send(new GetReturnRequestStatusTypes());

            return returnRequestStatusTypes.Single(e => e.Key == command.ReturnRequestStatusType);
        }

        #endregion
    }
}
