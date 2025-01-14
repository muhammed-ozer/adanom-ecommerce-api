using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductIsNewHandler : IRequestHandler<UpdateProductIsNew, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductIsNewHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateProductIsNew command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var product = await applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            product = _mapper.Map(command, product, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedByUserId = userId;
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            applicationDbContext.Update(product);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
