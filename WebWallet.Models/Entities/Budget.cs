using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public TimeSpan Period { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }

        [Required]
        [Range((double)decimal.MinValue, maximum: (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Available { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserId { get; set; }
    }
}