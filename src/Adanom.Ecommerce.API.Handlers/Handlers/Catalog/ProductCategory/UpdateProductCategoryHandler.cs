using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductCategoryHandler : IRequestHandler<UpdateProductCategory, ProductCategoryResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductCategoryHandler(
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

        public async Task<ProductCategoryResponse?> Handle(UpdateProductCategory command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productCategory = await applicationDbContext.ProductCategories
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productCategory = _mapper.Map(command, productCategory);

            productCategory.UrlSlug = command.Name.ConvertToUrlSlug();
            productCategory.UpdatedAtUtc = DateTime.UtcNow;
            productCategory.UpdatedByUserId = userId;

            applicationDbContext.Update(productCategory);
            await applicationDbContext.SaveChangesAsync();

            var productCategoryResponse = _mapper.Map<ProductCategoryResponse>(productCategory);

            await _mediator.Publish(new UpdateFromCache<ProductCategoryResponse>(productCategoryResponse));

            return productCategoryResponse;
        }

        #endregion
    }
}
