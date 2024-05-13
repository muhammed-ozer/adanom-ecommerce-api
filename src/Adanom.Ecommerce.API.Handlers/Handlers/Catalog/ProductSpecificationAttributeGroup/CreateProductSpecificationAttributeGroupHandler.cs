using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductSpecificationAttributeGroupHandler : 
        IRequestHandler<CreateProductSpecificationAttributeGroup, ProductSpecificationAttributeGroupResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProductSpecificationAttributeGroupHandler(
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

            await _applicationDbContext.AddAsync(productSpecificationAttributeGroup);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"ProductSpecificationAttributeGroup_Create_Failed: {exception.Message}");

                return null;
            }

            var productSpecificationAttributeGroupResponse = _mapper.Map<ProductSpecificationAttributeGroupResponse>(productSpecificationAttributeGroup);

            await _mediator.Publish(new AddToCache<ProductSpecificationAttributeGroupResponse>(productSpecificationAttributeGroupResponse));

            return productSpecificationAttributeGroupResponse;
        }

        #endregion
    }
}
