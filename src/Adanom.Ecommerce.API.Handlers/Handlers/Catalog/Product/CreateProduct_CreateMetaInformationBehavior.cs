namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_CreateMetaInformationBehavior : IPipelineBehavior<CreateProduct, ProductResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateProduct_CreateMetaInformationBehavior(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<ProductResponse?> Handle(CreateProduct command, RequestHandlerDelegate<ProductResponse?> next, CancellationToken cancellationToken)
        {
            var productResponse = await next();

            if (productResponse == null)
            {
                return null;
            }

            var createMetaInformationRequest = new CreateMetaInformationRequest()
            {
                EntityId = productResponse.Id,
                EntityType = EntityType.PRODUCT,
                Title = command.Name,
                Description = command.Name,
                Keywords = command.Name
            };

            var createMetaInformationCommand = _mapper.Map(createMetaInformationRequest, new CreateMetaInformation(command.Identity));

            var metaInformationResponse = await _mediator.Send(createMetaInformationCommand);

            if (metaInformationResponse == null)
            {
                return null;
            }

            return productResponse;
        }

        #endregion
    }
}
