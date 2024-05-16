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
                default:
                    break;
            }

            return folderName;
        }
    }
}
