namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateReturnRequest_CreateReturnRequestItemsBehavior : IPipelineBehavior<CreateReturnRequest, ReturnRequestResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ICalculationService _calculationService;

        #endregion

        #region Ctor

        public CreateReturnRequest_CreateReturnRequestItemsBehavior(
            IMediator mediator,
            IMapper mapper,
            ICalculationService calculationService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
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

            var returnRequestItems = new List<ReturnRequestItem>();

            foreach (var createReturnRequestItemRequest in command.CreateReturnRequest_ItemRequests)
            {
                var orderItem = await _mediator.Send(new GetOrderItem(createReturnRequestItemRequest.OrderItemId));

                if (orderItem == null)
                {
                    return null;
                }

                var returnRequestItem = new ReturnRequestItem()
                {
                    OrderItemId = orderItem.Id,
                    TaxExcludedPrice = orderItem.TaxExcludedPrice,
                    Amount = createReturnRequestItemRequest.Amount,
                    AmountUnit = orderItem.AmountUnit,
                    TaxRate = orderItem.TaxRate,
                    Total = _calculationService.CalculateTaxIncludedPrice(orderItem.TaxExcludedPrice, orderItem.TaxRate) * createReturnRequestItemRequest.Amount,
                    Description = createReturnRequestItemRequest.Description
                };

                returnRequestItems.Add(returnRequestItem);
            }

            var returnRequestItemResponses = _mapper.Map<IEnumerable<ReturnRequestItemResponse>>(returnRequestItems);
            returnRequestResponse.Items = returnRequestItemResponses.ToList();

            return returnRequestResponse;
        }

        #endregion
    }
}
