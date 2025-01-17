namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesOrderPaymentExistsHandler : IRequestHandler<DoesEntityExists<OrderPaymentResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesOrderPaymentExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<OrderPaymentResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.OrderPayments
                .AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
