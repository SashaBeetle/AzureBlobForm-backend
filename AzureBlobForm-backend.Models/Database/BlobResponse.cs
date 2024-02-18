using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobForm_backend.Models.Database
{
    public class BlobResponse : Dbitem
    {
        public string? Status { get; set; }
        public bool Error { get; set; }
        public Blob Blob { get; set; }
        public BlobResponse() {
            Blob = new Blob();
        }
    }
}
