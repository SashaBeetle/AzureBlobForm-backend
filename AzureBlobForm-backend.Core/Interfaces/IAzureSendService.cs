using AzureBlobForm_backend.Models.Database;
using Microsoft.AspNetCore.Http;

namespace AzureBlobForm_backend.Core.Interfaces
{
    public interface IAzureSendService
    {
       Task<BlobResponse>  UploadAsync(IFormFile file, string email);
    }
}
