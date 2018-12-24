using System;
using System.ComponentModel.DataAnnotations;
using WebWallet.ViewModels.Common;
using WebWallet.ViewModels.Constants;

namespace WebWallet.ViewModels.Budget
{
    public class BudgetVM : ViewModelWithPeriod
    {
        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Лимит")]
        [Range(1.0, maximum: (double)decimal.MaxValue, ErrorMessage = Message.InvalidValue)]
        [DisplayFormat(DataFormatString = FormatStrig.BGN, ApplyFormatInEditMode = false)]
        public decimal Limit { get; set; }

        [Display(Name = "Остават")]
        [DisplayFormat(DataFormatString = FormatStrig.BGN, ApplyFormatInEditMode = false)]
        public decimal Available { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}