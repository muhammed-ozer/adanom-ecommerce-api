using Adanom.Ecommerce.API.Data.Models;

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

        #region GetProductSKUsAsync

        public async Task<IEnumerable<ProductSKUResponse>> GetProductSKUsAsync(
           [Parent] ProductResponse productResponse,
           [Service] IMediator mediator)
        {
            var productSKUs = await mediator.Send(new GetProductSKUs(new GetProductSKUsFilter()
            {
                ProductId = productResponse.Id
            }));

            return productSKUs.Rows;
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

        #region GetMetaInformationAsync

        public async Task<MetaInformationResponse?> GetMetaInformationAsync(
           [Parent] ProductResponse productResponse,
           [Service] IMediator mediator)
        {
            var metaInformation = await mediator.Send(new GetMetaInformation(productResponse.Id, EntityType.PRODUCT));

            return metaInformation;
        }

        #endregion

        #region GetImagesAsync

        public async Task<IEnumerable<ImageResponse>> GetImagesAsync(
           [Parent] ProductResponse productResponse,
           [Service] IMediator mediator)
        {
            var images = await mediator.Send(new GetEntityImages(productResponse.Id, EntityType.PRODUCT));

            return images;
        }

        #endregion

        #region GetDefaultImageAsync

        public async Task<ImageResponse?> GetDefaultImageAsync(
           [Parent] ProductResponse productResponse,
           [Service] IMediator mediator)
        {
            var image = await mediator.Send(new GetEntityDefaultImage(productResponse.Id, EntityType.PRODUCT));

            return image;
        }

        #endregion
    }
}
