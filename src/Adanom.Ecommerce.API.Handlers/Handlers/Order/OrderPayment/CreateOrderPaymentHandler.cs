using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrderPaymentHandler : IRequestHandler<CreateOrderPayment, bool>

    {
        #region Fields

        IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateOrderPaymentHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateOrderPayment command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var orderPayment = _mapper.Map<CreateOrderPayment, OrderPayment>(command);

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync();

            await applicationDbContext.AddAsync(orderPayment);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
