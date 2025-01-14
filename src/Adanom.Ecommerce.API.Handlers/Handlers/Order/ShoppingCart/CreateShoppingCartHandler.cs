using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCart, ShoppingCartResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateShoppingCartHandler(
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

        public async Task<ShoppingCartResponse?> Handle(CreateShoppingCart command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var shoppingCart = new ShoppingCart() 
            { 
                UserId = userId
            };

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(shoppingCart);
            await applicationDbContext.SaveChangesAsync();

            var shoppingCartResponse = _mapper.Map<ShoppingCartResponse>(shoppingCart);

            return shoppingCartResponse;
        }

        #endregion
    }
}
