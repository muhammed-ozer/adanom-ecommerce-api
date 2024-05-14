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

            var createMetaInformation_EntityRequest = new CreateMetaInformation_EntityRequest()
            {
                MetaInformationId = metaInformationResponse.Id,
                EntityId = productCategoryResponse.Id,
                EntityType = EntityType.PRODUCTCATEGORY
            };

            var createMetaInformation_EntityCommand = _mapper.Map(
                createMetaInformation_EntityRequest,
                new CreateMetaInformation_Entity(command.Identity));

            var createMetaInformation_EntityResponse = await _mediator.Send(createMetaInformation_EntityCommand);

            if (!createMetaInformation_EntityResponse)
            {
                return null;
            }

            return productCategoryResponse;
        }

        #endregion
    }
}
