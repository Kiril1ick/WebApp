using WebApp.DAL.Models;

namespace WebApp.BL.Profile
{
    public class ProfileBL : IProfileBL
    {
        public Task<IEnumerable<ProfileModel>> Get(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
