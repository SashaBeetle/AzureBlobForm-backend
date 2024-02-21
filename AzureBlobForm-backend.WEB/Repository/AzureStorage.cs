using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using AzureBlobForm_backend.Core.Interfaces;
using AzureBlobForm_backend.Models.Database;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

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


        public async Task<BlobResponse> UploadAsync(IFormFile blob, string email)
        {
            BlobResponse response = new();

            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
           
            string Validation = await ValidateFile(blob);

            if(Validation != null)
            {
                response.Status = Validation;
                response.Error = true;
                return response;
            }

            try
            {
                
               

                BlobClient client = container.GetBlobClient(email + "|" + blob.FileName + ".docx");

                await using (Stream? data = blob.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }

                Uri sasUrl = await CreateServiceSASBlob(client);

                response.Status = $"File {blob.Name} Uploadet Succesfully.";
                response.Error = false;
                response.Blob.Uri = sasUrl.AbsoluteUri;
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

        public static async Task<Uri> CreateServiceSASBlob(BlobClient blobClient, string storedPolicyName = null)
        {
            if (blobClient.CanGenerateSasUri)
            {              
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                    BlobName = blobClient.Name,
                    Resource = "b",
                    StartsOn = DateTimeOffset.UtcNow
                };

                if (storedPolicyName == null)
                {
                    sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
                    sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);
                }
                else
                {
                    sasBuilder.Identifier = storedPolicyName;
                }

                Uri sasURI = blobClient.GenerateSasUri(sasBuilder);

                return sasURI;
            }
            else
            {
                return null;
            }
        }
        public async Task<string> ValidateFile(IFormFile blob)
        {
            if (blob == null)
            {
                return "No file uploaded.";
            }

            var allowedExtensions = new[] { ".docx" };
            var extension = Path.GetExtension(blob.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                return "Invalid file format. Only .docx files are allowed.";
            }

            if (!blob.ContentType.Contains("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
            {
                return "Invalid file content type. Expected application/vnd.openxmlformats-officedocument.wordprocessingml.document.";
            }

            return null;
        }
    }
}
