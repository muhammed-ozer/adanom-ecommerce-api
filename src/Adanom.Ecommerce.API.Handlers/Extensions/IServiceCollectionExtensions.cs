using Adanom.Ecommerce.API.Handlers;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionalBehavior<,>));

            services.AddMediatR(options =>
            {
                options.Lifetime = ServiceLifetime.Scoped;

                options.RegisterServicesFromAssemblies(typeof(LoginHandler).Assembly, typeof(Login).Assembly);

                #region Catalog

                #region Brand

                options.AddBehavior<IPipelineBehavior<DeleteBrand, bool>, DeleteBrand_DeleteRelationsBehavior>();

                #endregion

                #region Product

                options.AddBehavior<IPipelineBehavior<CreateProduct, ProductResponse?>, CreateProduct_CreateProduct_ProductCategoryBehavior>();
                options.AddBehavior<IPipelineBehavior<CreateProduct, ProductResponse?>, CreateProduct_CreateProductSKUBehavior>();

                options.AddBehavior<IPipelineBehavior<DeleteProduct, bool>, DeleteProduct_DeleteRelationsBehavior>();

                #endregion

                #region ProductSKU

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

                #region ProductReview

                options.AddBehavior<IPipelineBehavior<CreateProductReview, bool>, CreateProductReveiw_UpdatePointsBehavior>();

                #endregion

                #endregion

                #region Common

                #region ShippingProvider

                options.AddBehavior<IPipelineBehavior<DeleteShippingProvider, bool>, DeleteShippingProvider_DeleteRelationsBehavior>();

                #endregion

                #region LocalDeliveryProvider

                options.AddBehavior<IPipelineBehavior<DeleteLocalDeliveryProvider, bool>, DeleteLocalDeliveryProvider_DeleteRelationsBehavior>();

                #endregion

                #endregion

                #region Cms

                #region SliderItem

                options.AddBehavior<IPipelineBehavior<CreateSliderItem, SliderItemResponse?>, CreateSliderItem_CreateImageBehavior>();

                options.AddBehavior<IPipelineBehavior<DeleteSliderItem, bool>, DeleteSliderItem_DeleteImageBehavior>();

                #endregion

                #endregion

                #region Order

                #region Order
                
                options.AddBehavior<IPipelineBehavior<CreateOrder, OrderResponse?>, CreateOrder_SaveChangesBehavior>();
                options.AddBehavior<IPipelineBehavior<CreateOrder, OrderResponse?>, CreateOrder_CalculateTotalBehavior>();
                options.AddBehavior<IPipelineBehavior<CreateOrder, OrderResponse?>, CreateOrder_CalculateShippingBehavior>();
                options.AddBehavior<IPipelineBehavior<CreateOrder, OrderResponse?>, CreateOrder_ConvertShoppingCartItemsToOrderItems>();
                options.AddBehavior<IPipelineBehavior<CreateOrder, OrderResponse?>, CreateOrder_CreateAddressesBehavior>();

                options.AddBehavior<IPipelineBehavior<UpdateOrder_OrderStatusType, bool>, UpdateOrder_OrderStatusTypeCreateNotification>();
                options.AddBehavior<IPipelineBehavior<UpdateOrder_OrderStatusType, bool>, UpdateOrder_OrderStatusTypeSendMailsBehavior>();

                #endregion

                #region Checkout

                options.AddBehavior<IPipelineBehavior<GetCheckout, CheckoutResponse?>, GetCheckout_CalculateShippingBehavior>();
                options.AddBehavior<IPipelineBehavior<GetCheckout, CheckoutResponse?>, GetCheckout_CalculateShoppingCartSummaryBehavior>();

                #endregion

                #region ReturnRequest

                options.AddBehavior<IPipelineBehavior<CreateReturnRequest, ReturnRequestResponse?>, CreateReturnRequest_CreateNotificationBehavior>();
                options.AddBehavior<IPipelineBehavior<CreateReturnRequest, ReturnRequestResponse?>, CreateReturnRequest_SendMailsBehavior>();
                options.AddBehavior<IPipelineBehavior<CreateReturnRequest, ReturnRequestResponse?>, CreateReturnRequest_SaveChangesBehavior>();
                options.AddBehavior<IPipelineBehavior<CreateReturnRequest, ReturnRequestResponse?>, CreateReturnRequest_CalculateTotalBehavior>();
                options.AddBehavior<IPipelineBehavior<CreateReturnRequest, ReturnRequestResponse?>, CreateReturnRequest_CreateReturnRequestItemsBehavior>();

                options.AddBehavior<IPipelineBehavior<UpdateReturnRequest_ReturnRequestStatusType, bool>, UpdateReturnRequest_ReturnRequestStatusTypeSendMailsBehavior>();

                #endregion

                #region ShoppingCart

                options.AddBehavior<IPipelineBehavior<GetShoppingCart, ShoppingCartResponse?>, GetShoppingCart_GetSummaryBehavior>();
                options.AddBehavior<IPipelineBehavior<GetShoppingCart, ShoppingCartResponse?>, GetShoppingCart_GetItemsBehavior>();

                #endregion

                #region ShoppingCartItem

                options.AddBehavior<IPipelineBehavior<DeleteShoppingCartItem, bool>, DeleteShoppingCartItem_DeleteShoppingCartBehavior>();

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
