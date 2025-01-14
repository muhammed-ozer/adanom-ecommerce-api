using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductPriceHandler : IRequestHandler<CreateProductPrice, ProductPriceResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProductPriceHandler(
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

        public async Task<ProductPriceResponse?> Handle(CreateProductPrice command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productPrice = _mapper.Map<CreateProductPrice, ProductPrice>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(productPrice);
            await applicationDbContext.SaveChangesAsync();

            var productPriceResponse = _mapper.Map<ProductPriceResponse>(productPrice);

            return productPriceResponse;
        }

        #endregion
    }
}
