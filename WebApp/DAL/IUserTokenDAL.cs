using WebApp.DAL.Models;

namespace WebApp.DAL
{
    public interface IUserTokenDAL
    {
        Task<Guid> Create(int userId);
        Task<int?> Get(Guid tokenId);
    }
}
