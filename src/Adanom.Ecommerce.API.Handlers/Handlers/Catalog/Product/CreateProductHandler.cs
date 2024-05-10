using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductHandler : IRequestHandler<CreateProduct, ProductResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateProductHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductResponse?> Handle(CreateProduct command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();
            
            var product = _mapper.Map<CreateProduct, Product>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UrlSlug = command.Name.ConvertToUrlSlug();
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await _applicationDbContext.AddAsync(product);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"Product_Create_Failed: {exception.Message}");

                product = null;
            }

            var productResponse = _mapper.Map<ProductResponse>(product);

            return productResponse;
        }

        #endregion
    }
}
