namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductTagHandler : IRequestHandler<GetProductTag, ProductTagResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetProductTagHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductTagResponse?> Handle(GetProductTag command, CancellationToken cancellationToken)
        {
            var productTags = await _mediator.Send(new GetProductTags());

            if (command.Value.IsNotNullOrEmpty())
            {
                return productTags.Rows.SingleOrDefault(e => e.Value == command.Value);
            }

            return productTags.Rows.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
