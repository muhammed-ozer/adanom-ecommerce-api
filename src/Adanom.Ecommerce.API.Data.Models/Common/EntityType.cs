﻿namespace Adanom.Ecommerce.API.Data.Models
{
    public enum EntityType : byte
    {
        PRODUCT,
        PRODUCTCATEGORY,
        PRODUCTSKU,
        PRODUCTPRICE,
        PRODUCTSPECIFICATIONATTRIBUTE,
        PRODUCTSPECIFICATIONATTRIBUTEGROUP,
        PRODUCTTAG,
        PRODUCT_PRODUCTCATEGORY,
        PRODUCT_PRODUCTSPECIFICATIONATTRIBUTE,
        PRODUCT_PRODUCTTAG,
        BRAND,
        TAXCATEGORY,
        NOTIFICATION,
        IMAGE,
        PRODUCTREVIEW,
        TAXADMINISTRATION,
        SLIDERITEM,
        COMPANY,
        SHIPPINGPROVIDER,
        PICKUPSTORE,
        USER,
        ORDER,
        RETURNREQUEST,
        SHIPPINGADDRESS,
        BILLINGADDRESS,
        FAVORITEITEM,
        STOCKNOTIFICATION,
        SHOPPINGCART,
        SHOPPINGCARTITEM,
        ANONYMOUSSHOPPINGCART,
        ANONYMOUSSHOPPINGCARTITEM,
        LOCALDELIVERYPROVIDER,
        LOCALDELIVERYPROVIDER_ADDRESSDISTRICT,
    }
}
