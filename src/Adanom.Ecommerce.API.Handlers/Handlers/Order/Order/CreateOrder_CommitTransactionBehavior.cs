using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrder_CommitTransactionBehavior : IPipelineBehavior<CreateOrder, OrderResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateOrder_CommitTransactionBehavior(
            ApplicationDbContext applicationDbContext,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<OrderResponse?> Handle(CreateOrder command, RequestHandlerDelegate<OrderResponse?> next, CancellationToken cancellationToken)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            var orderResponse = await next();

            if (orderResponse == null)
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync(cancellationToken);

                    await _mediator.Publish(new CreateLog(new CustomerTransactionLogRequest()
                    {
                        UserId = command.Identity.GetUserId(),
                        EntityType = EntityType.ORDER,
                        TransactionType = TransactionType.CREATE,
                        Description = LogMessages.CustomerTransaction.DatabaseTransactionHasFailed,
                    }));
                }

                return null;
            }

            var order = _mapper.Map<Order>(orderResponse);
            await _applicationDbContext.AddAsync(order);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                orderResponse.Id = order.Id;

                await _mediator.Send(new DeleteShoppingCart(command.Identity));

                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }
            }
            catch (Exception exception)
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync(cancellationToken);
                }

                await _mediator.Publish(new CreateLog(new CustomerTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.ORDER,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.CustomerTransaction.DatabaseTransactionHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            return orderResponse;
        }

        #endregion
    }
}
