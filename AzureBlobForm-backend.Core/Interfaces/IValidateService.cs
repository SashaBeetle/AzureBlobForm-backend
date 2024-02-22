using Microsoft.AspNetCore.Http;

namespace AzureBlobForm_backend.Core.Interfaces
{
    public interface IValidateService
    {
        public Task<string> ValidateEmail(string email);
        public Task<string> ValidateFile(IFormFile blob);

    }
}
