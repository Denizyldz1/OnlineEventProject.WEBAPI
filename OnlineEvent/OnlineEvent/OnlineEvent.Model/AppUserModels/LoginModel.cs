using System.ComponentModel.DataAnnotations;

namespace OnlineEvent.Model.AppUserModels
{
    public class LoginModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
