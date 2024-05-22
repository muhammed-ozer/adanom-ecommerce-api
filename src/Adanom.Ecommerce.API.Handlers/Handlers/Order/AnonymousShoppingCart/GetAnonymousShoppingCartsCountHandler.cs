using AutoMapper.QueryableExtensions;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartsCountHandler : IRequestHandler<GetAnonymousShoppingCartsCount, int>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartsCountHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<int> Handle(GetAnonymousShoppingCartsCount command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.AnonymousShoppingCarts
                .AsNoTracking()
                .CountAsync();
        }

        #endregion
    }
}
