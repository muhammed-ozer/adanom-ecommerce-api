﻿using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateBillingAddressHandler : IRequestHandler<UpdateBillingAddress, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateBillingAddressHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateBillingAddress command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var billingAddress = await applicationDbContext.BillingAddresses
                .Where(e => e.DeletedAtUtc == null &&
                            e.UserId == userId &&
                            e.Id == command.Id)
                .SingleAsync();

            if (command.IsDefault && !billingAddress.IsDefault)
            {
                var curentDefaultBillingAddress = await applicationDbContext.BillingAddresses
                    .Where(e => e.DeletedAtUtc == null &&
                                e.UserId == userId &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (curentDefaultBillingAddress != null)
                {
                    curentDefaultBillingAddress.IsDefault = false;
                }
            }
            else if (!command.IsDefault && billingAddress.IsDefault)
            {
                billingAddress.IsDefault = false;
            }
            else
            {
                command.IsDefault = false;
            }

            billingAddress = _mapper.Map(command, billingAddress, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            applicationDbContext.Update(billingAddress);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
