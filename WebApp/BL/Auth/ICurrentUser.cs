using WebApp.DAL.Models;

namespace WebApp.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();
        Task<int?> GetCurrentUserId();
        Task<IEnumerable<ProfileModel>> GetProfiles();
    }
}
