namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesOrderPaymentExistsByOrderIdHandler : IRequestHandler<DoesOrderPaymentExistsByOrderId, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesOrderPaymentExistsByOrderIdHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesOrderPaymentExistsByOrderId command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.OrderPayments
                .AnyAsync(e => e.OrderId == command.OrderId);
        }

        #endregion
    }
}
