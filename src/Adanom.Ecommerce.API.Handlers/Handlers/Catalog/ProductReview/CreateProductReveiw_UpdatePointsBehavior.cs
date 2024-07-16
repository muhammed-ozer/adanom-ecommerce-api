namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductReveiw_UpdatePointsBehavior : IPipelineBehavior<CreateProductReview, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProductReveiw_UpdatePointsBehavior(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(CreateProductReview command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var createProductReviewResponse = await next();

            if (!createProductReviewResponse)
            {
                return false;
            }

            var product = await _applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.ProductId)
                .SingleAsync();

            var averagePoints = await _applicationDbContext.ProductReviews
                .Where(e => e.ProductId == product.Id)
                .AverageAsync(e => e.Points);

            product.OverallReviewPoints = averagePoints;

            await _applicationDbContext.SaveChangesAsync();

            return createProductReviewResponse;
        }

        #endregion
    }
}
