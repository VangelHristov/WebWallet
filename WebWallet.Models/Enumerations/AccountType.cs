using System.ComponentModel.DataAnnotations;

namespace WebWallet.Models.Enumerations
{
    public enum AccountType
    {
        [Display(Name = "Кредитна карта")]
        Credit,

        [Display(Name = "Разплащателна сметка")]
        Debit,

        [Display(Name = "Спестовна сметка")]
        Savings,

        [Display(Name = "В брой")]
        Cash
    }
}