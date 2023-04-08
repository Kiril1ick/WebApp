using WebApp.DAL.Model;
using WebApp.DAL.Models;
using WebApp.ViewModel;

namespace WebApp.ViewMapper
{
    public static class ProfileMapper
    {
        public static ProfileModel MapProfileViewModelToProfileModel(ProfileViewModel model)
        {
            return new ProfileModel()
            {
                ProfileName = model.ProfileName,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
        }

        public static ProfileViewModel MapProfileModelToProfileViewModel(ProfileModel model)
        {
            return new ProfileViewModel()
            {
                ProfileId = model.ProfileID,
                ProfileName = model.ProfileName,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
        }
    }
}
