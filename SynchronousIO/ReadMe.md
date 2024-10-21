# SynchronousIO Sample Code

It is a sample implementation to ilustrate [Synchronous I/O antipattern](https://learn.microsoft.com/azure/architecture/antipatterns/synchronous-io/)

The SynchronousIO sample code illustrates techniques for retrieving information from a web service and returning it to a client. The sample comprises the following items:

- SynchronousIO Web Api Application
-

The sample simulates fetching information from a data store. The data returned is a `UserProfile` object (defined in the Models folder in the project):

**C#**

```C#
public class UserProfile
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```

The code that actually retrieves the data is located in the `FakeUserProfileService` class, located in the project. This class exposes the following three methods:
**_C#_**

```C#
public class FakeUserProfileService : IUserProfileService
{
    public UserProfile GetUserProfile()
    {
        ...
    }

    public async Task<UserProfile> GetUserProfileAsync()
    {
        ...
    }

    public Task<UserProfile> GetUserProfileWrappedAsync()
    {
        ...
    }
}
```

These methods demonstrate the synchronous, task-based asynchronous, and wrapped async techniques for fetching data. The methods return hard-coded values, but simulate the delay expected when retrieving information from a remote data store.

The Web API project contains five controllers:

- `AsyncController`

- `AsyncUploadController`

- `SyncController`

- `SyncUploadController`

- `WrappedSyncController`

The `AsyncController`, `SyncController`, and `WrappedSyncController` controllers call the corresponding methods of the `FakeUserProfileService` class.

The `AsyncUploadController` and `SyncUploadController` controllers call corresponding methods in the Azure Blob storage sdk to upload the "FileToUpload.txt" file to Blob storage.

The CreateFile class is used to generate a file content that is 10 MB in size.

## Prerequisites

- Permission to create a new resource group and resources in an [Azure subscription](https://azure.com/free)
- Unix-like shell. Also available in:
  - [Azure Cloud Shell](https://shell.azure.com/)
  - [Windows Subsystem for Linux (WSL)](https://learn.microsoft.com/windows/wsl/install)
- [Git](https://git-scm.com/downloads)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli)
- Optionally, an IDE, like [Visual Studio](https://visualstudio.microsoft.com/downloads/) or [Visual Studio Code](https://code.visualstudio.com/).

## Steps

1. Clone this repository to your workstation and navigate to the working directory.

   ```bash
   git clone https://github.com/mspnp/performance-optimization
   cd SynchronousIO
   ```

1. Log into Azure and create an empty resource group.

   ```bash
   az login
   az account set -s <Name or ID of subscription>

   export USER_OBJECTID=<Microsoft Entra Id user's object id>
   
   LOCATION=eastus
   RESOURCEGROUP=rg-synchronous-IO-${LOCATION}

   az group create --name ${RESOURCEGROUP} --location ${LOCATION}

   ```

1. Deploy the supporting Azure resources.  
   It will create a storage account that only allows Managed Identity access. The User Object Id will have the Role to upload Blobs.

   ```bash
   az deployment group create --resource-group ${RESOURCEGROUP}  \
                        -f ./bicep/main.bicep  \
                        -p userObjectId=${USER_OBJECTID}
   ```

1. Configure database connection string

   On appsettings.json you need to complete with your azure account name.

   ```bash
   "https://<storageAccountName>.blob.core.windows.net/",
   ```

1. Authenticate with a Microsoft Entra identity

    As far the implementation is using manage identity, you need to assign the role to your [developer identity](https://learn.microsoft.com/azure/azure-functions/functions-reference?tabs=blob&pivots=programming-language-csharp#local-development-with-identity-based-connections).

1. Run proyect locally

   Execute the API and then you will be able to call both endpoints

## :broom: Clean up resources

Most of the Azure resources deployed in the prior steps will incur ongoing charges unless removed.

```bash
az group delete -n ${RESOURCEGROUP} -y
```

## Contributions

Please see our [Contributor guide](./CONTRIBUTING.md).

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact <opencode@microsoft.com> with any additional questions or comments.

With :heart: from Azure Patterns & Practices, [Azure Architecture Center](https://azure.com/architecture).
