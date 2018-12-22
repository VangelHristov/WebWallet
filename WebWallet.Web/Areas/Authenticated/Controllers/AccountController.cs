using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading.Tasks;
using WebWallet.Services.AccountServces;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.Account;
using WebWallet.Web.Controllers;
using WebWallet.Web.Extensions.Alert;

namespace WebWallet.Web.Areas.Authenticated.Controllers
{
    [Area("Authenticated")]
    [Authorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(IAccountService accountService, IUserService userService)
        {
            this._accountService = accountService;
            this._userService = userService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountVM accountVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return View(accountVM)
                    .WithDanger("Грешка!", "Моля поправете грешките маркирани с червено.");
            }

            var user = await this._userService.GetByUsername(User.Identity.Name);
            accountVM.UserId = user.Id;

            await this._accountService.Create(accountVM);

            return this.RedirectToAction(nameof(All))
                 .WithSuccess("Успех!", "Успешно добавихте нова сметка.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> All()
        {
            var user = await this._userService.GetByUsername(User.Identity.Name);
            var accounts = this._accountService.GetAll(user.Id);
            return View(accounts);
        }

        public async Task<IActionResult> Edit(string accountId)
        {
            ThrowIfNull(accountId);
            var accountDetails = await this._accountService.GetById(accountId);
            ThrowIfNull(accountDetails);
            return View(accountDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountVM accountVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return View(accountVM)
                    .WithDanger("Грешка!", "Моля поправете грешките маркирани с червено.");
            }

            if (!await this._accountService.Update(accountVM))
            {
                throw new OperationException("Error occured while updating account");
            }

            return this.RedirectToAction(nameof(All))
                    .WithSuccess("Успех!", "Сметката беше обновена.");
        }

        public async Task<IActionResult> Delete(string accountId)
        {
            ThrowIfNull(accountId);
            if (!await this._accountService.Delete(accountId))
            {
                throw new OperationException("Error occured while deleting account");
            }

            return this.RedirectToAction(nameof(All))
                .WithSuccess("Успех!", "Сметката беше изтрита.");
        }
    }
}