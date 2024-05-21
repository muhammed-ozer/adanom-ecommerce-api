using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateReturnRequest_ReturnRequestStatusTypeHandler : IRequestHandler<UpdateReturnRequest_ReturnRequestStatusType, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateReturnRequest_ReturnRequestStatusTypeHandler(
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

        public async Task<bool> Handle(UpdateReturnRequest_ReturnRequestStatusType command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var returnRequest = await _applicationDbContext.ReturnRequests
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            command.OldReturnRequestStatusType = returnRequest.ReturnRequestStatusType;

            returnRequest = _mapper.Map(command, returnRequest);

            returnRequest.UpdatedAtUtc = DateTime.UtcNow;
            returnRequest.UpdatedByUserId = userId;

            _applicationDbContext.Update(returnRequest);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.RETURNREQUEST,
                    TransactionType = TransactionType.UPDATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.RETURNREQUEST,
                TransactionType = TransactionType.UPDATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, returnRequest.Id),
            }));

            return true;
        }

        #endregion
    }
}
