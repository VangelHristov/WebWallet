using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WebWallet.Services.ReportService;
using WebWallet.Web.Controllers;

namespace WebWallet.Web.Areas.Dashboard.Controllers
{
    [Authorize]
    [Area("Authenticated")]
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            this._reportService = reportService;
        }

        public IActionResult IncomeAndSpendings() => View();

        public async Task<JsonResult> AllReports()
        {
            var reportVM = await _reportService.GetAllReports(User.Identity.Name);
            return Json(JsonConvert.SerializeObject(reportVM));
        }

        public IActionResult Investments() => View();
    }
}