using System;
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
        [Range(1, maximum: (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Limit { get; set; }

        [Required]
        public long Period { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserId { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public void SetStartAndEnd()
        {
            if (End.Year < 2018)
            {
                End = Start.AddTicks(Period);
            }

            if (Start >= End)
            {
                Start = End.AddDays(1);
                End = End.AddTicks(Period);
            }
        }

        [Required]
        [Range(1, maximum: (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Available { get; set; }
    }
}