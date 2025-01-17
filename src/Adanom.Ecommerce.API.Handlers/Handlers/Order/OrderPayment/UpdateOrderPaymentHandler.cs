using System.Security.Claims;
using HotChocolate.Execution;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateOrderPaymentHandler : IRequestHandler<UpdateOrderPayment, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateOrderPaymentHandler(
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

        public async Task<bool> Handle(UpdateOrderPayment command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var orderPayment = await applicationDbContext.OrderPayments
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            if (orderPayment.OrderPaymentType == OrderPaymentType.ONLINE_PAYMENT && string.IsNullOrEmpty(command.GatewayResponse))
            {
                var queryError = new Error("Ödeme işlemi için sağlayıcı yanıtı gereklidir.", ValidationErrorCodesEnum.NOT_ALLOWED.ToString());
                throw new QueryException(queryError);
            }

            if (command.Paid && command.PaidAtUtc == null)
            {
                command.PaidAtUtc = DateTime.UtcNow;
            }

            orderPayment = _mapper.Map(command, orderPayment);

            applicationDbContext.Update(orderPayment);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
