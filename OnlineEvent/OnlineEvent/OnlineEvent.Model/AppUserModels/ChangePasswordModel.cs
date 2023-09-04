using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEvent.Model.AppUserModels
{
    public class ChangePasswordModel
    {
        [Required]

        public string OldPassword { get; set; } = null!;

        [Required]
        public string NewPassword { get; set; } = null!;
        [Required]
        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmNewPassword { get; set; } = null!;

    }
}
