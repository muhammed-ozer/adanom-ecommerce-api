namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductSpecificationAttributeGroupHandler : IRequestHandler<GetProductSpecificationAttributeGroup, ProductSpecificationAttributeGroupResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetProductSpecificationAttributeGroupHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductSpecificationAttributeGroupResponse?> Handle(GetProductSpecificationAttributeGroup command, CancellationToken cancellationToken)
        {
            var productSpecificationAttributeGroups = await _mediator.Send(new GetProductSpecificationAttributeGroups());

            return productSpecificationAttributeGroups.Rows.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
