namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateSliderItem_CreateImageBehavior : IPipelineBehavior<CreateSliderItem, SliderItemResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateSliderItem_CreateImageBehavior(
            IMediator mediator, 
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<SliderItemResponse?> Handle(CreateSliderItem command, RequestHandlerDelegate<SliderItemResponse?> next, CancellationToken cancellationToken)
        {
            var sliderItemResponse = await next();

            if (sliderItemResponse == null)
            {
                return null;
            }

            var createImageRequest = new CreateImageRequest()
            {
                EntityId = sliderItemResponse.Id,
                EntityType = EntityType.SLIDERITEM,
                File = command.File,
                EntityNameAsUrlSlug = sliderItemResponse.Name.ConvertToUrlSlug()
            };

            var createImageCommand = _mapper.Map(createImageRequest, new CreateImage(command.Identity));

            var imageResponse = await _mediator.Send(createImageCommand);

            if (imageResponse == null)
            {
                return null;
            }

            return sliderItemResponse;
        }

        #endregion
    }
}
