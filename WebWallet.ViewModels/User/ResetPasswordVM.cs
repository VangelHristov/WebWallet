using System.ComponentModel.DataAnnotations;
using WebWallet.ViewModels.Constants;

namespace WebWallet.ViewModels.User
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = Message.RequiredField)]
        [DataType(DataType.Password)]
        [RegularExpression(Pattern.Password, ErrorMessage = Message.PasswordError)]
        [Display(Name = "Въведи Нова Парола")]
        public string Password { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = Message.ConfirmPasswordError)]
        [Display(Name = "Повтори Паролата")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}