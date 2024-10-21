using Microsoft.AspNetCore.Mvc;

namespace SynchronousIO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AsyncController(IUserProfileService _userProfileService) : ControllerBase
    {
        /// <summary>
        /// This is an asynchronous method that calls the Task based GetUserProfileAsync method.
        /// </summary>
        [HttpGet()]
        public async Task<IActionResult> GetUserProfileAsync()
        {
            return Ok(await _userProfileService.GetUserProfileAsync());
        }
    }
}
