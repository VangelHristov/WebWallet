using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebWallet.Services.BudgetServices;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.Budget;
using WebWallet.Web.Controllers;
using WebWallet.Web.Extensions.Alert;

namespace WebWallet.Web.Areas.Authenticated.Controllers
{
    [Area("Authenticated")]
    [Authorize]
    public class BudgetController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IBudgetService _budgetService;

        public BudgetController(IUserService userService, IBudgetService budgetService)
        {
            this._userService = userService;
            this._budgetService = budgetService;
        }

        public IActionResult Create()
        {
            ViewData["BudgetPeriods"] = this._periods;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BudgetVM budgetVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return this.View(budgetVM)
                    .WithDanger("Грешка!", "Моля поправете грешките маркирани с червено.");
            }

            var user = await this._userService.GetByUsername(User.Identity.Name);
            budgetVM.UserId = user.Id;

            if (!await this._budgetService.Create(budgetVM))
            {
                return this.View(budgetVM);
            }

            return this.RedirectToAction(nameof(All))
                    .WithSuccess("Успех!", "Успешно добавихте нов бюджет.");
        }

        public async Task<IActionResult> All(long? timestamp = null)
        {
            var user = await this._userService.GetByUsername(User.Identity.Name);
            var budgetVM = this._budgetService.GetAll(user.Id);

            return this.View(budgetVM);
        }

        public async Task<IActionResult> Edit(string budgetId)
        {
            var budgetVM = await this._budgetService.GetById(budgetId);
            ViewData["BudgetPeriods"] = this._periods;
            return this.View(budgetVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BudgetVM budgetVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                ViewData["BudgetPeriods"] = this._periods;
                return this.View(budgetVM)
                    .WithDanger("Грешка!", "Моля поправете грешките маркирани с червено.");
            }

            await this._budgetService.Update(budgetVM);

            return this.RedirectToAction(nameof(All))
                .WithSuccess("Успех!", "Бюджета беше обновен.");
        }

        public async Task<IActionResult> Delete(string budgetId)
        {
            ThrowIfNull(budgetId);
            await this._budgetService.Delete(budgetId);

            return this.RedirectToAction(nameof(All), new { timestamp = DateTime.Now.Ticks })
                .WithSuccess("Успех!", "Бюджета беше изтрит.");
        }
    }
}