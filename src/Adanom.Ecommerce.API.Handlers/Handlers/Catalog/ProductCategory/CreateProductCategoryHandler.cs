using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductCategoryHandler : IRequestHandler<CreateProductCategory, ProductCategoryResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProductCategoryHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductCategoryResponse?> Handle(CreateProductCategory command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productCategory = _mapper.Map<CreateProductCategory, ProductCategory>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UrlSlug = command.Name.ConvertToUrlSlug();
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(productCategory);
            await applicationDbContext.SaveChangesAsync();

            var productCategoryResponse = _mapper.Map<ProductCategoryResponse>(productCategory);

            await _mediator.Publish(new AddToCache<ProductCategoryResponse>(productCategoryResponse));

            return productCategoryResponse;
        }

        #endregion
    }
}
