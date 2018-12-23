using System;
using System.ComponentModel.DataAnnotations;
using WebWallet.ViewModels.Common;

namespace WebWallet.ViewModels.Budget
{
    public class BudgetVM : ViewModelWithPeriod
    {
        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Лимит")]
        [Range(1.0, maximum: (double)decimal.MaxValue, ErrorMessage = "Невалидна стойност.")]
        [DisplayFormat(DataFormatString = "{0:0.## лв.}", ApplyFormatInEditMode = false)]
        public decimal Limit { get; set; }

        [Display(Name = "Остават")]
        [DisplayFormat(DataFormatString = "{0:0.## лв.}", ApplyFormatInEditMode = false)]
        public decimal Available { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}