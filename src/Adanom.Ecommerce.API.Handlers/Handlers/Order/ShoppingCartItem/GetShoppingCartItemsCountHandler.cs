using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCartItemsCountHandler : IRequestHandler<GetShoppingCartItemsCount, int>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetShoppingCartItemsCountHandler(
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

        public async Task<int> Handle(GetShoppingCartItemsCount command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            return await _applicationDbContext.ShoppingCartItems
                .Where(e => e.ShoppingCart.UserId == userId)
                .CountAsync();
        }

        #endregion
    }
}
