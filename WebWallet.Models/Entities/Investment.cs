using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebWallet.Models.Contracts;
using WebWallet.Models.Enumerations;

namespace WebWallet.Models.Entities
{
    public class Investment : IEntity
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
        public InvestmentType Type { get; set; }

        [Required]
        [Range((double)decimal.MinValue, maximum: (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserId { get; set; }
    }
}