﻿namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateAnonymousShoppingCartHandler : IRequestHandler<CreateAnonymousShoppingCart, AnonymousShoppingCartResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateAnonymousShoppingCartHandler(
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

        public async Task<AnonymousShoppingCartResponse?> Handle(CreateAnonymousShoppingCart command, CancellationToken cancellationToken)
        {
            var anonymousShoppingCart = new AnonymousShoppingCart()
            {
                LastModifiedAtUtc = DateTime.UtcNow
            };

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(anonymousShoppingCart);
            await applicationDbContext.SaveChangesAsync();

            var anonymousShoppingCartResponse = _mapper.Map<AnonymousShoppingCartResponse>(anonymousShoppingCart);

            return anonymousShoppingCartResponse;
        }

        #endregion
    }
}
