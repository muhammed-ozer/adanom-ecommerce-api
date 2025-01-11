using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Services.Azure
{
    public static class AzureBlobStorageHelpers
    {
        public static string GetFolderName(EntityType entityType)
        {
            var folderName = string.Empty;

            switch (entityType)
            {
                case EntityType.PRODUCT:
                    folderName = AzureBlobStorageConstants.FolderNames.Products;
                    break;
                case EntityType.PRODUCTCATEGORY:
                    folderName = AzureBlobStorageConstants.FolderNames.ProductCategories;
                    break;
                case EntityType.BRAND:
                    folderName = AzureBlobStorageConstants.FolderNames.Brands;
                    break;
                case EntityType.SLIDERITEM:
                    folderName = AzureBlobStorageConstants.FolderNames.SliderItems;
                    break;
                case EntityType.SHIPPINGPROVIDER:
                    folderName = AzureBlobStorageConstants.FolderNames.ShippingProviders;
                    break;
                case EntityType.PICKUPSTORE:
                    folderName = AzureBlobStorageConstants.FolderNames.PickUpStores;
                    break;
                case EntityType.LOCALDELIVERYPROVIDER:
                    folderName = AzureBlobStorageConstants.FolderNames.LocalDeliveryProviders;
                    break;
                default:
                    break;
            }

            return folderName;
        }
    }
}
