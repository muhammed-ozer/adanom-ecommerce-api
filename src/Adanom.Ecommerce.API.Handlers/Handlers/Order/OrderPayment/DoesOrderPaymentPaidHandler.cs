namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesOrderPaymentPaidHandler : IRequestHandler<DoesOrderPaymentPaid, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesOrderPaymentPaidHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesOrderPaymentPaid command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.OrderPayments
                .AnyAsync(e => e.OrderId == command.OrderId && e.Paid);
        }

        #endregion
    }
}
