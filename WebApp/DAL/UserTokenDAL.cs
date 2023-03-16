
using WebApp.DAL.Models;

namespace WebApp.DAL
{
    public class UserTokenDAL : IUserTokenDAL
    {
        
        public async Task<Guid> Create(int userId)
        {
            Guid tokenId = Guid.NewGuid();
            string sql = @"insert into UserToken(UserTokenId, UserId, Created)
                values (@tokenId, @userId, NOW())"
            ;

            await DbHelper.ExecuteScalarAsync(sql, new {userId=userId, tokenId = tokenId});
            return tokenId;
        }

        public async Task<int?> Get(Guid tokenId)
        {
            string sql = @"select userid from usertoken where usertokenid = @tokenId";
            return await DbHelper.ExecuteScalarAsync(sql, new { tokenId = tokenId });
        }
    }
}
