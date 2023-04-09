using WebApp.BL.Generel;
using WebApp.BL.Profile;
using WebApp.DAL;
using WebApp.DAL.Models;

namespace WebApp.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDbSessionBL dbSessionBL;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IProfileDAL profileDAL;

        public CurrentUser(IHttpContextAccessor httpContextAccessor, IDbSessionBL dbSessionBL, IWebCookie webCookie, IUserTokenDAL userTokenDAL, IProfileDAL profileDAL)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dbSessionBL = dbSessionBL;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
            this.profileDAL = profileDAL;
        }

        public async Task<int?> GetUserIdByToken()
        {
            string? tokenCookie = webCookie.Get(AuthConstants.RememberMeCookieName);
            if (tokenCookie == null)
                return null;
            Guid? token = Helpers.StringToGuidDef(tokenCookie ?? "");
            if (token == null)
                return null;
            int? userId = await userTokenDAL.Get((Guid)token);
            return userId;
        }

        public async Task<bool> IsLoggedIn()
        {
            bool isloggedIn = await dbSessionBL.IsLoggedIn();
            if (!isloggedIn)
            {
                int? userId = await GetUserIdByToken();
                if(userId != null)
                {
                    await dbSessionBL.SetUserId((int)userId);
                    isloggedIn = true;
                }
            }
            return isloggedIn;
        }

        public async Task<int?> GetCurrentUserId()
        {
            return await dbSessionBL.GetUserId();
        }

        public async Task<IEnumerable<ProfileModel>> GetProfiles() 
        {
            int? userId = await GetCurrentUserId();
            if (userId == null) throw new Exception("Пользователь не найден");
            return await profileDAL.Get((int)userId);
        }

    }
}
