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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Полето е задължително.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Лимит")]
        [Range(1.0, maximum: (double)decimal.MaxValue, ErrorMessage = "Невалидна стойност.")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal Limit { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Период")]
        public string PeriodString
        {
            get
            {
                switch (TimeSpan.FromTicks(Period).Days)
                {
                    case 7: _periodString = "7 дни"; break;
                    case 14: _periodString = "14 дни"; break;
                    case 30: _periodString = "1 месец"; break;
                    case 90: _periodString = "3 месеца"; break;
                    case 180: _periodString = "6 месеца"; break;
                    case 365: _periodString = "1 година"; break;
                    default: _periodString = "1 месец"; break;
                }
                return this._periodString;
            }
            set
            {
                this._periodString = value;

                switch (value)
                {
                    case "7 дни": this.Period = TimeSpan.FromDays(7).Ticks; break;
                    case "14 дни": this.Period = TimeSpan.FromDays(14).Ticks; break;
                    case "1 месец": this.Period = TimeSpan.FromDays(30).Ticks; break;
                    case "3 месеца": this.Period = TimeSpan.FromDays(90).Ticks; break;
                    case "6 месеца": this.Period = TimeSpan.FromDays(180).Ticks; break;
                    case "1 година": this.Period = TimeSpan.FromDays(365).Ticks; break;
                    default:
                        this.Period = TimeSpan.FromDays(30).Ticks;
                        this._periodString = "1 месец";
                        break;
                }
            }
        }

        public long Period { get; set; }

        [Display(Name = "Остават")]
        public decimal Available { get; set; }

        public string UserId { get; set; }

        public IEnumerable<TransactionVM> Transactions { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}