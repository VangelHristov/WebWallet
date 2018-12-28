using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebWallet.Services.GoalServices;
using WebWallet.ViewModels.Goal;
using WebWallet.Web.Controllers;
using WebWallet.Web.Extensions.Alert;

namespace WebWallet.Web.Areas.Authenticated.Controllers
{
    [Authorize]
    [Area("Authenticated")]
    public class GoalController : BaseController
    {
        private readonly IGoalService _goalService;

        public GoalController(IGoalService goalService)
        {
            this._goalService = goalService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GoalVM goalVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return View(goalVM);
            }

            if (!await this._goalService.Create(goalVM, User.Identity.Name))
            {
                return View(goalVM);
            }

            return RedirectToAction(nameof(All), new { timestamp = DateTime.Now.Ticks })
                .WithSuccess("Успех!", "Финансовата цел беше запазена.");
        }

        public async Task<IActionResult> All()
        {
            var goalsVM = await this._goalService.GetAll(User.Identity.Name);
            return View(goalsVM);
        }

        public async Task<IActionResult> Edit(string goalId)
        {
            var goalVM = await this._goalService.GetById(goalId);
            return View(goalVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GoalVM goalVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return View(goalVM);
            }

            await this._goalService.Update(goalVM);
            return RedirectToAction(nameof(All), new { timestamp = DateTime.Now.Ticks })
                .WithSuccess("Успех!", "Промените бяха запазени.");
        }

        public async Task<IActionResult> Delete(string goalId)
        {
            if (!await this._goalService.Delete(goalId))
            {
                return RedirectToAction(nameof(All), new { timestamp = DateTime.Now.Ticks })
                    .WithDanger("Грешка!", "Финансовата цел не беше изтрита.");
            }

            return RedirectToAction(nameof(All), new { timestamp = DateTime.Now.Ticks })
                .WithSuccess("Успех!", "Финансовата цел беше изтрита.");
        }
    }
}