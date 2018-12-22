using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WebWallet.Models.Contracts;
using WebWallet.Models.Enumerations;

namespace WebWallet.Models.Entities
{
    public class RecurringPayment : IEntity
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Required]
        public RecurringPaymentType RecurringPaymentType { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public long Period { get; set; }

        [Range((double)decimal.MinValue, maximum: (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public bool Overdue { get; set; }

        public decimal OverdueAmount { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserId { get; set; }

        public void SetDueDate()
        {
            if (DueDate >= DateTime.UtcNow)
            {
                var startDate = new DateTime(DueDate.Ticks - Period);

                var totalPayedForPeriod = Transactions
                    .Where(x => x.CreatedOn >= startDate)
                    .Sum(x => x.Amount);
                if (totalPayedForPeriod < Amount)
                {
                    Overdue = true;
                    OverdueAmount += Amount - totalPayedForPeriod;
                }

                DueDate = DateTime.UtcNow.AddTicks(Period);
            }
        }
    }
}