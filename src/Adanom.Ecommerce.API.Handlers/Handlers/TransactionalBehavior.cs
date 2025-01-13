using System.Transactions;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class TransactionalBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : class, IRequest<TResponse>
    {
        private readonly IMediator _mediator;

        public TransactionalBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        public async Task<TResponse> Handle(TRequest command, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var isTransactional = typeof(TRequest).GetCustomAttributes(typeof(TransactionalAttribute), true).Any();

            if (!isTransactional)
            {
                try
                {
                    return await next();
                } catch
                {
                    // TODO: Implement logging
                }
            }

            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                var response = await next();

                if (typeof(TResponse) == typeof(bool))
                {
                    if (response is bool booleanResponse && !booleanResponse)
                    {
                        return response;
                    }
                }

                if (response == null)
                {
                    return response;
                }

                transactionScope.Complete();

                return response;
            }
            catch {
                // TODO: Implement logging
                //await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                //{
                //    UserId = userId,
                //    EntityType = EntityType.BRAND,
                //    TransactionType = TransactionType.DELETE,
                //    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                //    Exception = exception.ToString()
                //}));

                throw;
            }
        }
    }
}
