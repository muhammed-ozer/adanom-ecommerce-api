namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductCategory_DeleteRelationsBehavior : IPipelineBehavior<DeleteProductCategory, bool>
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProductCategory_DeleteRelationsBehavior(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteProductCategory command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteProductCategoryResponse = await next();

            if (!deleteProductCategoryResponse)
            {
                return deleteProductCategoryResponse;
            }

            #region Images

            var deleteImagesRequest = new DeleteImagesRequest()
            {
                EntityId = command.Id,
                EntityType = EntityType.PRODUCTCATEGORY
            };

            var deleteImagesCommand = _mapper
                .Map(deleteImagesRequest, new DeleteImages(command.Identity));

            await _mediator.Send(deleteImagesCommand);

            #endregion

            return deleteProductCategoryResponse;
        }

        #endregion
    }
}
