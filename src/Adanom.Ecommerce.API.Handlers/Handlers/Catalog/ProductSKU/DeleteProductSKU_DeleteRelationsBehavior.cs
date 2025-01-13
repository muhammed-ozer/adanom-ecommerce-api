namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductSKU_DeleteRelationsBehavior : IPipelineBehavior<DeleteProductSKU, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public DeleteProductSKU_DeleteRelationsBehavior(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteProductSKU command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteProductSKUResponse = await next();

            if (deleteProductSKUResponse)
            {
                #region Delete ProductPrice

                await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

                var productPrice = await applicationDbContext.ProductSKUs
                    .Where(e => e.Id == command.Id)
                    .Select(e => e.ProductPrice)
                    .Where(e => e.DeletedAtUtc == null)
                    .SingleOrDefaultAsync();

                if (productPrice != null)
                {
                    var deleteProductPriceRequest = new DeleteProductPriceRequest()
                    {
                        Id = productPrice.Id
                    };

                    var deleteProductPriceCommand = _mapper.Map(deleteProductPriceRequest, new DeleteProductPrice(command.Identity));

                    var deleteProductPriceResponse = await _mediator.Send(deleteProductPriceCommand);

                    if (!deleteProductPriceResponse)
                    {
                        return false;
                    }
                }

                #endregion
            }

            return deleteProductSKUResponse;
        }

        #endregion
    }
}
