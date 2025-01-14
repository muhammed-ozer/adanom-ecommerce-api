using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_ProductSpecificationAttributeHandler : IRequestHandler<CreateProduct_ProductSpecificationAttribute, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProduct_ProductSpecificationAttributeHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateProduct_ProductSpecificationAttribute command, CancellationToken cancellationToken)
        {
            var product_ProductSpecificationAttribute = new Product_ProductSpecificationAttribute_Mapping()
            {
                ProductId = command.ProductId,
                ProductSpecificationAttributeId = command.ProductSpecificationAttributeId
            };

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(product_ProductSpecificationAttribute);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
