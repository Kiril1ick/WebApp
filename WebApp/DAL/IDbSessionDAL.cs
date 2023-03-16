using WebApp.DAL.Models;

namespace WebApp.DAL
{
    public interface IDbSessionDAL
    {
        Task<int> CreateSession(SessionModel model);
        Task<int> UpdateSession(SessionModel model);
        Task<SessionModel?> GetSession(Guid sessionID);
        Task LockSession(Guid sessionID);
    }
}
