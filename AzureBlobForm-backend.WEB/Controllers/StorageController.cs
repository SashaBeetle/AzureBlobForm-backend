using AzureBlobForm_backend.Core.Interfaces;
using AzureBlobForm_backend.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobForm_backend.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IAzureStorage _storage;
        public StorageController(IAzureStorage storage)
        {
            _storage = storage;
        }
        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload(IFormFile blob, string email)
        {
            BlobResponse? response = await _storage.UploadAsync(blob, email);

            

            if (response.Error == true) {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            } 
            else
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }
        
    }
}
