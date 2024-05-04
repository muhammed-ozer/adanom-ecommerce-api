using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateBrandHandler : IRequestHandler<UpdateBrand, BrandResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateBrandHandler(
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

        public async Task<BrandResponse?> Handle(UpdateBrand command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var brand = await _applicationDbContext.Brands
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            brand = _mapper.Map(command, brand);

            brand.UrlSlug = command.Name.ConvertToUrlSlug();
            brand.UpdatedAtUtc = DateTime.UtcNow;
            brand.UpdatedByUserId = userId;

            _applicationDbContext.Update(brand);
            await _applicationDbContext.SaveChangesAsync();

            var brandResponse = _mapper.Map<BrandResponse>(brand);

            await _mediator.Publish(new UpdateFromCache<BrandResponse>(brandResponse));

            return brandResponse;
        }

        #endregion
    }
}
