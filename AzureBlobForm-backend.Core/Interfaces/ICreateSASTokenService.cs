using Azure.Storage.Blobs;

namespace AzureBlobForm_backend.Core.Interfaces
{
    public interface ICreateSASTokenService
    {
        Task<Uri> CreateSASToken(BlobClient blobClient, string storedPolicyName = null);
    }
}
