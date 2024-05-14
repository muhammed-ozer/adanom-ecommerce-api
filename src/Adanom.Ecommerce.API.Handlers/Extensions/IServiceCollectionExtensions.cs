using Adanom.Ecommerce.API.Handlers;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddMediatR(options =>
            {
                options.Lifetime = ServiceLifetime.Scoped;

                options.RegisterServicesFromAssemblies(typeof(LoginHandler).Assembly, typeof(Login).Assembly);

                #region Catalog

                #region Brand

                options.AddBehavior<IPipelineBehavior<CreateBrand, BrandResponse?>, CreateBrand_CommitTransactionBehavior>();
                options.AddBehavior<IPipelineBehavior<CreateBrand, BrandResponse?>, CreateBrand_CreateMetaInformationBehavior>();

                options.AddBehavior<IPipelineBehavior<DeleteBrand, bool>, DeleteBrand_DeleteRelationsBehavior>();

                #endregion

                #region Product

                options.AddBehavior<IPipelineBehavior<CreateProduct, ProductResponse?>, CreateProduct_CommitTransactionBehavior>();
                options.AddBehavior<IPipelineBehavior<CreateProduct, ProductResponse?>, CreateProduct_CreateMetaInformationBehavior>();
                options.AddBehavior<IPipelineBehavior<CreateProduct, ProductResponse?>, CreateProduct_CreateProduct_ProductCategoryBehavior>();
                options.AddBehavior<IPipelineBehavior<CreateProduct, ProductResponse?>, CreateProduct_CreateProductSKUBehavior>();

                options.AddBehavior<IPipelineBehavior<DeleteProduct, bool>, DeleteProduct_CommitTransactionBehavior>();
                options.AddBehavior<IPipelineBehavior<DeleteProduct, bool>, DeleteProduct_DeleteRelationsBehavior>();

                #endregion

                #region ProductSKU

                options.AddBehavior<IPipelineBehavior<DeleteProductSKU, bool>, DeleteProductSKU_CommitTransactionBehavior>();
                options.AddBehavior<IPipelineBehavior<DeleteProductSKU, bool>, DeleteProductSKU_DeleteRelationsBehavior>();

                #endregion

                #region ProductCategory

                options.AddBehavior<IPipelineBehavior<DeleteProductCategory, bool>, DeleteProductCategory_DeleteRelationsBehavior>();

                #endregion

                #region ProductSpecificationAttribute

                options.AddBehavior<IPipelineBehavior<DeleteProductSpecificationAttribute, bool>, DeleteProductSpecificationAttribute_DeleteRelationsBehavior>();

                #endregion

                #region ProductTag

                options.AddBehavior<IPipelineBehavior<DeleteProductTag, bool>, DeleteProductTag_DeleteRelationsBehavior>();

                #endregion

                #endregion

                #region Auth

                #region RegisterUser

                options.AddBehavior<IPipelineBehavior<RegisterUser, bool>, RegisterUser_SendMailsBehavior>();
                options.AddBehavior<IPipelineBehavior<RegisterUser, bool>, RegisterUser_CreateNotificationBehavior>();

                #endregion

                #endregion
            });

            return services;
        }
    }
}
