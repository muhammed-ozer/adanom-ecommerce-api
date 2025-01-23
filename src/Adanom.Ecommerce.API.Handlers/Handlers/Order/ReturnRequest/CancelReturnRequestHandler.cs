using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CancelReturnRequestHandler : IRequestHandler<CancelReturnRequest, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public CancelReturnRequestHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CancelReturnRequest command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var userId = command.Identity.GetUserId();

            var returnRequest = await applicationDbContext.ReturnRequests
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            returnRequest.ReturnRequestStatusType = ReturnRequestStatusType.CANCEL;

            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
