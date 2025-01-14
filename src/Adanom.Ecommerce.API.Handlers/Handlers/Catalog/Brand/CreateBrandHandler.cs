using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateBrandHandler : IRequestHandler<CreateBrand, BrandResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateBrandHandler(
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

        public async Task<BrandResponse?> Handle(CreateBrand command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var brand = _mapper.Map<CreateBrand, Brand>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UrlSlug = command.Name.ConvertToUrlSlug();
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(brand);
            await applicationDbContext.SaveChangesAsync();

            var brandResponse = _mapper.Map<BrandResponse>(brand);

            await _mediator.Publish(new AddToCache<BrandResponse>(brandResponse));

            return brandResponse;
        }

        #endregion
    }
}
