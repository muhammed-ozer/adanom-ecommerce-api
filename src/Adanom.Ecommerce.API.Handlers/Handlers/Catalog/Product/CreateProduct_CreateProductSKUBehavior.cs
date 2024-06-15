namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_CreateProductSKUBehavior : IPipelineBehavior<CreateProduct, ProductResponse?>
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProduct_CreateProductSKUBehavior(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<ProductResponse?> Handle(CreateProduct command, RequestHandlerDelegate<ProductResponse?> next, CancellationToken cancellationToken)
        {
            var createProductSKUCommand = _mapper.Map(command.CreateProductSKURequest, new CreateProductSKU(command.Identity));

            var productSKUResponse = await _mediator.Send(createProductSKUCommand);

            if (productSKUResponse == null)
            {
                return null;
            }

            command.ProductSKUId = productSKUResponse.Id;

            var productResponse = await next();

            if (productResponse == null)
            {
                return null;
            }

            return productResponse;
        }

        #endregion
    }
}
