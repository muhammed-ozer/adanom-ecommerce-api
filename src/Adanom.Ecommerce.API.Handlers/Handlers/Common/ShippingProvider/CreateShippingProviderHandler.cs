using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateShippingProviderHandler : IRequestHandler<CreateShippingProvider, ShippingProviderResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateShippingProviderHandler(
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

        public async Task<ShippingProviderResponse?> Handle(CreateShippingProvider command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (!command.IsDefault)
            {
                var hasAnyOtherShippingProvider = await applicationDbContext.ShippingProviders
                    .AsNoTracking()
                    .AnyAsync(e => e.DeletedAtUtc == null);

                if (!hasAnyOtherShippingProvider)
                {
                    command.IsDefault = true;
                }
            }
            else
            {
                var currentDefaultShippingProvider = await applicationDbContext.ShippingProviders
                    .Where(e => e.DeletedAtUtc == null &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (currentDefaultShippingProvider != null)
                {
                    currentDefaultShippingProvider.IsDefault = false;
                }
            }

            var shippingProvider = _mapper.Map<CreateShippingProvider, ShippingProvider>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await applicationDbContext.AddAsync(shippingProvider);
            await applicationDbContext.SaveChangesAsync();
           
            await _mediator.Publish(new ClearEntityCache<ShippingProviderResponse>());

            var shippingProviderResponse = _mapper.Map<ShippingProviderResponse>(shippingProvider);

            return shippingProviderResponse;
        }

        #endregion
    }
}
