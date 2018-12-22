using Microsoft.AspNetCore.Mvc;
using WebWallet.Services.InvestmentServices;
using WebWallet.Web.Controllers;

namespace WebWallet.Web.Areas.Authenticated.Controllers
{
    public class InvestmentController : BaseController
    {
        private readonly IInvestmentService _investmentService;

        public InvestmentController(IInvestmentService investmentService)
        {
            this._investmentService = investmentService;
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}