namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductSKU_DeleteRelationsBehavior : IPipelineBehavior<DeleteProductSKU, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public DeleteProductSKU_DeleteRelationsBehavior(
            ApplicationDbContext applicationDbContext,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteProductSKU command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteProductSKUResponse = await next();

            if (deleteProductSKUResponse)
            {
                // TODO: Remove product attribute

                // TODO: Remove product price

                // TODO: Remove anonymous shopping cart items

                // TODO: shopping cart items
            }

            return deleteProductSKUResponse;
        }

        #endregion
    }
}
