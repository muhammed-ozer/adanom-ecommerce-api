namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeletePickUpStore_DeleteRelationsBehavior : IPipelineBehavior<DeletePickUpStore, bool>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public DeletePickUpStore_DeleteRelationsBehavior(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeletePickUpStore command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deletePickUpStoreResponse = await next();

            if (deletePickUpStoreResponse)
            {
                #region Images

                var deleteImagesRequest = new DeleteImagesRequest()
                {
                    EntityId = command.Id,
                    EntityType = EntityType.PICKUPSTORE
                };

                var deleteImagesCommand = _mapper
                    .Map(deleteImagesRequest, new DeleteImages(command.Identity));

                await _mediator.Send(deleteImagesCommand);

                #endregion
            }

            return deletePickUpStoreResponse;
        }

        #endregion
    }
}
