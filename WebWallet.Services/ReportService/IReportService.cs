using System.Collections.Generic;
using System.Threading.Tasks;
using WebWallet.ViewModels.MonthlyReport;

namespace WebWallet.Services.ReportService
{
    public interface IReportService
    {
        Task<MonthlyReportVM> GetCurrentMonthReport(string username);

        Task<IList<MonthlyReportVM>> GetAllReports(string username);

        Task<bool> Create(string username);

        Task<bool> DeleteAll(string username);
    }
}