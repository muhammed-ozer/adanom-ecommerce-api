﻿using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductSpecificationAttributeGroupHandler : IRequestHandler<UpdateProductSpecificationAttributeGroup, ProductSpecificationAttributeGroupResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductSpecificationAttributeGroupHandler(
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

        public async Task<ProductSpecificationAttributeGroupResponse?> Handle(UpdateProductSpecificationAttributeGroup command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productSpecificationAttributeGroup = await applicationDbContext.ProductSpecificationAttributeGroups
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productSpecificationAttributeGroup = _mapper.Map(command, productSpecificationAttributeGroup);

            productSpecificationAttributeGroup.UpdatedAtUtc = DateTime.UtcNow;
            productSpecificationAttributeGroup.UpdatedByUserId = userId;

            applicationDbContext.Update(productSpecificationAttributeGroup);
            await applicationDbContext.SaveChangesAsync();

            var productSpecificationAttributeGroupResponse = _mapper.Map<ProductSpecificationAttributeGroupResponse>(productSpecificationAttributeGroup);

            await _mediator.Publish(new UpdateFromCache<ProductSpecificationAttributeGroupResponse>(productSpecificationAttributeGroupResponse));

            return productSpecificationAttributeGroupResponse;
        }

        #endregion
    }
}
