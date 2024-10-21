using SynchronousIO.Models;

namespace SynchronousIO
{
    public interface IUserProfileService
    {
        UserProfile GetUserProfile();
        Task<UserProfile> GetUserProfileAsync();
        Task<UserProfile> GetUserProfileWrappedAsync();
    }
}
