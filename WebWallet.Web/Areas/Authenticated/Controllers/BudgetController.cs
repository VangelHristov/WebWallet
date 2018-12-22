using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebWallet.Services.BudgetServices;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.Budget;
using WebWallet.Web.Controllers;

namespace WebWallet.Web.Areas.Authenticated.Controllers
{
    [Area("Authenticated")]
    [Authorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class BudgetController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IBudgetService _budgetService;

        private readonly List<SelectListItem> _periods = new List<SelectListItem>
            {
                new SelectListItem { Value = "7 дни", Text = "7 дни" },
                new SelectListItem { Value = "14 дни", Text = "14 дни" },
                new SelectListItem { Value = "1 месец", Text = "1 месец"  },
                new SelectListItem { Value = "3 месеца", Text = "3 месеца" },
                new SelectListItem { Value = "6 месеца", Text = "6 месеца"  },
                new SelectListItem { Value = "1 година", Text = "1 година"  }
            };

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
                return this.View(budgetVM);
            }

            var user = await this._userService.GetByUsername(User.Identity.Name);
            budgetVM.UserId = user.Id;

            if (!await this._budgetService.Create(budgetVM))
            {
                return this.View(budgetVM);
            }

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