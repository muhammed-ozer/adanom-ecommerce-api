﻿using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Services.Azure
{
    public interface IBlobStorageService
    {
        #region UploadFileAsync

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="existingFileName">File name to delete</param>
        /// <returns></returns>

        Task<bool> UploadFileAsync(UploadedFile file, string? existingFileName = null);

        #endregion

        #region UpdateFileNameAsync

        Task<bool> UpdateFileNameAsync(string oldName, string newName);

        #endregion

        #region DeleteFileAsync

        Task<bool> DeleteFileAsync(string fileName);

        #endregion
    }
}
