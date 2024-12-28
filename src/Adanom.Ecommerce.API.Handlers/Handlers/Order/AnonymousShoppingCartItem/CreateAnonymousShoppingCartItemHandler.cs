namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateAnonymousShoppingCartItemHandler : IRequestHandler<CreateAnonymousShoppingCartItem, CreateAnonymousShoppingCartItemResponse>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateAnonymousShoppingCartItemHandler(
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

        public async Task<CreateAnonymousShoppingCartItemResponse> Handle(CreateAnonymousShoppingCartItem command, CancellationToken cancellationToken)
        {
            var anonymousShoppingCartResponse = new AnonymousShoppingCartResponse();
            var createAnonymousShoppingCartItemResponse = new CreateAnonymousShoppingCartItemResponse();

            if (command.AnonymousShoppingCartId != null)
            {
                anonymousShoppingCartResponse = await _mediator.Send(new GetAnonymousShoppingCart(command.AnonymousShoppingCartId.Value));

                if (anonymousShoppingCartResponse == null)
                {
                    return createAnonymousShoppingCartItemResponse;
                }
            }
            else
            {
                anonymousShoppingCartResponse = await _mediator.Send(new CreateAnonymousShoppingCart());

                if (anonymousShoppingCartResponse == null)
                {
                    return createAnonymousShoppingCartItemResponse;
                }
            }

            var anonymousShoppingCartItemResponse = await _mediator.Send(new GetAnonymousShoppingCartItem(anonymousShoppingCartResponse.Id, command.ProductId));

            if (anonymousShoppingCartItemResponse == null)
            {
                var anonymousShoppingCartItem = _mapper.Map<CreateAnonymousShoppingCartItem, AnonymousShoppingCartItem>(command, options =>
                {
                    options.AfterMap((source, target) =>
                    {
                        target.AnonymousShoppingCartId = anonymousShoppingCartResponse.Id;
                    });
                });

                await _applicationDbContext.AddAsync(anonymousShoppingCartItem);
            }
            else
            {
                var anonymousShoppingCartItem = _mapper.Map<AnonymousShoppingCartItem>(anonymousShoppingCartItemResponse);

                anonymousShoppingCartItem.Amount += command.Amount;

                _applicationDbContext.Update(anonymousShoppingCartItem);
            }

            var anonymousShoppingCart = _mapper.Map<AnonymousShoppingCart>(anonymousShoppingCartResponse);
            anonymousShoppingCart.LastModifiedAtUtc = DateTime.UtcNow;

            _applicationDbContext.Update(anonymousShoppingCart);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new CustomerTransactionLogRequest()
                {
                    UserId = Guid.Empty,
                    EntityType = EntityType.SHOPPINGCARTITEM,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.CustomerTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return createAnonymousShoppingCartItemResponse;
            }

            createAnonymousShoppingCartItemResponse.IsSuccess = true;
            createAnonymousShoppingCartItemResponse.AnonymousShoppingCart = anonymousShoppingCartResponse;

            return createAnonymousShoppingCartItemResponse;
        }

        #endregion
    }
}
