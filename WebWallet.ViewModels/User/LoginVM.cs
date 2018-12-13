using System.ComponentModel.DataAnnotations;

namespace WebWallet.ViewModels.User
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Полето е задължително.")]
        [RegularExpression(
            @"^[a-zA-Z0-9.\-@#^]{8,20}$",
            ErrorMessage = "Потребителското име трябва да бъде между 8 и 20 символа и може да съдържа само букви и цифри, [.-@$#^]")]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [DataType(DataType.Password)]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Паролата трябва да е между 8 и 20 символа. Трябва да съдържа главни и малки букви, цифри и специални символи [ @$!%*?& ]."
        )]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Display(Name = "Запомне ме?")]
        public bool RememberMe { get; set; } = true;
    }
}