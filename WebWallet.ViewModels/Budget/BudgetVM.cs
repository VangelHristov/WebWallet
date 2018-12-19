using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebWallet.ViewModels.Transaction;

namespace WebWallet.ViewModels.Budget
{
    public class BudgetVM
    {
        private string _periodString = string.Empty;

        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Полетро е задължително.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Лимит")]
        [Range(
            (double)decimal.MinValue,
            maximum: (double)decimal.MaxValue,
            ErrorMessage = "Невалидна стойност."
        )]
        public decimal Limit { get; set; }

        public List<string> Periods = new List<string>
        {
            "7 дни",
            "14 дни",
            "1 месец",
            "3 месеца",
            "6 месеца",
            "1 година"
        };

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Период")]
        public string PeriodString
        {
            get
            {
                switch (this.Period.Days)
                {
                    case 7: this._periodString = "7 дни"; break;
                    case 14: this._periodString = "14 дни"; break;
                    case 30: this._periodString = "1 месец"; break;
                    case 90: this._periodString = "3 месеца"; break;
                    case 180: this._periodString = "6 месеца"; break;
                    case 365: this._periodString = "1 година"; break;
                    default: this._periodString = "1 месец"; break;
                }
                return this._periodString;
            }
            set
            {
                this._periodString = value;

                switch (value)
                {
                    case "7 дни": this.Period = TimeSpan.FromDays(7); break;
                    case "14 дни": this.Period = TimeSpan.FromDays(14); break;
                    case "1 месец": this.Period = TimeSpan.FromDays(30); break;
                    case "3 месеца": this.Period = TimeSpan.FromDays(90); break;
                    case "6 месеца": this.Period = TimeSpan.FromDays(180); break;
                    case "1 година": this.Period = TimeSpan.FromDays(365); break;
                    default:
                        this.Period = TimeSpan.FromDays(30);
                        this._periodString = "1 месец";
                        break;
                }
            }
        }

        public TimeSpan Period { get; set; }

        [Display(Name = "Остават")]
        public decimal Available { get; set; }

        public string UserId { get; set; }

        public IEnumerable<TransactionVM> Transactions { get; set; }
    }
}