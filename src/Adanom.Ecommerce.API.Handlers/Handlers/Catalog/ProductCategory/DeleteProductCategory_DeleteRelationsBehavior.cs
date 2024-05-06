namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductCategory_DeleteRelationsBehavior : IPipelineBehavior<DeleteProductCategory, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProductCategory_DeleteRelationsBehavior(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteProductCategory command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteProductCategoryResponse = await next();

            if (deleteProductCategoryResponse)
            {
                // TODO: Test here after create azure blob storage 

                var productCategoryImageMappings = await _applicationDbContext.Image_Entity_Mappings
                                .Where(e => e.EntityType == EntityType.PRODUCTCATEGORY &&
                                            e.EntityId == command.Id)
                                .ToListAsync();

                if (productCategoryImageMappings.Any())
                {
                    foreach (var productCategoryImageMapping in productCategoryImageMappings)
                    {
                        // TODO: Delete images
                    }
                }

                var productCategoryMetaInformationMappings = await _applicationDbContext.MetaInformation_Entity_Mappings
                    .Where(e => e.EntityType == EntityType.PRODUCTCATEGORY && 
                                e.EntityId == command.Id)
                    .ToListAsync();

                if (productCategoryMetaInformationMappings.Any())
                {
                    foreach (var productCategoryMetaInformationMapping in productCategoryMetaInformationMappings)
                    {
                        // TODO: Delete meta informations
                    }
                }
            }

            return deleteProductCategoryResponse;
        }

        #endregion
    }
}
