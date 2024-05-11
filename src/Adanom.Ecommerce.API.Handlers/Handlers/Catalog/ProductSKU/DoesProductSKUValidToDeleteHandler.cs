namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductSKUValidToDeleteHandler : IRequestHandler<DoesProductSKUValidToDelete, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DoesProductSKUValidToDeleteHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProductSKUValidToDelete command, CancellationToken cancellationToken)
        {
            var productSKUResponse = await _mediator.Send(new GetProductSKU(command.Id));

            if (productSKUResponse == null)
            {
                return true;
            }

            var productHasAnyOtherProductSKU = await _applicationDbContext.ProductSKUs
                .Where(e =>
                    e.DeletedAtUtc == null &&
                    e.ProductId == productSKUResponse.ProductId &&
                    e.Id != command.Id)
                .AnyAsync();

            return productHasAnyOtherProductSKU;
        }

        #endregion
    }
}
