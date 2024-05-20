using AutoMapper.QueryableExtensions;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartsHandler : IRequestHandler<GetAnonymousShoppingCarts, PaginatedData<AnonymousShoppingCartResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<AnonymousShoppingCartResponse>> Handle(GetAnonymousShoppingCarts command, CancellationToken cancellationToken)
        {
            var anonymousShoppingCartsQuery = _applicationDbContext.AnonymousShoppingCarts.AsNoTracking();

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
                            options.MapFrom(e => e.Items
                            .Sum(e => e.Amount * e.ProductSKU.ProductPrice.DiscountedPrice ?? e.Amount * e.ProductSKU.ProductPrice.OriginalPrice)))
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
