using System.Reflection;
using WebApp.DAL.Model;
using WebApp.DAL.Models;

namespace WebApp.DAL
{
    public class ProfileDAL : IProfileDAL
    {
        public async Task<int> Add(ProfileModel profileModel)
        {
            string query = @"insert into AppUser(UserId,ProfileName,FirstName,LastName,ProfileImage)
                values(@UserId,@ProfileName,@FirstName,@LastName,@ProfileImage);
                returning ProfileID;";
            var result = await DbHelper.QueryAsync<int>(query, profileModel);
            return result.First();
        }

        public async Task<IEnumerable<ProfileModel>> Get(int userId)
        {
            return await DbHelper.QueryAsync<ProfileModel>(
                    @"select ProfileID,UserId,ProfileName,FirstName,LastName,ProfileImage" +
                    @"from Profile " +
                    @"where UserId = @id", new { id = userId });
        }

        public async Task Update(ProfileModel profileModel)
        {
            string query = @"update Profile
                set ProfileName = @ProfileName,
                    FirstName = @FirstName,
                    LastName = @LastName,
                    ProfileImage = @ProfileImage
                where ProfileID = @ProfileID";
            var result = await DbHelper.QueryAsync<int>(query, profileModel);
        }
    }
}
