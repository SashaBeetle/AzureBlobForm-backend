using System;
using System.IO;
using System.Net.Mail;
using System.Reflection.Metadata;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AzureBlob.Function
{
    public class Function1
    {
      //  private readonly ILogger _logger;

        //public Function1(ILoggerFactory loggerFactory)
        //{
        //    _logger = loggerFactory.CreateLogger<Function1>();
        //}

        [Function("Function1")]
        public static async Task Run(
            [BlobTrigger("upload-files/{name}", Connection = "")]
            string myBlob, 
            string name
            /*ILogger log*/)
        {
             //log.LogInformation($"C# Blob trigger function Processed blob\n Name: {name}");

            var apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("bot2112421234@gmail.com", "Bot"),
                Subject = "Subject of the Email",
                PlainTextContent = $"Blob Name: {name}",
                HtmlContent = $"<strong>Blob Name:</strong> {name}<br/><br/><strong>Blob Content:</strong> "
            };
            msg.AddTo(new EmailAddress("farmgames153234@gmail.com", "Farmgames153234 Name"));

            var response = await client.SendEmailAsync(msg);
            //log.LogInformation($"Email sent with status code: {response.StatusCode}");
        }
    }
}
