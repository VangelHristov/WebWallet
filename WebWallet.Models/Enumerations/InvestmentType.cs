using System.ComponentModel.DataAnnotations;

namespace WebWallet.Models.Enumerations
{
    public enum InvestmentType
    {
        [Display(Name = "Акции и ценни книжа")]
        Stock,

        [Display(Name = "Валута")]
        Currency,

        [Display(Name = "Крипто валута")]
        Crypto
    }
}