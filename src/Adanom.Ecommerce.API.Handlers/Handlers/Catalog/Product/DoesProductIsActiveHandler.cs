namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductIsActiveHandler : IRequestHandler<DoesProductIsActive, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductIsActiveHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProductIsActive command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .AnyAsync(e => e.IsActive);
        }

        #endregion
    }
}
