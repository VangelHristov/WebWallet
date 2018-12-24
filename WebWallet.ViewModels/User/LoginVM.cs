using System.ComponentModel.DataAnnotations;
using WebWallet.ViewModels.Constants;

namespace WebWallet.ViewModels.User
{
    public class LoginVM
    {
        [Required(ErrorMessage = Message.RequiredField)]
        [RegularExpression(Pattern.Username, ErrorMessage = Message.UsernameError)]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [DataType(DataType.Password)]
        [RegularExpression(Pattern.Password, ErrorMessage = Message.PasswordError)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Display(Name = "Запомне ме?")]
        public bool RememberMe { get; set; } = true;
    }
}