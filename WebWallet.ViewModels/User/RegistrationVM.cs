using System.ComponentModel.DataAnnotations;

namespace WebWallet.ViewModels.User
{
    public class RegistrationVM
    {
        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Потребителско име")]
        [RegularExpression(
            @"^[a-zA-Z0-9.\-@#^]{8,20}$",
            ErrorMessage = "Потребителското име трябва да бъде между 8 и 20 символа и може да съдържа само букви и цифри, [.-@$#^]")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Невалиден имейл адрес.")]
        [Display(Name = "Имейл Адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Парола")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
             ErrorMessage = "Паролата трябва да е между 8 и 20 символа. Трябва да съдържа главни и малки букви, цифри и специални символи [ @$!%*?& ]."
        )]
        public string Password { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Порвърди Паролата")]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}