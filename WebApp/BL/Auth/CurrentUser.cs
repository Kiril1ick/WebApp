using WebApp.BL.Generel;
using WebApp.DAL;

namespace WebApp.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDbSessionBL dbSessionBL;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;
        public CurrentUser(IHttpContextAccessor httpContextAccessor, IDbSessionBL dbSessionBL, IWebCookie webCookie, IUserTokenDAL userTokenDAL)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dbSessionBL = dbSessionBL;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
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

        public async Task<int?> GetCurrentUser()
        {
            return await dbSessionBL.GetUserId();
        }
    }
}
