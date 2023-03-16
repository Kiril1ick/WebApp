using WebApp.DAL.Models;

namespace WebApp.BL
{
    public interface IDbSessionBL
    {
        Task<SessionModel> GetSession();

        Task<int> SetUserId(int userId);

        Task<int?> GetUserId();

        Task<bool> IsLoggedIn();
        Task LockSession();
    }
}
