using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WebWallet.Models.Contracts;

namespace WebWallet.Models.Entities
{
    public class Budget : IEntity
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
        [Range((double)decimal.MinValue, maximum: (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Limit { get; set; }

        [Required]
        public long Period { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserId { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public void SetStartAndEnd()
        {
            if (End == new DateTime())
            {
                End = Start.AddTicks(Period);
            }

            if (Start >= End)
            {
                Start = End.AddDays(1);
                End = End.AddTicks(Period);
            }
        }

        public decimal GetAvailable()
        {
            return Limit - (Transactions?
                .Where(x => x.CreatedOn >= End)
                .Sum(x => x.Amount) ?? 0);
        }
    }
}