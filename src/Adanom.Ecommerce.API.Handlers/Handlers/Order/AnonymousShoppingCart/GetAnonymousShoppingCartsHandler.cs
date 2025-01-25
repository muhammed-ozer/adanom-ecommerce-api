using AutoMapper.QueryableExtensions;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartsHandler : IRequestHandler<GetAnonymousShoppingCarts, PaginatedData<AnonymousShoppingCartResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<AnonymousShoppingCartResponse>> Handle(GetAnonymousShoppingCarts command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var anonymousShoppingCartsQuery = applicationDbContext.AnonymousShoppingCarts.AsNoTracking();

            if (command.Filter is not null)
            {
                #region Apply filtering

                #endregion

                #region Apply ordering

                anonymousShoppingCartsQuery = command.Filter.OrderBy switch
                {
                    GetAnonymousShoppingCartsOrderByEnum.LAST_MODIFIED_AT_ASC =>
                        anonymousShoppingCartsQuery.OrderBy(e => e.LastModifiedAtUtc),
                    _ =>
                        anonymousShoppingCartsQuery.OrderByDescending(e => e.LastModifiedAtUtc)
                };

                #endregion
            }

            var totalCount = anonymousShoppingCartsQuery.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                anonymousShoppingCartsQuery = anonymousShoppingCartsQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateProjection<AnonymousShoppingCart, AnonymousShoppingCartResponse>()
                   .ForMember(member => member.Total, options =>
                       options.MapFrom(e => e.Items.Sum(item =>
                           (item.DiscountedPrice ?? item.OriginalPrice) * item.Amount)))
                   .ForMember(e => e.Items, options => options.Ignore());
            });

            var anonymousShoppingCartResponses = await anonymousShoppingCartsQuery.ProjectTo<AnonymousShoppingCartResponse>(mappingConfiguration).ToListAsync();

            return new PaginatedData<AnonymousShoppingCartResponse>(
                anonymousShoppingCartResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
