﻿namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class ProductSpecificationAttributeGroupQueries
    {
        #region GetProductSpecificationAttributeGroupAsync

        [GraphQLDescription("Gets product specification attribute group")]
        public async Task<ProductSpecificationAttributeGroupResponse?> GetProductSpecificationAttributeGroupAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetProductSpecificationAttributeGroup(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetProductSpecificationAttributeGroupsAsync

        [GraphQLDescription("Gets product specification attribute groups")]
        public async Task<PaginatedData<ProductSpecificationAttributeGroupResponse>> GetProductSpecificationAttributeGroupsAsync(
            GetProductSpecificationAttributeGroupsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetProductSpecificationAttributeGroups(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
