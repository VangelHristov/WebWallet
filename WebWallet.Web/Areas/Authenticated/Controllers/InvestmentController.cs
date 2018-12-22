using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebWallet.Services.InvestmentServices;
using WebWallet.ViewModels.Investment;
using WebWallet.Web.Controllers;
using WebWallet.Web.Extensions.Alert;

namespace WebWallet.Web.Areas.Authenticated.Controllers
{
    [Authorize]
    [Area("Authenticated")]
    public class InvestmentController : BaseController
    {
        private readonly IInvestmentService _investmentService;

        public InvestmentController(IInvestmentService investmentService)
        {
            this._investmentService = investmentService;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(InvestmentVM investmentVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return View(investmentVM);
            }

            if (!await this._investmentService.Create(investmentVM, User.Identity.Name))
            {
                return View(investmentVM);
            }

            return RedirectToAction(nameof(All))
                .WithSuccess("Успех!", "Инвестицията беше успешно запазена.");
        }

        public async Task<IActionResult> All()
        {
            var investmentsVM = await this._investmentService.GetAll(User.Identity.Name);
            return View(investmentsVM);
        }
    }
}