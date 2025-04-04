﻿using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductMutations
    {
        #region CreateProductAsync

        [GraphQLDescription("Creates a product")]
        public async Task<ProductResponse?> CreateProductAsync(
            CreateProductRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateProduct(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region BatchCreateProductsAsync

        [GraphQLDescription("Creates batch products")]
        public async Task<bool> BatchCreateProductsAsync(
            BatchCreateProductsRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new BatchCreateProducts(identity));

            return await mediator.Send(command);
        }

        #endregion


        #region UpdateProductNameAsync

        [GraphQLDescription("Updates a product name")]
        public async Task<ProductResponse?> UpdateProductNameAsync(
            UpdateProductNameRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductName(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductDescriptionAsync

        [GraphQLDescription("Updates a product description")]
        public async Task<ProductResponse?> UpdateProductDescriptionAsync(
            UpdateProductDescriptionRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductDescription(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductIsActiveAsync

        [GraphQLDescription("Updates a product is active")]
        public async Task<bool> UpdateProductIsActiveAsync(
            UpdateProductIsActiveRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductIsActive(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductIsNewAsync

        [GraphQLDescription("Updates a product is new")]
        public async Task<bool> UpdateProductIsNewAsync(
            UpdateProductIsNewRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductIsNew(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductIsInHighlightsAsync

        [GraphQLDescription("Updates a product is in highlights")]
        public async Task<bool> UpdateProductIsInHighlightsAsync(
            UpdateProductIsInHighlightsRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductIsInHighlights(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductIsInProductsOfTheWeekAsync

        [GraphQLDescription("Updates a product is in products of the week")]
        public async Task<bool> UpdateProductIsInProductsOfTheWeekAsync(
            UpdateProductIsInProductsOfTheWeekRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductIsInProductsOfTheWeek(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductDisplayOrderAsync

        [GraphQLDescription("Updates a product display order")]
        public async Task<bool> UpdateProductDisplayOrderAsync(
            UpdateProductDisplayOrderRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductDisplayOrder(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductBrandAsync

        [GraphQLDescription("Updates a product brand")]
        public async Task<ProductResponse?> UpdateProductBrandAsync(
            UpdateProductBrandRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductBrand(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteProductAsync

        [GraphQLDescription("Deletes a product")]
        public async Task<bool> DeleteProductAsync(
            DeleteProductRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteProduct(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
