# [Azure Blob Form](https://blobform.azurewebsites.net)
### Project for `'Web Technology'` course in Lviv National University of Ivan Franko
Contacts:
* [Telegram](https://t.me/Sasha_Beetle) 
* Email: oleksandrzuk218@gmail.com
## Stack
* [.NET](https://dotnet.microsoft.com/) - free, open-source, cross-platform framework for building modern apps and powerful cloud services.
* [Azure](https://azure.microsoft.com/) - cloud computing platform and a set of services provided by Microsoft for building, deploying, and managing applications and services through Microsoft-managed data centers.
* [MSTest](https://learn.microsoft.com/uk-ua/dotnet/core/testing/unit-testing-with-mstest) - unit testing framework for .NET developers that allows them to write and execute automated tests to ensure the correctness and reliability of their code.
* [App Service](https://azure.microsoft.com/en-us/products/app-service/) - set of cloud-based services provided by Microsoft that enable developers and businesses to build, deploy, and manage applications and services in the cloud, including computing, storage, networking, databases, analytics, and more.
* [NuGet packages](https://learn.microsoft.com/uk-ua/nuget/) - type of software package used in the Microsoft .NET ecosystem, containing compiled code and other resources, and are used by developers to easily add functionality to their projects and share code between teams.
## How to run project
Open your system terminal and run commands:
```sh
git clone https://github.com/SashaBeetle/AzureBlobForm-backend.git
cd AzureBlobForm-backend
```
In `SashaBeetle/AzureBlobForm-backend.WEB/appsettings.json` in `"BlobConnectionString"` and `"BlobContainerName"` add your keys. Code shluld look like this:
```sh
  "BlobConnectionString": "YourBlobConnectionString",
  "BlobContainerName": "YourBlobContainerName"
```
## Decomposition of tasks
* ✅ API for sending a file to Azure
* ✅ Validation for Email and File(.docx)
* ✅ Generate a SAS token that will be valide only for 1 hour
* ✅ Add unit tests using MSTests
* ✅ Deploy a project to Azure
