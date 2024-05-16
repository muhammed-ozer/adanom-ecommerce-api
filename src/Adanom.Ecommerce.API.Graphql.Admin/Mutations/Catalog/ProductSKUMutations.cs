using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductSKUMutations
    {
        #region CreateProductSKUAsync

        [GraphQLDescription("Creates a product SKU")]
        public async Task<ProductSKUResponse?> CreateProductSKUAsync(
            CreateProductSKURequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateProductSKU(identity));

            return await mediator.Send(command);
        }

        #endregion

        #region UpdateProductSKUStockAsync

        [GraphQLDescription("Updates a product SKU stock")]
        public async Task<bool> UpdateProductSKUStockAsync(
            UpdateProductSKUStockRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductSKUStock(identity));

            return await mediator.Send(command);
        }

        #endregion

        #region BatchUpdateProductSKUStocksAsync

        [GraphQLDescription("Updates batch product SKU stocks")]
        public async Task<bool> BatchUpdateProductSKUStocksAsync(
            BatchUpdateProductSKUStocksRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new BatchUpdateProductSKUStocks(identity));

            return await mediator.Send(command);
        }

        #endregion

        #region UpdateProductSKUBarcodesAsync

        [GraphQLDescription("Updates a product SKU barcodes")]
        public async Task<bool> UpdateProductSKUBarcodesAsync(
            UpdateProductSKUBarcodesRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductSKUBarcodes(identity));

            return await mediator.Send(command);
        }

        #endregion

        #region UpdateProductSKUInstallmentAsync

        [GraphQLDescription("Updates a product SKU installment")]
        public async Task<bool> UpdateProductSKUInstallmentAsync(
            UpdateProductSKUInstallmentRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductSKUInstallment(identity));

            return await mediator.Send(command);
        }

        #endregion

        #region UpdateProductSKU_ProductAttributeAsync

        [GraphQLDescription("Updates a product SKU product attribute")]
        public async Task<bool> UpdateProductSKU_ProductAttributeAsync(
            UpdateProductSKU_ProductAttributeRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductSKU_ProductAttribute(identity));

            return await mediator.Send(command);
        }

        #endregion

        #region DeleteProductSKUAsync

        [GraphQLDescription("Deletes a product SKU")]
        public async Task<bool> DeleteProductSKUAsync(
            DeleteProductSKURequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteProductSKU(identity));

            return await mediator.Send(command);
        }

        #endregion
    }
}
