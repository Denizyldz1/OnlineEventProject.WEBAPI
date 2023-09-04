using System.ComponentModel.DataAnnotations;

namespace OnlineEvent.Model.AppUserModels
{
    public class ChangeEmailModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [EmailAddress]
        [Compare("Email", ErrorMessage = "Email adresleri uyuşmuyor")]
        public string ConfirmEmail { get; set; } = null!;
    }
}
