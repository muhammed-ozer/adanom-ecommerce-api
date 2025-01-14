using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductTagHandler : IRequestHandler<CreateProductTag, ProductTagResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProductTagHandler(
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

        public async Task<ProductTagResponse?> Handle(CreateProductTag command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            command.Value = command.Value.ToLower(CultureInfoConstants.TurkishCulture);

            var productTag = _mapper
                .Map<CreateProductTag, ProductTag>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(productTag);
            await applicationDbContext.SaveChangesAsync();
            

            var productTagResponse = _mapper.Map<ProductTagResponse>(productTag);

            await _mediator.Publish(new AddToCache<ProductTagResponse>(productTagResponse));

            return productTagResponse;
        }

        #endregion
    }
}
