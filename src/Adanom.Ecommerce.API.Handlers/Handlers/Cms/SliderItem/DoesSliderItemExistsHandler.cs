namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesSliderItemExistsHandler : IRequestHandler<DoesEntityExists<SliderItemResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesSliderItemExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<SliderItemResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.SliderItems
                .AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
