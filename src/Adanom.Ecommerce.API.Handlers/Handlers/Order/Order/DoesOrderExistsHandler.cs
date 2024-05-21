namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesOrderExistsHandler : IRequestHandler<DoesEntityExists<OrderResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesOrderExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<OrderResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Orders
                .AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
