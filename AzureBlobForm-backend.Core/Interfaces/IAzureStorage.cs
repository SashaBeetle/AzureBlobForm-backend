using AzureBlobForm_backend.Models.Database;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobForm_backend.Core.Interfaces
{
    public  interface IAzureStorage
    {
       Task<BlobResponse>  UploadAsync(IFormFile file);
    }
}
