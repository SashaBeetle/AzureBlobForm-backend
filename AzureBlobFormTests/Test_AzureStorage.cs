using AzureBlobForm_backend.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace AzureBlobFormTests
{
    [TestClass]
    public class Test_AzureStorage
    {
        private IConfiguration Configuration { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("Settings.json", optional: false, reloadOnChange: true)
                .Build();
        }
        [TestMethod]
        public async Task TestMethod_Successful_Result()
        {
            // Arrange

            var someValue = Configuration["BlobConnectionString"];
            var somevalue = Configuration["BlobContainerName"];

            var configBuilder = new ConfigurationBuilder();

            configBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "BlobConnectionString", $"{someValue}" },
                { "BlobContainerName",$"{somevalue}" }
            });

            IConfiguration configuration = configBuilder.Build();


            var azureStorage = new AzureStorage(configuration);

            var testFile = new Mock<IFormFile>();
            testFile.Setup(x => x.FileName).Returns("testfile.docx"); 
            testFile.Setup(x => x.ContentType).Returns("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            testFile.Setup(x => x.OpenReadStream()).Returns(new MemoryStream());
            var testEmail = "test@example.com";

            // Act
            var response = await azureStorage.UploadAsync(testFile.Object, testEmail);

            // Assert
            Assert.IsFalse(response.Error);
        }

        [TestMethod]
        public async Task TestMethod_UnSuccessful_Result()
        {
            // Arrange

            var someValue = Configuration["BlobConnectionString"];
            var somevalue = Configuration["BlobContainerName"];

            var configBuilder = new ConfigurationBuilder();

            configBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "BlobConnectionString", $"{someValue}" },
                { "BlobContainerName",$"{somevalue}" }
            });

            IConfiguration configuration = configBuilder.Build();


            var azureStorage = new AzureStorage(configuration);

            var testFile = new Mock<IFormFile>();
            testFile.Setup(x => x.FileName).Returns("testfile.txt");
            testFile.Setup(x => x.ContentType).Returns("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            testFile.Setup(x => x.OpenReadStream()).Returns(new MemoryStream());
            var testEmail = "test@example.com";

            // Act
            var response = await azureStorage.UploadAsync(testFile.Object, testEmail);

            // Assert
            Assert.IsTrue(response.Error);
        }
    }
}