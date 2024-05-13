using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductBrandHandler : IRequestHandler<UpdateProductBrand, ProductResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public UpdateProductBrandHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductResponse?> Handle(UpdateProductBrand command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var product = await _applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            product = _mapper.Map(command, product, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedByUserId = userId;
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            _applicationDbContext.Update(product);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                // TODO: Log changes
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"Product_Update_Failed: {exception.Message}");

                product = null;
            }

            var productResponse = _mapper.Map<ProductResponse>(product);

            return productResponse;
        }

        #endregion
    }
}
