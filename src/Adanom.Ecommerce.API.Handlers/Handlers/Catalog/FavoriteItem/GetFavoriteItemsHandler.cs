namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetFavoriteItemsHandler : IRequestHandler<GetFavoriteItems, PaginatedData<FavoriteItemResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetFavoriteItemsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<FavoriteItemResponse>> Handle(GetFavoriteItems command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var favoriteItemsQuery = applicationDbContext.FavoriteItems.AsNoTracking();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (command.Filter.UserId != null)
                {
                    favoriteItemsQuery = favoriteItemsQuery.Where(e => e.UserId == command.Filter.UserId);
                }

                #endregion

                #region Apply ordering

                favoriteItemsQuery = command.Filter.OrderBy switch
                {
                    GetFavoriteItemsOrderByEnum.CREATED_AT_ASC =>
                        favoriteItemsQuery.OrderBy(e => e.CreatedAtUtc),
                    _ =>
                        favoriteItemsQuery.OrderByDescending(e => e.CreatedAtUtc)
                };

                #endregion
            }
            else
            {
                favoriteItemsQuery.OrderByDescending(e => e.CreatedAtUtc);
            }

            var totalCount = favoriteItemsQuery.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                favoriteItemsQuery = favoriteItemsQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var favoriteItemResponses = _mapper.Map<IEnumerable<FavoriteItemResponse>>(await favoriteItemsQuery.ToListAsync());

            return new PaginatedData<FavoriteItemResponse>(
                favoriteItemResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
