using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductCategoryHandler : IRequestHandler<UpdateProductCategory, ProductCategoryResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductCategoryHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductCategoryResponse?> Handle(UpdateProductCategory command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productCategory = await _applicationDbContext.ProductCategories
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productCategory = _mapper.Map(command, productCategory);

            productCategory.UrlSlug = command.Name.ConvertToUrlSlug();
            productCategory.UpdatedAtUtc = DateTime.UtcNow;
            productCategory.UpdatedByUserId = userId;

            _applicationDbContext.Update(productCategory);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"ProductCategory_Update_Failed: {exception.Message}");

                return null;
            }

            var productCategoryResponse = _mapper.Map<ProductCategoryResponse>(productCategory);

            await _mediator.Publish(new UpdateFromCache<ProductCategoryResponse>(productCategoryResponse));

            return productCategoryResponse;
        }

        #endregion
    }
}
