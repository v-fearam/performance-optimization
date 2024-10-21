using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace SynchronousIO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SyncUploadController(BlobServiceClient _blobServiceClient) : ControllerBase
    {
        /// <summary>
        /// This is an asynchronous method that calls the Task based GetUserProfileAsync method.
        /// </summary>
        [HttpGet()]
        public IActionResult UploadFile()
        {
            var container = _blobServiceClient.GetBlobContainerClient("data");

            var blobClient = container.GetBlobClient("myblob.txt");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var stream = CreateFile.Get())
            {
                blobClient.Upload(stream, true);
            }
            return Ok();
        }
    }
}
