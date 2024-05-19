using Adanom.Ecommerce.API.Services.Azure;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteBrand_DeleteRelationsBehavior : IPipelineBehavior<DeleteBrand, bool>
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteBrand_DeleteRelationsBehavior(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteBrand command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteBrandResponse = await next();

            if (!deleteBrandResponse)
            {
                return deleteBrandResponse;
            }

            #region MetaInformation

            var deleteMetaInformationRequest = new DeleteMetaInformationRequest()
            {
                EntityId = command.Id,
                EntityType = EntityType.BRAND
            };

            var deleteMetaInformationCommand = _mapper
                .Map(deleteMetaInformationRequest, new DeleteMetaInformation(command.Identity));

            await _mediator.Send(deleteMetaInformationCommand);

            #endregion

            #region Images

            var deleteImagesRequest = new DeleteImagesRequest()
            {
                EntityId = command.Id,
                EntityType = EntityType.BRAND
            };

            var deleteImagesCommand = _mapper
                .Map(deleteImagesRequest, new DeleteImages(command.Identity));

            await _mediator.Send(deleteImagesCommand);

            #endregion

            return deleteBrandResponse;
        }

        #endregion
    }
}
