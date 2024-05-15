namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductReviewHandler : IRequestHandler<GetProductReview, ProductReviewResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductReviewHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductReviewResponse?> Handle(GetProductReview command, CancellationToken cancellationToken)
        {
            var productReview = await _applicationDbContext.ProductReviews
                .AsNoTracking()
                .Where(e => e.Id == command.Id)
                .SingleOrDefaultAsync();
           
            return _mapper.Map<ProductReviewResponse>(productReview);
        } 

        #endregion
    }
}
