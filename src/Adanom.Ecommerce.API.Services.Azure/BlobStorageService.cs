using Adanom.Ecommerce.API.Data.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Serilog;

namespace Adanom.Ecommerce.API.Services.Azure
{
    internal class BlobStorageService : IBlobStorageService
    {
        #region Field

        private readonly BlobServiceClient _blobServiceClient;

        #endregion

        #region Ctor

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient ?? throw new ArgumentNullException(nameof(blobServiceClient));
        }

        #endregion

        #region IBlobStorageService Members

        #region UploadFileAsync

        public async Task<bool> UploadFileAsync(UploadedFile file, string containerName, string? existingFileName = null)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var isContainerExists = await blobContainerClient.ExistsAsync();

                if (!isContainerExists)
                {
                    await _blobServiceClient.CreateBlobContainerAsync(containerName);

                    await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
                }

                if (!string.IsNullOrEmpty(existingFileName))
                {
                    await blobContainerClient.DeleteBlobIfExistsAsync(existingFileName);
                }

                using var stream = new MemoryStream(file.Content);

                var contentType = file.Extension switch
                {
                    FileConstants.PDF.Extension =>
                        FileConstants.PDF.ContentType,
                    FileConstants.JPG.Extension =>
                        FileConstants.JPG.ContentType,
                    FileConstants.JPEG.Extension =>
                        FileConstants.JPEG.ContentType,
                    _ =>
                        FileConstants.PNG.ContentType
                };

                var blobClient = blobContainerClient.GetBlobClient(file.Name);

                var blobHttpHeader = new BlobHttpHeaders { ContentType = contentType };
                await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeader });

                // var uploadBlob = await blobContainerClient.UploadBlobAsync(file.Name, stream);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Azure:BlobStorage: {ex.Message}");

                return false;
            }
        }

        #endregion

        #region UpdateFileNameAsync

        public async Task<bool> UpdateFileNameAsync(string containerName, string oldName, string newName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var isContainerExists = await blobContainerClient.ExistsAsync();

                if (!isContainerExists)
                {
                    return true;
                }

                var oldBlob = blobContainerClient.GetBlobClient(oldName);

                var isOldBlobExists = await oldBlob.ExistsAsync();

                if (!isOldBlobExists)
                {
                    return false;
                }

                var lease = oldBlob.GetBlobLeaseClient();
                await lease.AcquireAsync(TimeSpan.FromSeconds(-1));

                BlobProperties oldBlobProperties = await oldBlob.GetPropertiesAsync();

                var newBlob = blobContainerClient.GetBlobClient(newName);

                await newBlob.StartCopyFromUriAsync(oldBlob.Uri);

                oldBlobProperties = await oldBlob.GetPropertiesAsync();

                if (oldBlobProperties.LeaseState == LeaseState.Leased)
                {
                    await lease.BreakAsync();

                    oldBlobProperties = await oldBlob.GetPropertiesAsync();
                }

                await blobContainerClient.DeleteBlobIfExistsAsync(oldBlob.Name);

                return true;
            }
            catch (Exception ex) 
            {
                Log.Error($"Azure:BlobStorage: {ex.Message}");

                return false;
            }
        }

        #endregion

        #region DeleteFileAsync

        public async Task<bool> DeleteFileAsync(string containerName, string fileName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var isContainerExists = await blobContainerClient.ExistsAsync();

                if (!isContainerExists)
                {
                    return true;
                }

                await blobContainerClient.DeleteBlobIfExistsAsync(fileName);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Azure:BlobStorage: {ex.Message}");

                return false;
            }
        }

        #endregion

        #endregion
    }
}