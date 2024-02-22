using Azure.Storage.Blobs;
using AzureBlobForm_backend.Core.Interfaces;
using AzureBlobForm_backend.Models.Database;
using AzureBlobForm_backend.Models.Repository;
using AzureBlobForm_backend.WEB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace AzureBlobForm_backend.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task UploadAsync_ValidData_ReturnsSuccess()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetValue<string>("BlobContainerName")).Returns("containerName");

            var mockValidateService = new Mock<IValidateService>();
            mockValidateService.Setup(x => x.ValidateFile(It.IsAny<IFormFile>())).ReturnsAsync("Ok"); // Assuming file is valid
            mockValidateService.Setup(x => x.ValidateEmail(It.IsAny<string>())).ReturnsAsync("Ne Ok"); // Assuming email is valid

            // Create an instance of AzureStorage
            var azureStorage = new AzureStorage(mockConfiguration.Object);

            // Call the method under test
            var testFile = new Mock<IFormFile>();
            testFile.Setup(x => x.OpenReadStream()).Returns(new MemoryStream());
            var testEmail = "test@example.com";
            var response = await azureStorage.UploadAsync(testFile.Object, testEmail);

            // Assert results
            // Since we are only testing validation, we expect the response to be successful
            Assert.False(response.Error);
            Assert.Equal($"File {testFile.Object.FileName} Uploadet Succesfully.", response.Status);
            Assert.Null(response.Blob);
        }
    }
}