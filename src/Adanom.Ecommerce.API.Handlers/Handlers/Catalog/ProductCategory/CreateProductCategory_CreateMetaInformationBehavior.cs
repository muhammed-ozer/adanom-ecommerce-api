namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductCategory_CreateMetaInformationBehavior : IPipelineBehavior<CreateProductCategory, ProductCategoryResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateProductCategory_CreateMetaInformationBehavior(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<ProductCategoryResponse?> Handle(CreateProductCategory command, RequestHandlerDelegate<ProductCategoryResponse?> next, CancellationToken cancellationToken)
        {
            var productCategoryResponse = await next();

            if (productCategoryResponse == null)
            {
                return null;
            }

            var createMetaInformationRequest = new CreateMetaInformationRequest()
            {
                EntityId = productCategoryResponse.Id,
                EntityType = EntityType.PRODUCTCATEGORY,
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

            return productCategoryResponse;
        }

        #endregion
    }
}
