using WebApp.DAL.Models;

namespace WebApp.DAL
{
    public interface IProfileDAL
    {
        Task<IEnumerable<ProfileModel>>Get(int userId);
        Task<int> Add(ProfileModel profileModel);
        Task Update(ProfileModel profileModel);

    }
}
