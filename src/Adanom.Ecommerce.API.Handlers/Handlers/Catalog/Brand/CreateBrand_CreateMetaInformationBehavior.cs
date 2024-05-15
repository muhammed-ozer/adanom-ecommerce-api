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
                EntityId = brandResponse.Id,
                EntityType = EntityType.BRAND,
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

            return brandResponse;
        }

        #endregion
    }
}
