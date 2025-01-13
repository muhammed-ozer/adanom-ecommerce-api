using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteSliderItem_DeleteImageBehavior : IPipelineBehavior<DeleteSliderItem, bool>
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public DeleteSliderItem_DeleteImageBehavior(
            IMediator mediator, 
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteSliderItem command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteSliderItemResponse = await next();

            if (!deleteSliderItemResponse)
            {
                return false;
            }

            var deleteImagesRequest = new DeleteImagesRequest()
            {
                EntityId = command.Id,
                EntityType = EntityType.SLIDERITEM,
            };

            var deleteImagesCommand = _mapper.Map(deleteImagesRequest, new DeleteImages(command.Identity));

            var deleteImagesResponse = await _mediator.Send(deleteImagesCommand);

            if (!deleteImagesResponse)
            {
                return false;
            }

            return deleteSliderItemResponse;
        }

        #endregion
    }
}
