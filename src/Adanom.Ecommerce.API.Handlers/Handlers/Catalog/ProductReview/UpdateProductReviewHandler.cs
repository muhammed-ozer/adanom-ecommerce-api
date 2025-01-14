using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductReviewHandler : IRequestHandler<UpdateProductReview, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductReviewHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateProductReview command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productReview = await applicationDbContext.ProductReviews
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            productReview = _mapper.Map(command, productReview, options =>
            {
                options.BeforeMap((source, target) =>
                {
                    if (source.IsApproved && !target.IsApproved)
                    {
                        target.ApprovedByUserId = userId;
                        target.ApprovedAtUtc = DateTime.UtcNow;
                    }
                });
            });

            applicationDbContext.Update(productReview);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
