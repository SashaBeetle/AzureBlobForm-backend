using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlobForm_backend.Core.Interfaces;
using AzureBlobForm_backend.Models.Database;

namespace AzureBlobForm_backend.Models.Repository
{
    public class AzureStorage : IAzureStorage
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;

        public AzureStorage(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
        }


        public async Task<BlobResponse> UploadAsync(IFormFile blob)
        {
            BlobResponse response = new();

            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                BlobClient client = container.GetBlobClient(blob.FileName);

                await using (Stream? data = blob.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }

                response.Status = $"File {blob.Name} Uploadet Succesfully.";
                response.Error = false;
                response.Blob.Uri = client.Uri.AbsoluteUri;
                response.Blob.Name = client.Name;
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                response.Status = $"File {blob.Name} already exist. Try again.";
                response.Error = true;
                return response;
            }
            catch (RequestFailedException ex)
            {
                response.Status = $"Unexpected error: {ex.StackTrace}";
                response.Error = true;
                return response;
            }
            return response;
        }
    }
}
