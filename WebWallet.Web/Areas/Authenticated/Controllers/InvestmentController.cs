using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        }
    }
}