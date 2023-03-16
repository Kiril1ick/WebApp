using System.ComponentModel.DataAnnotations;

namespace WebApp.DAL.Model
{
    public class UserModel
    {
        [Key]
        public int? UserID { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
		public string Salt { get; set; } = null!;
        public int Status { get; set; } = 0;
    }
}
