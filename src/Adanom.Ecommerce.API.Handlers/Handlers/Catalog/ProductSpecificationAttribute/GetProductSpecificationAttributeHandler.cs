namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductSpecificationAttributeHandler : IRequestHandler<GetProductSpecificationAttribute, ProductSpecificationAttributeResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetProductSpecificationAttributeHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductSpecificationAttributeResponse?> Handle(GetProductSpecificationAttribute command, CancellationToken cancellationToken)
        {
            var productSpecificationAttributes = await _mediator.Send(new GetProductSpecificationAttributes());

            return productSpecificationAttributes.Rows.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
