﻿using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductSpecificationAttributeHandler : IRequestHandler<UpdateProductSpecificationAttribute, ProductSpecificationAttributeResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductSpecificationAttributeHandler(
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

        public async Task<ProductSpecificationAttributeResponse?> Handle(UpdateProductSpecificationAttribute command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productSpecificationAttribute = await applicationDbContext.ProductSpecificationAttributes
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productSpecificationAttribute = _mapper.Map(command, productSpecificationAttribute);

            productSpecificationAttribute.UpdatedAtUtc = DateTime.UtcNow;
            productSpecificationAttribute.UpdatedByUserId = userId;

            applicationDbContext.Update(productSpecificationAttribute);

            await applicationDbContext.SaveChangesAsync();

            var productSpecificationAttributeResponse = _mapper.Map<ProductSpecificationAttributeResponse>(productSpecificationAttribute);

            await _mediator.Publish(new UpdateFromCache<ProductSpecificationAttributeResponse>(productSpecificationAttributeResponse));

            return productSpecificationAttributeResponse;
        }

        #endregion
    }
}
