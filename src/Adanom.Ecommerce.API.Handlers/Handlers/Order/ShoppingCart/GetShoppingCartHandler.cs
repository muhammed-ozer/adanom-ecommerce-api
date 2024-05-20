using AutoMapper.QueryableExtensions;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCartHandler : IRequestHandler<GetShoppingCart, ShoppingCartResponse?>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetShoppingCartHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ShoppingCartResponse?> Handle(GetShoppingCart command, CancellationToken cancellationToken)
        {
            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateProjection<ShoppingCart, ShoppingCartResponse>()
                        .ForMember(member => member.Total, options =>
                            options.MapFrom(e => e.Items
                            .Sum(e => e.Amount * e.ProductSKU.ProductPrice.DiscountedPrice ?? e.Amount * e.ProductSKU.ProductPrice.OriginalPrice)))
                        .ForMember(e => e.Items, options => options.Ignore());
            });

            var shoppingCartsQuery = _applicationDbContext.ShoppingCarts
                    .AsNoTracking();

            if (command.UserId != null && command.UserId != Guid.Empty)
            {
                return await shoppingCartsQuery
                    .Where(e => e.UserId == command.UserId)
                    .ProjectTo<ShoppingCartResponse>(mappingConfiguration)
                    .SingleOrDefaultAsync();
            }
            else 
            {
                return await shoppingCartsQuery
                    .Where(e => e.Id == command.Id)
                    .ProjectTo<ShoppingCartResponse>(mappingConfiguration)
                    .SingleOrDefaultAsync();
            }
        }

        #endregion
    }
}
