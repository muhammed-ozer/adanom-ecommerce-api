using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdatePickUpStoreHandler : IRequestHandler<UpdatePickUpStore, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdatePickUpStoreHandler(
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

        public async Task<bool> Handle(UpdatePickUpStore command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var pickUpStore = await applicationDbContext.PickUpStores
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            if (command.IsDefault && !pickUpStore.IsDefault)
            {
                var cuurentDefaultpickUpStore = await applicationDbContext.PickUpStores
                    .Where(e => e.DeletedAtUtc == null &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (cuurentDefaultpickUpStore != null)
                {
                    cuurentDefaultpickUpStore.IsDefault = false;
                }
            }
            else
            {
                command.IsDefault = true;
            }

            pickUpStore = _mapper.Map(command, pickUpStore, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedByUserId = userId;
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            applicationDbContext.Update(pickUpStore);
            await applicationDbContext.SaveChangesAsync();
           
            await _mediator.Publish(new ClearEntityCache<PickUpStoreResponse>());

            return true;
        }

        #endregion
    }
}
