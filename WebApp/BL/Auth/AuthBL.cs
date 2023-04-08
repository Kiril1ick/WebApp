using System.ComponentModel.DataAnnotations;
using WebApp.BL.Exceptions;
using WebApp.BL.Generel;
using WebApp.DAL;
using WebApp.DAL.Model;

namespace WebApp.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDAL authDAL;
        private readonly IEncrypt encrypt;
        private readonly IDbSessionBL dbSessionBL;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IWebCookie webCookie;
        public AuthBL(IAuthDAL authDAL, IEncrypt encrypt, IUserTokenDAL userTokenDAL, IDbSessionBL dbSessionBL,IWebCookie webCookie)
        {
            this.authDAL = authDAL;
            this.encrypt = encrypt;
            this.userTokenDAL = userTokenDAL;
            this.dbSessionBL = dbSessionBL;
            this.webCookie = webCookie;
        }

        public async Task<int> Authenticate(string Email, string Password, bool RememberMe)
        {
            var user = await authDAL.GetUser(Email); 
            if (user.UserID != null && user.Password == encrypt.HashPassword(Password, user.Salt))
            {
                await Login(user.UserID ?? 0);
                if (RememberMe)
                {
                    Guid tokenId = await userTokenDAL.Create(user.UserID ?? 0);
                    webCookie.AddSecure(AuthConstants.RememberMeCookieName, tokenId.ToString(), AuthConstants.RememberMeDays);
                }
                return user.UserID ?? 0;
            }
            throw new AuthorizationException();
        }

        public async Task<int> CreateUser(UserModel model)
        {
            model.Salt = Guid.NewGuid().ToString();
            model.Password = encrypt.HashPassword(model.Password, model.Salt);
            int id = await authDAL.CreateUser(model);
            await Login(id);
            return id;
        }

        public async Task Validate(string email)
        {
            var user = await authDAL.GetUser(email);
            if (user.UserID !=null)
            {
                throw new DuplicateEmailException();
            }
        }

        public async Task Register(UserModel user)
        {
            using (var scope = Helpers.CreateTransactionScope())
            {
                await dbSessionBL.LockSession();
                await Validate(user.Email);
                await CreateUser(user);
                scope.Complete();
            }
        }

        public async Task Login(int id)
        {
            await dbSessionBL.SetUserId(id);
        }

    }
}
