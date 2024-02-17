using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobForm_backend.Models.Database
{
    public class Request : Dbitem
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
