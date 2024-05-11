using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductPriceTaxCategoryHandler : IRequestHandler<UpdateProductPriceTaxCategory, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public UpdateProductPriceTaxCategoryHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateProductPriceTaxCategory command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productPrice = await _applicationDbContext.ProductPrices
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productPrice = _mapper.Map(command, productPrice, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedByUserId = userId;
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            _applicationDbContext.Update(productPrice);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"ProductPrice_Update_Failed: {exception.Message}");

                return false;
            }

            return true;
        }

        #endregion
    }
}
