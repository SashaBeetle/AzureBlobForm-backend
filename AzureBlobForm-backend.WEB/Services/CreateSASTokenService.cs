using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using AzureBlobForm_backend.Core.Interfaces;

namespace AzureBlobForm_backend.WEB.Services
{
    public class CreateSASTokenService : ICreateSASTokenService
    {
        public Task<Uri> CreateSASToken(BlobClient blobClient, string storedPolicyName = null)
        {
            return GenerateSASToken(blobClient, storedPolicyName);
        }

        public static async Task<Uri> GenerateSASToken(BlobClient blobClient, string storedPolicyName = null)
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
    }
}
