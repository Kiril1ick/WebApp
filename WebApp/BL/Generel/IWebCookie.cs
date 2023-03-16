namespace WebApp.BL.Generel
{
    public interface IWebCookie
    {
        public void AddSecure(string cookieName, string value, int days = 0);
        public void Add(string cookieName, string value, int days = 0);
        public void Delete(string cookieName);
        public string? Get(string cookieName);
    }
}
