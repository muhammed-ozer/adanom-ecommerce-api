namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteShippingProvider_DeleteRelationsBehavior : IPipelineBehavior<DeleteShippingProvider, bool>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public DeleteShippingProvider_DeleteRelationsBehavior(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteShippingProvider command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteShippingProviderResponse = await next();

            if (deleteShippingProviderResponse)
            {
                #region Images

                var deleteImagesRequest = new DeleteImagesRequest()
                {
                    EntityId = command.Id,
                    EntityType = EntityType.SHIPPINGPROVIDER
                };

                var deleteImagesCommand = _mapper
                    .Map(deleteImagesRequest, new DeleteImages(command.Identity));

                await _mediator.Send(deleteImagesCommand);

                #endregion
            }

            return deleteShippingProviderResponse;
        }

        #endregion
    }
}
