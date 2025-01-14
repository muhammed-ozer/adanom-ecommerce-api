using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateReturnRequest_ReturnRequestStatusTypeHandler : IRequestHandler<UpdateReturnRequest_ReturnRequestStatusType, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateReturnRequest_ReturnRequestStatusTypeHandler(
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

        public async Task<bool> Handle(UpdateReturnRequest_ReturnRequestStatusType command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var returnRequest = await applicationDbContext.ReturnRequests
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            command.OldReturnRequestStatusType = returnRequest.ReturnRequestStatusType;

            returnRequest = _mapper.Map(command, returnRequest);

            returnRequest.UpdatedAtUtc = DateTime.UtcNow;
            returnRequest.UpdatedByUserId = userId;

            applicationDbContext.Update(returnRequest);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
