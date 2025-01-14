using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProductSKUHandler : IRequestHandler<CreateProductSKU, ProductSKUResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProductSKUHandler(
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

        public async Task<ProductSKUResponse?> Handle(CreateProductSKU command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productSKU = _mapper.Map<CreateProductSKU, ProductSKU>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.Code = source.Code.ToUpper();
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            var createProductPriceCommand = _mapper.Map(command.CreateProductPriceRequest, new CreateProductPrice(command.Identity));

            var productPriceResponse = await _mediator.Send(createProductPriceCommand);

            if (productPriceResponse == null)
            {
                return null;
            }

            productSKU.ProductPriceId = productPriceResponse.Id;

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(productSKU);
            await applicationDbContext.SaveChangesAsync();

            var productSKUResponse = _mapper.Map<ProductSKUResponse>(productSKU);

            return productSKUResponse;
        }

        #endregion
    }
}
