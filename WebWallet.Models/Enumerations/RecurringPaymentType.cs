using System.ComponentModel.DataAnnotations;

namespace WebWallet.Models.Enumerations
{
    public enum RecurringPaymentType
    {
        [Display(Name = "Наем на жилище")]
        Housing,

        [Display(Name = "Битови сметки")]
        Utility,

        [Display(Name = "Заеми")]
        Loan,

        [Display(Name = "Застраховки")]
        Insurance,

        [Display(Name = "Издръжка за деца")]
        ChildSupport,

        [Display(Name = "Друго")]
        Other
    }
}