using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
namespace SynchronousIO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AsyncUploadController(BlobServiceClient _blobServiceClient) : ControllerBase
    {
        /// <summary>
        /// This is an asynchronous method that calls the Task based GetUserProfileAsync method.
        /// </summary>
        [HttpGet()]
        public async Task<IActionResult> UploadFileAsync()
        {
            var container = _blobServiceClient.GetBlobContainerClient("data");

            var blobClient = container.GetBlobClient("myblob.txt");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var stream = CreateFile.Get())
            {
                await blobClient.UploadAsync(stream, true);
            }
            return Ok();
        }
    }
}
