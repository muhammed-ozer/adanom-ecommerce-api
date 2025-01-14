using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateShippingProviderHandler : IRequestHandler<UpdateShippingProvider, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateShippingProviderHandler(
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

        public async Task<bool> Handle(UpdateShippingProvider command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var shippingProvider = await applicationDbContext.ShippingProviders
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            if (command.IsDefault && !shippingProvider.IsDefault)
            {
                var cuurentDefaultShippingprovider = await applicationDbContext.ShippingProviders
                    .Where(e => e.DeletedAtUtc == null &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (cuurentDefaultShippingprovider != null)
                {
                    cuurentDefaultShippingprovider.IsDefault = false;
                }
            }
            else
            {
                command.IsDefault = true;
            }

            shippingProvider = _mapper.Map(command, shippingProvider, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedByUserId = userId;
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            applicationDbContext.Update(shippingProvider);
            await applicationDbContext.SaveChangesAsync();

            await _mediator.Publish(new ClearEntityCache<ShippingProviderResponse>());

            return true;
        }

        #endregion
    }
}
