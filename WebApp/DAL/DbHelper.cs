using Dapper;
using Npgsql;

namespace WebApp.DAL
{
    public class DbHelper
    {
        public static string ConnectionString = "Host=localhost;Port=5432;Database=testdb;User ID=kirill;Password=kirill02";

        public static async Task<int> ExecuteScalarAsync(string sql, object model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(sql, model);
            }
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(sql, model);
            }
        }
    }
}
