namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateAnonymousShoppingCartHandler : IRequestHandler<CreateAnonymousShoppingCart, AnonymousShoppingCartResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateAnonymousShoppingCartHandler(
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

        public async Task<AnonymousShoppingCartResponse?> Handle(CreateAnonymousShoppingCart command, CancellationToken cancellationToken)
        {
            var anonymousShoppingCart = new AnonymousShoppingCart()
            {
                LastModifiedAtUtc = DateTime.UtcNow
            };

            await _applicationDbContext.AddAsync(anonymousShoppingCart);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new CustomerTransactionLogRequest()
                {
                    UserId = Guid.Empty,
                    EntityType = EntityType.ANONYMOUSSHOPPINGCART,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.CustomerTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            var anonymousShoppingCartResponse = _mapper.Map<AnonymousShoppingCartResponse>(anonymousShoppingCart);

            return anonymousShoppingCartResponse;
        }

        #endregion
    }
}
