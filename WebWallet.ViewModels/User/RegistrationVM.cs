using System.ComponentModel.DataAnnotations;
using WebWallet.ViewModels.Constants;

namespace WebWallet.ViewModels.User
{
    public class RegistrationVM
    {
        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Потребителско име")]
        [RegularExpression(Pattern.Username, ErrorMessage = Message.UsernameError)]
        public string UserName { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [DataType(DataType.EmailAddress, ErrorMessage = Message.EmailError)]
        [Display(Name = "Имейл Адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Парола")]
        [DataType(DataType.Password)]
        [RegularExpression(Pattern.Password, ErrorMessage = Message.PasswordError)]
        public string Password { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Порвърди Паролата")]
        [Compare("Password", ErrorMessage = Message.ConfirmPasswordError)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string ActivationEmailSubjec => ConfirmationEmail.Subject;

        public string ActivationEmailBody => ConfirmationEmail.Body;
    }
}