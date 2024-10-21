using Microsoft.AspNetCore.Mvc;
using SynchronousIO.Models;

namespace SynchronousIO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SyncController(IUserProfileService _userProfileService) : ControllerBase
    {
        /// <summary>
        /// This is a synchronous method that calls the synchronous GetUserProfile method.
        /// </summary>
        [HttpGet()]
        public UserProfile GetUserProfile()
        {
            return _userProfileService.GetUserProfile();
        }
    }
}
