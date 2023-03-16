using WebApp.DAL.Model;
using Npgsql;
using Dapper;

namespace WebApp.DAL
{
    public class AuthDAL : IAuthDAL
    {
        public async Task<int> CreateUser(UserModel model)
        {
            string query = @"insert into AppUser(Email, Password, Salt, Status)
                values(@Email,@Password, @Salt, @Status);
                select currval(pg_get_serial_sequence('AppUser','userid'));";
            var result = await DbHelper.QueryAsync<int>(query, model);
            return result.First();
        }

        public async Task<UserModel> GetUser(int id)
        {
            var result = await DbHelper.QueryAsync<UserModel>(
                    @"select UserID, Email, Password, Salt, Status " +
                    @"from AppUser " +
                    @"where UserID = @id", new {id = id});
            return result.FirstOrDefault() ?? new UserModel();
        }

        public async Task<UserModel> GetUser(string email)
        {
            var result = await DbHelper.QueryAsync<UserModel>(
                    @"select UserID, Email, Password, Salt, Status " +
                    @"from AppUser " +
                    @"where Email = @email", new { email = email });
            return result.FirstOrDefault() ?? new UserModel();
        }
    }
}
