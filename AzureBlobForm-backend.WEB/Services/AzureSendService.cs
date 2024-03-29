﻿using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlobForm_backend.Core.Interfaces;
using AzureBlobForm_backend.Models.Database;


namespace AzureBlobForm_backend.WEB.Services
{
    public class AzureSendService : IAzureSendService
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        private ValidateService _validateService = new ValidateService();


        public AzureSendService(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");

        }

        public async Task<BlobResponse> UploadAsync(IFormFile blob, string email)
        {
            BlobResponse response = new();

            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            string ValidationF = await _validateService.ValidateFile(blob);
            string ValidationE = await _validateService.ValidateEmail(email);

            if (ValidationF != null || ValidationE != null)
            {
                response.Status = "File: " + ValidationF + "\nEmail: " + ValidationE;
                response.Error = true;
                return response;
            }

            try
            {



                BlobClient client = container.GetBlobClient(email + "|" + blob.FileName + ".docx");

                await using (Stream? data = blob.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }

                Uri sasUrl = await CreateSASTokenService.GenerateSASToken(client);

                response.Status = $"File {blob.FileName} Uploadet Succesfully.";
                response.Error = false;
                response.Blob.Uri = sasUrl.AbsoluteUri;
                response.Blob.Name = client.Name;



            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                response.Status = $"File {blob.FileName} already exist. Try again.";
                response.Error = true;
                return response;
            }
            catch (RequestFailedException ex)
            {
                response.Status = $"Unexpected error: {ex.StackTrace}";
                response.Error = true;
                return response;
            }

            return response;
        }

    }
}
