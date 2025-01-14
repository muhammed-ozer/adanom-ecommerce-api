using AutoMapper.QueryableExtensions;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartHandler : IRequestHandler<GetAnonymousShoppingCart, AnonymousShoppingCartResponse?>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<AnonymousShoppingCartResponse?> Handle(GetAnonymousShoppingCart command, CancellationToken cancellationToken)
        {
            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateProjection<AnonymousShoppingCart, AnonymousShoppingCartResponse>()
                        .ForMember(member => member.Total, options =>
                            options.MapFrom(e => e.Items
                            .Sum(e => e.Amount * e.DiscountedPrice ?? e.Amount * e.OriginalPrice)))
                        .ForMember(e => e.Items, options => options.Ignore());
            });

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.AnonymousShoppingCarts
                .AsNoTracking()
                .Where(e => e.Id == command.Id)
                .ProjectTo<AnonymousShoppingCartResponse>(mappingConfiguration)
                .SingleOrDefaultAsync();
        }

        #endregion
    }
}
