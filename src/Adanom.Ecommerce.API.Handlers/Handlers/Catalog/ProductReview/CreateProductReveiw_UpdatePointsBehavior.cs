namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductReveiw_UpdatePointsBehavior : IPipelineBehavior<CreateProductReview, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProductReveiw_UpdatePointsBehavior(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
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

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var product = await applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.ProductId)
                .SingleAsync();

            var averagePoints = await applicationDbContext.ProductReviews
                .Where(e => e.ProductId == product.Id)
                .AverageAsync(e => e.Points);

            product.OverallReviewPoints = averagePoints;

            await applicationDbContext.SaveChangesAsync();

            return createProductReviewResponse;
        }

        #endregion
    }
}
