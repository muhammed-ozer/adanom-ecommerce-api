using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateProductSpecificationAttributeGroupHandler : IRequestHandler<UpdateProductSpecificationAttributeGroup, ProductSpecificationAttributeGroupResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateProductSpecificationAttributeGroupHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductSpecificationAttributeGroupResponse?> Handle(UpdateProductSpecificationAttributeGroup command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productSpecificationAttributeGroup = await _applicationDbContext.ProductSpecificationAttributeGroups
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productSpecificationAttributeGroup = _mapper.Map(command, productSpecificationAttributeGroup);

            productSpecificationAttributeGroup.UpdatedAtUtc = DateTime.UtcNow;
            productSpecificationAttributeGroup.UpdatedByUserId = userId;

            _applicationDbContext.Update(productSpecificationAttributeGroup);
            await _applicationDbContext.SaveChangesAsync();

            var productSpecificationAttributeGroupResponse = _mapper.Map<ProductSpecificationAttributeGroupResponse>(productSpecificationAttributeGroup);

            await _mediator.Publish(new UpdateFromCache<ProductSpecificationAttributeGroupResponse>(productSpecificationAttributeGroupResponse));

            return productSpecificationAttributeGroupResponse;
        }

        #endregion
    }
}
