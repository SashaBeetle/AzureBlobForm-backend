using System.Text.RegularExpressions;

namespace AzureBlobForm_backend.WEB.Services
{
    public class ValidateService
    {
        public async Task<string> ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return "Email address is required.";
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(email, emailPattern))
            {
                return "Invalid email format. Please provide a valid email address.";
            }

            return null;
        }
        public async Task<string> ValidateFile(IFormFile blob)
        {
            if (blob == null)
            {
                return "No file uploaded.";
            }

            var allowedExtensions = new[] { ".docx" };
            var extension = Path.GetExtension(blob.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                return "Invalid file format. Only .docx files are allowed.";
            }

            if (!blob.ContentType.Contains("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
            {
                return "Invalid file content type. Expected application/vnd.openxmlformats-officedocument.wordprocessingml.document.";
            }

            return null;
        }
    }
}
