using WebApp.DAL.Models;

namespace WebApp.BL.Profile
{
    public interface IProfileBL
    {
        Task<IEnumerable<ProfileModel>> Get(int userId);
    }
}
