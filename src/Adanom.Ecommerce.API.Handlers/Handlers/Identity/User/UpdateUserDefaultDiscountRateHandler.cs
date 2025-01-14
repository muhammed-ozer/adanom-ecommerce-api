using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateUserDefaultDiscountRateHandler : IRequestHandler<UpdateUserDefaultDiscountRate, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateUserDefaultDiscountRateHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            UserManager<User> userManager,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateUserDefaultDiscountRate command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var user = await _userManager.Users
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .SingleAsync();

            user = _mapper.Map(command, user, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedByUserId = userId;
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            applicationDbContext.Update(user);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
