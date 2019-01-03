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

        [Range((double)decimal.MinValue, maximum: (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal OverdueAmount { get; set; }

        [Range((double)decimal.MinValue, maximum: (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountRemaining { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserId { get; set; }

        public void SetDueDate()
        {
            if (DueDate >= DateTime.UtcNow)
            {
                if (AmountRemaining > 0)
                {
                    Overdue = true;
                    OverdueAmount += AmountRemaining;
                }

                DueDate = DateTime.UtcNow.AddTicks(Period);
            }
        }
    }
}