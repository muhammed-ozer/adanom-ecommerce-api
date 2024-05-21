namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesReturnRequestExistsHandler : IRequestHandler<DoesEntityExists<ReturnRequestResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesReturnRequestExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ReturnRequestResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ReturnRequests
                .AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
