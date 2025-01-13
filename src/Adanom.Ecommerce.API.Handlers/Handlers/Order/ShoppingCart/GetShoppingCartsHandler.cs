using AutoMapper.QueryableExtensions;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCartsHandler : IRequestHandler<GetShoppingCarts, PaginatedData<ShoppingCartResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetShoppingCartsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<ShoppingCartResponse>> Handle(GetShoppingCarts command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var shoppingCartsQuery = applicationDbContext.ShoppingCarts.AsNoTracking();

            if (command.Filter is not null)
            {
                #region Apply filtering

                #endregion

                #region Apply ordering

                shoppingCartsQuery = command.Filter.OrderBy switch
                {
                    GetShoppingCartsOrderByEnum.LAST_MODIFIED_AT_ASC =>
                        shoppingCartsQuery.OrderBy(e => e.LastModifiedAtUtc),
                    _ =>
                        shoppingCartsQuery.OrderByDescending(e => e.LastModifiedAtUtc)
                };

                #endregion
            }

            var totalCount = shoppingCartsQuery.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                shoppingCartsQuery = shoppingCartsQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateProjection<ShoppingCart, ShoppingCartResponse>()
                        .ForMember(member => member.Total, options =>
                            options.MapFrom(e => e.Items
                            .Sum(e => e.Amount * e.DiscountedPrice ?? e.Amount * e.OriginalPrice)))
                        .ForMember(e => e.Items, options => options.Ignore());
            });

            var shoppingCartResponses = await shoppingCartsQuery.ProjectTo<ShoppingCartResponse>(mappingConfiguration).ToListAsync();

            return new PaginatedData<ShoppingCartResponse>(
                shoppingCartResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
