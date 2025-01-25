using Adanom.Ecommerce.API.Data.Models;
using Adanom.Ecommerce.API.Graphql.DataLoaders;

namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ProductResponse))]
    public sealed class ProductResolvers
    {
        #region GetBrandAsync

        public async Task<BrandResponse?> GetBrandAsync(
           [Parent] ProductResponse productResponse,
           [Service] IMediator mediator)
        {
            if (productResponse.BrandId == null)
            {
                return null;
            }

            var brand = await mediator.Send(new GetBrand(productResponse.BrandId.Value));

            return brand;
        }

        #endregion

        #region GetProductCategoriesAsync

        public async Task<IEnumerable<ProductCategoryResponse>> GetProductCategoriesAsync(
           [Parent] ProductResponse productResponse,
           [Service] IMediator mediator)
        {
            var productCategories = await mediator.Send(new GetProduct_ProductCategories(productResponse.Id));

            return productCategories;
        }

        #endregion

        #region GetProductSKUAsync

        public async Task<ProductSKUResponse?> GetProductSKUAsync(
           [Parent] ProductResponse productResponse,
           [Service] ProductSKUByProductIdDataLoader dataLoader,
           [Service] IMediator mediator,
           [Service] IMapper mapper)
        {
            var productSKU = await dataLoader.LoadAsync(productResponse.Id);

            return mapper.Map<ProductSKUResponse>(productSKU);
        }

        #endregion

        #region GetProductTagsAsync

        public async Task<IEnumerable<ProductTagResponse>> GetProductTagsAsync(
           [Parent] ProductResponse productResponse,
           [Service] IMediator mediator)
        {
            var productTags = await mediator.Send(new GetProduct_ProductTags(productResponse.Id));

            return productTags;
        }

        #endregion

        #region GetProductSpecificationAttributesAsync

        public async Task<IEnumerable<ProductSpecificationAttributeResponse>> GetProductSpecificationAttributesAsync(
           [Parent] ProductResponse productResponse,
           [Service] IMediator mediator)
        {
            var productSpecificationAttributes = await mediator.Send(new GetProduct_ProductSpecificationAttributes(productResponse.Id));

            return productSpecificationAttributes;
        }

        #endregion

        #region GetImagesAsync

        public async Task<IEnumerable<ImageResponse>> GetImagesAsync(
           [Parent] ProductResponse productResponse,
           [Service] ImagesByEntityDataLoader dataLoader,
           [Service] IMediator mediator,
           [Service] IMapper mapper)
        {
            var images = await dataLoader.LoadAsync((productResponse.Id, EntityType.PRODUCT));

            return mapper.Map<List<ImageResponse>>(images);
        }

        #endregion

        #region GetDefaultImageAsync

        public async Task<ImageResponse?> GetDefaultImageAsync(
           [Parent] ProductResponse productResponse,
           [Service] DefaultEntityImageDataLoader dataLoader,
           [Service] IMediator mediator,
           [Service] IMapper mapper)
        {
            var defaultImage = await dataLoader.LoadAsync((productResponse.Id, EntityType.PRODUCT));

            return mapper.Map<ImageResponse>(defaultImage);
        }

        #endregion

        #region GetProductReviewsAsync

        public async Task<IEnumerable<ProductReviewResponse>> GetProductReviewsAsync(
           [Parent] ProductResponse productResponse,
           [Service] IMediator mediator)
        {
            var productReviews = await mediator.Send(new GetProductReviews(new GetProductReviewsFilter()
            {
                ProductId = productResponse.Id,
            }));

            return productReviews.Rows;
        }

        #endregion
    }
}
