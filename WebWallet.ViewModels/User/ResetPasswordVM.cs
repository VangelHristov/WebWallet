using System.ComponentModel.DataAnnotations;

namespace WebWallet.ViewModels.User
{
    public class ResetPasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "The password must be between 8 and 20 symbols long. It must contain uppercase and lowercase letters, number and a special character[ @$!%*?& ]."
        )]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "The password must be between 8 and 20 symbols long. It must contain uppercase and lowercase letters, number and a special character[ @$!%*?& ]."
        )]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
    }
}