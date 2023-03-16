using WebApp.DAL.Model;

namespace WebApp.DAL
{
    public interface IAuthDAL
    {
        Task<UserModel> GetUser(int id);
        Task<UserModel> GetUser(string email);
        Task<int> CreateUser(UserModel User);
    }
}
