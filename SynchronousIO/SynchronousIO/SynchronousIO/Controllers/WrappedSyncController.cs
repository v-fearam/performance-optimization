using Microsoft.AspNetCore.Mvc;
using SynchronousIO.Models;

namespace SynchronousIO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WrappedSyncController(IUserProfileService _userProfileService) : ControllerBase
    {
        /// <summary>
        /// This is an asynchronous method that calls the Task based GetUserProfileWrappedAsync method.
        /// Even though this method is async, the result is similar to the SyncController in that threads
        /// are tied up by the synchronous GetUserProfile method in the Task.Run. Under significant load
        /// new threads will need to be created.
        /// </summary>
        [HttpGet()]
        public async Task<UserProfile> GetUserProfileAsync()
        {
            return await _userProfileService.GetUserProfileWrappedAsync();
        }
    }
}
