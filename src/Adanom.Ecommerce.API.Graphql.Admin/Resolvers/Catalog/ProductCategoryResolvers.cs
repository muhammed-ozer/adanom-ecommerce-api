using Adanom.Ecommerce.API.Data.Models;
using Adanom.Ecommerce.API.Graphql.DataLoaders;

namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ProductCategoryResponse))]
    public sealed class ProductCategoryResolvers
    {
        #region GetParentAsync

        public async Task<ProductCategoryResponse?> GetParentAsync(
           [Parent] ProductCategoryResponse productCategoryResponse,
           [Service] IMediator mediator)
        {
            if (productCategoryResponse.ParentId == null)
            {
                return null;
            }

            var parent = await mediator.Send(new GetProductCategory(productCategoryResponse.ParentId.Value));

            return parent;
        }

        #endregion

        #region GetChildrenAsync

        public async Task<IEnumerable<ProductCategoryResponse>> GetChildrenAsync(
           [Parent] ProductCategoryResponse productCategoryResponse,
           [Service] IMediator mediator)
        {
            var children = await mediator.Send(new GetProductCategories(new GetProductCategoriesFilter()
            { 
                ParentId = productCategoryResponse.Id
            }));

            return children.Rows;
        }

        #endregion

        #region GetImagesAsync

        public async Task<IEnumerable<ImageResponse>> GetImagesAsync(
           [Parent] ProductCategoryResponse productCategoryResponse,
           [Service] ImagesByEntityDataLoader dataLoader,
           [Service] IMediator mediator,
           [Service] IMapper mapper)
        {
            var images = await dataLoader.LoadAsync((productCategoryResponse.Id, EntityType.PRODUCTCATEGORY));

            return mapper.Map<List<ImageResponse>>(images);
        }

        #endregion
    }
}
