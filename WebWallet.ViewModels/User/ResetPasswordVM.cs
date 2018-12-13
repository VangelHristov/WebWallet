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
        [Display(Name = "Стара Парола")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
             ErrorMessage = "Паролата трябва да е между 8 и 20 символа. Трябва да съдържа главни и малки букви, цифри и специални символи [ @$!%*?& ]."
        )]
        [Display(Name = "Нова Парола")]
        public string NewPassword { get; set; }
    }
}