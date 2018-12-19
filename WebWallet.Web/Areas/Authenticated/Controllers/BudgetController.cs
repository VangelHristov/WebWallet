using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebWallet.Services.BudgetServices;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.Budget;
using WebWallet.Web.Controllers;

namespace WebWallet.Web.Areas.Authenticated.Controllers
{
    [Area("Authenticated")]
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BudgetVM budgetVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return this.View(budgetVM);
            }

            if (!await this._budgetService.Create(budgetVM))
            {
                return this.View(budgetVM);
            }

            var user = await this._userService.GetByUsername(User.Identity.Name);
            budgetVM.UserId = user.Id;

            await this._budgetService.Create(budgetVM);
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All()
        {
            var user = await this._userService.GetByUsername(User.Identity.Name);
            var budgetVM = this._budgetService.GetAll(user.Id);

            return this.View(budgetVM);
        }

        public async Task<IActionResult> Edit(string budgetId)
        {
            var budgetVM = await this._budgetService.GetById(budgetId);
            return this.View(budgetVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BudgetVM budgetVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return this.View(budgetVM);
            }

            await this._budgetService.Update(budgetVM);
            return this.RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(string budgetId)
        {
            ThrowIfNull(budgetId);
            await this._budgetService.Delete(budgetId);

            return this.RedirectToAction(nameof(All));
        }
    }
}