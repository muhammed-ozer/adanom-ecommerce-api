namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProduct_DeleteRelationsBehavior : IPipelineBehavior<DeleteProduct, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public DeleteProduct_DeleteRelationsBehavior(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteProduct command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteProductResponse = await next();

            if (!deleteProductResponse)
            {
                return deleteProductResponse;
            }

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            #region Product_ProductCategory

            var product_ProductCategories = await applicationDbContext.Product_ProductCategory_Mappings
                .Where(e => e.ProductId == command.Id)
                .ToListAsync();

            if (product_ProductCategories.Any())
            {
                var deleteProduct_ProductCategoryRequests = _mapper
                    .Map<IEnumerable<DeleteProduct_ProductCategoryRequest>>(product_ProductCategories);

                foreach (var deleteProduct_ProductCategoryRequest in deleteProduct_ProductCategoryRequests)
                {
                    var deleteProduct_ProductCategoryCommand = _mapper
                        .Map(deleteProduct_ProductCategoryRequest, new DeleteProduct_ProductCategory(command.Identity));

                    await _mediator.Send(deleteProduct_ProductCategoryCommand);
                }
            }

            #endregion

            #region Product_ProductSpecificationAttribute

            var product_ProductSpecificationAttributes = await applicationDbContext.Product_ProductSpecificationAttribute_Mappings
                       .Where(e => e.ProductId == command.Id)
                       .ToListAsync();

            if (product_ProductSpecificationAttributes.Any())
            {
                var product_ProductSpecificationAttributeRequests = _mapper
                    .Map<IEnumerable<DeleteProduct_ProductSpecificationAttributeRequest>>(product_ProductSpecificationAttributes);

                foreach (var product_ProductSpecificationAttributeRequest in product_ProductSpecificationAttributeRequests)
                {
                    var deleteProduct_ProductSpecificationAttributeCommand = _mapper
                        .Map(product_ProductSpecificationAttributeRequest, new DeleteProduct_ProductSpecificationAttribute(command.Identity));

                    await _mediator.Send(deleteProduct_ProductSpecificationAttributeCommand);
                }
            }

            #endregion

            #region Product_ProductTag

            var product_ProductTags = await applicationDbContext.Product_ProductTag_Mappings
                        .Where(e => e.ProductId == command.Id)
                        .ToListAsync();

            if (product_ProductTags.Any())
            {
                var product_ProductTagRequests = _mapper
                    .Map<IEnumerable<DeleteProduct_ProductTagRequest>>(product_ProductTags);

                foreach (var product_ProductTagRequest in product_ProductTagRequests)
                {
                    var deleteProduct_ProductTagCommand = _mapper
                        .Map(product_ProductTagRequest, new DeleteProduct_ProductTag(command.Identity));

                    await _mediator.Send(deleteProduct_ProductTagCommand);
                }
            }

            #endregion

            #region Images

            var deleteImagesRequest = new DeleteImagesRequest()
            {
                EntityId = command.Id,
                EntityType = EntityType.PRODUCT
            };

            var deleteImagesCommand = _mapper
                .Map(deleteImagesRequest, new DeleteImages(command.Identity));

            await _mediator.Send(deleteImagesCommand);

            #endregion

            #region ProductSKU

            var product_productSKUMappings = await applicationDbContext.Product_ProductSKU_Mappings
                .Where(e => e.ProductId == command.Id)
                .ToListAsync();

            foreach (var product_productSKUMapping in product_productSKUMappings)
            {
                var deleteProductSKURequest = new DeleteProductSKURequest()
                {
                    Id = product_productSKUMapping.ProductSKUId,
                };

                var deleteProductSKUCommand = _mapper
                    .Map(deleteProductSKURequest, new DeleteProductSKU(command.Identity));

                await _mediator.Send(deleteProductSKUCommand);

                await _mediator.Send(new DeleteProduct_ProductSKU(command.Identity)
                {
                    ProductId = command.Id,
                    ProductSKUId = product_productSKUMapping.ProductSKUId
                });
            }

            #endregion

            // TODO: Remove shopping cart items

            // TODO: Remove anonymous shopping cart items

            // TODO: Remove favorite items

            // TODO: Remove stock notification items

            // TODO: Remove reviews

            return deleteProductResponse;
        }

        #endregion
    }
}
