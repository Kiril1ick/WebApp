using System.ComponentModel.DataAnnotations;
using WebApp.DAL.Model;

namespace WebApp.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel model);
        Task<int> Authenticate(string Email, string Password, bool RememberMe);
        Task Validate(string email);
        Task Register(UserModel user);
    }
}
