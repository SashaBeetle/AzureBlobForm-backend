using AzureBlobForm_backend.Core.Interfaces;
using AzureBlobForm_backend.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobForm_backend.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IAzureSendService _azureService;
        public StorageController(IAzureSendService azureService)
        {
            _azureService = azureService;
        }
        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload(IFormFile blob, string email)
        {
            BlobResponse? response = await _azureService.UploadAsync(blob, email);
            

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
