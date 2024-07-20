using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateReturnRequest_CommitTransactionBehavior : IPipelineBehavior<CreateReturnRequest, ReturnRequestResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateReturnRequest_CommitTransactionBehavior(
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

        public async Task<ReturnRequestResponse?> Handle(CreateReturnRequest command, RequestHandlerDelegate<ReturnRequestResponse?> next, CancellationToken cancellationToken)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            var returnRequestResponse = await next();

            if (returnRequestResponse == null)
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync(cancellationToken);

                    await _mediator.Publish(new CreateLog(new CustomerTransactionLogRequest()
                    {
                        UserId = command.Identity.GetUserId(),
                        EntityType = EntityType.RETURNREQUEST,
                        TransactionType = TransactionType.CREATE,
                        Description = LogMessages.CustomerTransaction.DatabaseTransactionHasFailed,
                    }));
                }

                return null;
            }

            var returnRequest = _mapper.Map<ReturnRequest>(returnRequestResponse);
            await _applicationDbContext.AddAsync(returnRequest);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                returnRequestResponse.Id = returnRequest.Id;

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
                    EntityType = EntityType.RETURNREQUEST,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.CustomerTransaction.DatabaseTransactionHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            return returnRequestResponse;
        }

        #endregion
    }
}
