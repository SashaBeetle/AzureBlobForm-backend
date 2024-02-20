using System;
using System.IO;
using System.Net.Mail;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using static Google.Protobuf.WireFormat;

namespace AzureSendMail
{
    [StorageAccount("ConnectionString")]
    public class SendEmail
    {
        private readonly ILogger _logger;
        private readonly SendGridClient _sendGridClient;
        private readonly string _sendGridApiKey;

        public SendEmail(ILoggerFactory loggerFactory, string sendGridApiKey)
        {
            _logger = loggerFactory.CreateLogger<SendEmail>();
            _sendGridApiKey = sendGridApiKey;
            _sendGridClient = new SendGridClient(_sendGridApiKey);
        }

        [Function("SendEmail")]
        public async Task Run([BlobTrigger("send-email/{name}")] Stream myBlob, string name)
        {
            _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name}");

            // Send email notification
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("sender@example.com", "Sender Name"));
            msg.AddTo(new EmailAddress("recipient@example.com", "Recipient Name"));
            msg.SetSubject("File Uploaded Successfully");
            msg.AddContent(MimeType.Text, $"File '{name}' has been uploaded successfully.");

            try
            {
                var response = await _sendGridClient.SendEmailAsync(msg);
                if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    _logger.LogError($"Failed to send email: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email: {ex.Message}");
            }
        }
    }
}
