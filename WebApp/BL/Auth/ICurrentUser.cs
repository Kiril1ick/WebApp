namespace WebApp.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();
    }
}
