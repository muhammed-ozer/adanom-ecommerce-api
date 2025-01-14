using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
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
                } catch (Exception exception)
                {
                    await CreateLogAsync(command, exception);

                    return default;
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
            catch(Exception exception) 
            {
                transactionScope.Dispose();

                await CreateLogAsync(command, exception);

                throw;
            }
        }

        private async Task CreateLogAsync(TRequest command, Exception exception)
        {
            var transactionLogRequest = new TransactionLogRequest()
            {
                LogLevel = LogLevel.ERROR,
                CommandName = typeof(TRequest).Name,
                Exception = exception.ToString()
            };

            var filteredProperties = typeof(TRequest)
                .GetProperties()
                .Where(p => p.PropertyType != typeof(ClaimsPrincipal)) // Identity gibi karmaşık yapıları hariç tut
                .ToDictionary(p => p.Name, p => p.GetValue(command));

            string serializedCommand = JsonSerializer.Serialize(filteredProperties, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = false
            });

            transactionLogRequest.CommandValues = serializedCommand;

            var identity = typeof(TRequest)
                .GetProperties()
                .Where(p => p.PropertyType == typeof(ClaimsPrincipal)) // Identity gibi karmaşık yapıları hariç tut
                .ToDictionary(p => p.Name, p => p.GetValue(command))
                .SingleOrDefault();

            if (identity.Value != null)
            {
                var identityValue = identity.Value as ClaimsPrincipal;

                if (identityValue != null)
                {
                    var userId = identityValue.GetUserId();

                    transactionLogRequest.UserId = userId;
                }
            }

            await _mediator.Publish(new CreateLog(transactionLogRequest));
        }
    }
}
