using System.ComponentModel.DataAnnotations;

namespace WebWallet.ViewModels.User
{
    public class LoginVM
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9.\-@#^]{8,20}$")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "The password must be between 8 and 20 symbols long. It must contain uppercase and lowercase letters, number and a special character[ @$!%*?& ]."
        )]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; } = true;
    }
}