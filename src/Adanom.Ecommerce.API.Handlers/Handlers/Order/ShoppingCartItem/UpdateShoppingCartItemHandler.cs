using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateShoppingCartItemHandler : IRequestHandler<UpdateShoppingCartItem, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateShoppingCartItemHandler(
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

        public async Task<bool> Handle(UpdateShoppingCartItem command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var shoppingCartItem = await applicationDbContext.ShoppingCartItems
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            var productPrice = await _mediator.Send(new GetProductPriceByProductId(command.ProductId));

            if (productPrice == null)
            {
                return false;
            }

            if (command.Amount > 0)
            {
                shoppingCartItem = _mapper.Map(command, shoppingCartItem, options =>
                {
                    options.AfterMap((source, target) =>
                    {
                        target.LastModifiedAtUtc = DateTime.UtcNow;
                        target.OriginalPrice = productPrice.OriginalPrice;
                        target.DiscountedPrice = productPrice.DiscountedPrice;
                    });
                });

                applicationDbContext.Update(shoppingCartItem);
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

            await _mediator.Send(new UpdateShoppingCart_LastModifiedDate(shoppingCartItem.ShoppingCartId));

            try
            {
                await applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new CustomerTransactionLogRequest()
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
