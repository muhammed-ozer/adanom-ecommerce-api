using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateShoppingCartItemHandler : IRequestHandler<UpdateShoppingCartItem, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateShoppingCartItemHandler(
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

        public async Task<bool> Handle(UpdateShoppingCartItem command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var shoppingCartItem = await _applicationDbContext.ShoppingCartItems
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            if (command.Amount > 0)
            {
                shoppingCartItem = _mapper.Map(command, shoppingCartItem, options =>
                {
                    options.AfterMap((source, target) =>
                    {
                        target.LastModifiedAtUtc = DateTime.UtcNow;
                    });
                });

                _applicationDbContext.Update(shoppingCartItem);
            }
            else
            {
                var deleteSHoppingCartItemRequest = new DeleteShoppingCartItemRequest()
                {
                    Id = shoppingCartItem.Id
                };

                var deleteShoppingCartItemCommand = _mapper.Map(deleteSHoppingCartItemRequest, new DeleteShoppingCartItem(command.Identity));

                var deleteSHoppingCartItemResponse = await _mediator.Send(deleteShoppingCartItemCommand);

                if (!deleteSHoppingCartItemResponse)
                {
                    return false;
                }
            }

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.SHOPPINGCARTITEM,
                    TransactionType = TransactionType.UPDATE,
                    Description = LogMessages.CustomerTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            return true;
        }

        #endregion
    }
}
