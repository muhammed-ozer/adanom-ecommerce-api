namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateBrand_CreateMetaInformationBehavior : IPipelineBehavior<CreateBrand, BrandResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateBrand_CreateMetaInformationBehavior(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<BrandResponse?> Handle(CreateBrand command, RequestHandlerDelegate<BrandResponse?> next, CancellationToken cancellationToken)
        {
            var brandResponse = await next();

            if (brandResponse == null)
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
                EntityId = brandResponse.Id,
                EntityType = EntityType.BRAND
            };

            var createMetaInformation_EntityCommand = _mapper.Map(
                createMetaInformation_EntityRequest,
                new CreateMetaInformation_Entity(command.Identity));

            var createMetaInformation_EntityResponse = await _mediator.Send(createMetaInformation_EntityCommand);

            if (!createMetaInformation_EntityResponse)
            {
                return null;
            }

            return brandResponse;
        }

        #endregion
    }
}
