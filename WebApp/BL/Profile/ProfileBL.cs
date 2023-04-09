using WebApp.DAL;
using WebApp.DAL.Models;

namespace WebApp.BL.Profile
{
    public class ProfileBL : IProfileBL
    {
        private readonly IProfileDAL profileDAL;
        public ProfileBL(IProfileDAL profileDAL) 
        {
            this.profileDAL = profileDAL;
        }
        public async Task<IEnumerable<ProfileModel>> Get(int userId)
        {
            return await profileDAL.Get(userId);
        }

        public async Task AddOrUpdate(ProfileModel model)
        {
            if(model.ProfileId == null)
            {
                await profileDAL.Add(model);
            }
            else
            {
                await profileDAL.Update(model);
            }
        }
    }
}
