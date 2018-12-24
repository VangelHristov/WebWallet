using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebWallet.ViewModels.Constants;
using WebWallet.ViewModels.Transaction;

namespace WebWallet.ViewModels.Common
{
    public class ViewModelWithPeriod
    {
        private string _periodString = string.Empty;

        public string Id { get; set; }

        public string UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredField)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
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

        public IEnumerable<TransactionVM> Transactions { get; set; }
    }
}