using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductSpecificationAttributeHandler :
        IRequestHandler<CreateProductSpecificationAttribute, ProductSpecificationAttributeResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProductSpecificationAttributeHandler(
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

        public async Task<ProductSpecificationAttributeResponse?> Handle(CreateProductSpecificationAttribute command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productSpecificationAttribute = _mapper
                .Map<CreateProductSpecificationAttribute, ProductSpecificationAttribute>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(productSpecificationAttribute);
            await applicationDbContext.SaveChangesAsync();
            
            var productSpecificationAttributeResponse = _mapper.Map<ProductSpecificationAttributeResponse>(productSpecificationAttribute);

            await _mediator.Publish(new AddToCache<ProductSpecificationAttributeResponse>(productSpecificationAttributeResponse));

            return productSpecificationAttributeResponse;
        }

        #endregion
    }
}
