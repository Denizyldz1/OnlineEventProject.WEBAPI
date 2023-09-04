using System.ComponentModel.DataAnnotations;

namespace OnlineEvent.Model.AppUserModels
{
    public class UserCreateModel
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        [EmailAddress(ErrorMessage ="Email formatı doğru görünmüyor")]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        [Required]
        public string UserType { get; set; } = null!;
        public string? InstitutionName { get; set; }
        public string? InstitutionWebSite { get; set; }
    }
}
