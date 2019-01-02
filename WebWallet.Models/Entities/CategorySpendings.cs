using System.Collections.Generic;

namespace WebWallet.Models.Entities
{
    public class CategorySpendings
    {
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
        public Dictionary<string, decimal> SubCategories { get; set; }
    }
}