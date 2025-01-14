using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductSKUInstallmentHandler : IRequestHandler<UpdateProductSKUInstallment, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductSKUInstallmentHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateProductSKUInstallment command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productSKU = await applicationDbContext.ProductSKUs
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productSKU = _mapper.Map(command, productSKU, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedByUserId = userId;
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            applicationDbContext.Update(productSKU);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
