using Dapper;
using Npgsql;
using WebApp.DAL.Models;

namespace WebApp.DAL
{
    public class DbSessionDAL : IDbSessionDAL
    {
        public async Task<int> CreateSession(SessionModel model)
        {
            string sql = @"insert into DbSession(DbSessionID, SessionData, Created, LastAccessed, UserID)
                values (@DbSessionID, @SessionContent, @Created, @LastAccessed, @UserID)";

            return await DbHelper.ExecuteScalarAsync(sql, model);
        }
        public async Task<SessionModel?> GetSession(Guid sessionID)
        {
            string sql = @"select * from DbSession where DbSessionID = @sessionID";
            var session = await DbHelper.QueryAsync<SessionModel>(sql, new { sessionID = sessionID });
            return session.FirstOrDefault();
        }

        public async Task LockSession(Guid sessionID)
        {
            string sql = @"select DbSessionID from DbSession where DbSessionID = @sessionID for update";
            var session = await DbHelper.QueryAsync<SessionModel>(sql, new { sessionID = sessionID });
        }

        public async Task<int> UpdateSession(SessionModel model)
        {
            string sql = @"update DbSession
                      set SessionData = @SessionData, LastAccessed = @LastAccessed, UserId = @UserId
                      where DbSessionID = @DbSessionID
                ";
            return await DbHelper.ExecuteScalarAsync(sql, model);
        }
    }
}
