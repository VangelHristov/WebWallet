using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public async Task<IActionResult> Edit(string investmentId)
        {
            ThrowIfNull(investmentId);
            var investmentVM = await this._investmentService.GetById(investmentId);

            return View(investmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InvestmentVM investmentVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return View(investmentVM);
            }

            if (!await this._investmentService.Update(investmentVM))
            {
                return View(investmentVM)
                    .WithWarning("Грешка!", "Моля задайте коректни стойности за всички полета.");
            }

            return RedirectToAction(nameof(All))
                .WithSuccess("Успех!", "Промените бяха запазени.");
        }

        public async Task<IActionResult> Delete(string investmentId)
        {
            ThrowIfNull(investmentId);
            await this._investmentService.Delete(investmentId);

            return this.RedirectToAction(nameof(All), new { timestamp = DateTime.Now.Ticks })
                .WithSuccess("Успех!", "Инвестицията беше изтрита.");
        }
    }
}