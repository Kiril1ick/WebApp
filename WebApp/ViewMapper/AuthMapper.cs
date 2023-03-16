using WebApp.DAL.Model;
using WebApp.ViewModel;

namespace WebApp.ViewMapper
{
    public class AuthMapper
    {
        public static UserModel MapRegistrationViewModelToUserModel(RegistrationViewModel model)
        {
            return new UserModel()
            {
                Email = model.Email!,
                Password = model.Password!
            };
        }
    }
}
