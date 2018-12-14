using System.ComponentModel.DataAnnotations;

namespace WebWallet.ViewModels.User
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Полето е задължително.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
             ErrorMessage = "Паролата трябва да е между 8 и 20 символа. Трябва да съдържа главни и малки букви, цифри и специални символи [ @$!%*?& ]."
        )]
        [Display(Name = "Въведи Нова Парола")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
        [Display(Name = "Повтори Паролата")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}