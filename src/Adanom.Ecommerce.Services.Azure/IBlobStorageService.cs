using Adanom.Ecommerce.Data.Models;

namespace Adanom.Ecommerce.Services.Azure
{
    public interface IBlobStorageService
    {
        #region UploadFileAsync

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="containerName"></param>
        /// <param name="existingFileName">File name to delete</param>
        /// <returns></returns>

        Task<bool> UploadFileAsync(UploadedFile file, string containerName, string? existingFileName = null);

        #endregion

        #region UpdateFileNameAsync

        Task<bool> UpdateFileNameAsync(string containerName, string oldName, string newName);

        #endregion

        #region DeleteFileAsync

        Task<bool> DeleteFileAsync(string containerName, string fileName);

        #endregion
    }
}
