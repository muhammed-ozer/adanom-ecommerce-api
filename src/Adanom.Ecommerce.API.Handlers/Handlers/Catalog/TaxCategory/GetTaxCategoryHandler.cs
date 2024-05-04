namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetTaxCategoryHandler : IRequestHandler<GetTaxCategory, TaxCategoryResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetTaxCategoryHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<TaxCategoryResponse?> Handle(GetTaxCategory command, CancellationToken cancellationToken)
        {
            var taxCategories = await _mediator.Send(new GetTaxCategories());

            return taxCategories.Rows.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
