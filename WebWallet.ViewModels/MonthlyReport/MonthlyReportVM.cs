using System;
using System.Collections.Generic;

namespace WebWallet.ViewModels.MonthlyReport
{
    public class MonthlyReportVM
    {
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal EndBalance { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalInvested { get; set; }
        public decimal TotalSpendings { get; set; }
        public Dictionary<string, string> SpendingsPerCategory { get; set; }
        public Dictionary<string, string> SpendingsPerMainCategory { get; set; }
        public Dictionary<string, decimal> InvestmentsPerType { get; set; }
    }
}