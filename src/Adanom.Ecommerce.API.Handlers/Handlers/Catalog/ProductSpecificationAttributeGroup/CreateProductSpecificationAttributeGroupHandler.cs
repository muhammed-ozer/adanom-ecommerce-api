﻿using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductSpecificationAttributeGroupHandler :
        IRequestHandler<CreateProductSpecificationAttributeGroup, ProductSpecificationAttributeGroupResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProductSpecificationAttributeGroupHandler(
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

        public async Task<ProductSpecificationAttributeGroupResponse?> Handle(CreateProductSpecificationAttributeGroup command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productSpecificationAttributeGroup = _mapper
                .Map<CreateProductSpecificationAttributeGroup, ProductSpecificationAttributeGroup>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(productSpecificationAttributeGroup);
            await applicationDbContext.SaveChangesAsync();

            var productSpecificationAttributeGroupResponse = _mapper.Map<ProductSpecificationAttributeGroupResponse>(productSpecificationAttributeGroup);

            await _mediator.Publish(new AddToCache<ProductSpecificationAttributeGroupResponse>(productSpecificationAttributeGroupResponse));

            return productSpecificationAttributeGroupResponse;
        }

        #endregion
    }
}
