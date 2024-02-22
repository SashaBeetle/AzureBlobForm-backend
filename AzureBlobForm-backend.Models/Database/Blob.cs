namespace AzureBlobForm_backend.Models.Database
{
    public class Blob : Dbitem
    {
        public string? Uri { get; set; }
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public Stream? Stream { get; set; }
    }
}
