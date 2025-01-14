using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductTagHandler : IRequestHandler<UpdateProductTag, ProductTagResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductTagHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductTagResponse?> Handle(UpdateProductTag command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productTag = await applicationDbContext.ProductTags
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            command.Value = command.Value.ToLower(CultureInfoConstants.TurkishCulture);

            productTag = _mapper.Map(command, productTag);

            productTag.UpdatedAtUtc = DateTime.UtcNow;
            productTag.UpdatedByUserId = userId;

            applicationDbContext.Update(productTag);

            await applicationDbContext.SaveChangesAsync();

            var productTagResponse = _mapper.Map<ProductTagResponse>(productTag);

            await _mediator.Publish(new UpdateFromCache<ProductTagResponse>(productTagResponse));

            return productTagResponse;
        }

        #endregion
    }
}
