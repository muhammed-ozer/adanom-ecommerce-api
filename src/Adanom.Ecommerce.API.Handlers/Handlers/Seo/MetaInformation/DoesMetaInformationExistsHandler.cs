namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesMetaInformationExistsHandler : IRequestHandler<DoesEntityExists<MetaInformationResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesMetaInformationExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<MetaInformationResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.MetaInformations
                .AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
