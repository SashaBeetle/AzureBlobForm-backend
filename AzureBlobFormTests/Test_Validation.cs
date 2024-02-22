using AzureBlobForm_backend.WEB.Services;
using Microsoft.AspNetCore.Http;
using Moq;

namespace AzureBlobFormTests
{
    [TestClass]
    public class Test_Validation
    {
        [TestMethod]
        public async Task TestMethod_Missing_File()
        {
            // Arrange
            var validateService = new ValidateService();
            IFormFile testFile = null;

            // Act
            string errorMessage = await validateService.ValidateFile(testFile);

            // Assert
            Assert.AreEqual("No file uploaded.", errorMessage);
        }

        [TestMethod]
        public async Task TestMethod_Correct_File()
        {
            // Arrange
            var validateService = new ValidateService();

            var testFile = new Mock<IFormFile>();
            testFile.Setup(x => x.FileName).Returns("testfile.docx");
            testFile.Setup(x => x.ContentType).Returns("application/vnd.openxmlformats-officedocument.wordprocessingml.document");

            // Act
            string errorMessage = await validateService.ValidateFile(testFile.Object);

            // Assert
            Assert.IsNull(errorMessage);
        }

        [TestMethod]
        public async Task TestMethod_UnCorrect_File_Type()
        {
            // Arrange
            var validateService = new ValidateService();

            var testFile = new Mock<IFormFile>();
            testFile.Setup(x => x.FileName).Returns("testfile.txt");

            // Act
            string errorMessage = await validateService.ValidateFile(testFile.Object);

            // Assert
            Assert.AreEqual("Invalid file format. Only .docx files are allowed.", errorMessage);
        }

        [TestMethod]
        public async Task TestMethod_Missing_Email()
        {
            // Arrange
            var validateService = new ValidateService();
            string email = null;

            // Act
            string errorMessage = await validateService.ValidateEmail(email);

            // Assert
            Assert.AreEqual("Email address is required.", errorMessage);
        }

        [TestMethod]
        public async Task TestMethod_UnCorrect_Email()
        {
            // Arrange
            var validateService = new ValidateService();
            string email = "badexample.com";

            // Act
            string errorMessage = await validateService.ValidateEmail(email);

            // Assert
            Assert.AreEqual("Invalid email format. Please provide a valid email address.", errorMessage);
        }

        [TestMethod]
        public async Task TestMethod_Correct_Email()
        {
            // Arrange
            var validateService = new ValidateService();
            string email = "example@gmail.com";

            // Act
            string errorMessage = await validateService.ValidateEmail(email);

            // Assert
            Assert.IsNull(errorMessage);
        }

    }
}
