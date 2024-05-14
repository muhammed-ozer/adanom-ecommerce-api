namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesImageExistsHandler : IRequestHandler<DoesEntityExists<ImageResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesImageExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ImageResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Images.AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id) &&
                   await _applicationDbContext.Image_Entity_Mappings.AnyAsync(e => e.ImageId == command.Id);
        }

        #endregion
    }
}
