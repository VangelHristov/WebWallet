using System;
using System.Collections.Generic;

namespace WebWallet.ViewModels.MonthlyReport
{
    public class MonthlyReportVM
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public decimal EndBalance { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalSpendings { get; set; }
        public Dictionary<string, decimal> SpendingsPerCategory { get; set; }
    }
}