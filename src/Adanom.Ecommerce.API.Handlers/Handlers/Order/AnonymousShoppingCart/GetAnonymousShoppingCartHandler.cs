using AutoMapper.QueryableExtensions;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartHandler : IRequestHandler<GetAnonymousShoppingCart, AnonymousShoppingCartResponse?>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
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
                            .Sum(e => e.Amount * e.Product.ProductSKU.ProductPrice.DiscountedPrice ?? e.Amount * e.Product.ProductSKU.ProductPrice.OriginalPrice)))
                        .ForMember(e => e.Items, options => options.Ignore());
            });

            return await _applicationDbContext.AnonymousShoppingCarts
                .AsNoTracking()
                .Where(e => e.Id == command.Id)
                .ProjectTo<AnonymousShoppingCartResponse>(mappingConfiguration)
                .SingleOrDefaultAsync();
        }

        #endregion
    }
}
