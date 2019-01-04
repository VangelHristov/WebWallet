using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebWallet.Models.Contracts;

namespace WebWallet.Models.Entities
{
    public class MonthlyReport : IEntity
    {
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal EndBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalIncome { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalInvested { get; set; }

        [NotMapped]
        public Dictionary<string, decimal> InvestmentsPerType { get; set; }

        public string InvestmentsPerTypeJson
        {
            get
            {
                return JsonConvert.SerializeObject(InvestmentsPerType);
            }
            set
            {
                InvestmentsPerType = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(value);
            }
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSpendings { get; set; }

        [NotMapped]
        public IList<CategorySpendings> SpendingsPerCategory { get; set; }

        public string SpendingsPerCategoryJson
        {
            get
            {
                return JsonConvert.SerializeObject(SpendingsPerCategory);
            }
            set
            {
                SpendingsPerCategory = JsonConvert
                    .DeserializeObject<IList<CategorySpendings>>(value);
            }
        }

        [NotMapped]
        public Dictionary<string, HashSet<string>> Abbreviations { get; set; }

        public string AbbreviationsJson
        {
            get
            {
                return JsonConvert.SerializeObject(Abbreviations);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Abbreviations = JsonConvert
                    .DeserializeObject<Dictionary<string, HashSet<string>>>(value);
                }
            }
        }
    }
}