using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebWallet.Services.AccountServces;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.Account;
using WebWallet.Web.Controllers;

namespace WebWallet.Web.Areas.Authenticated.Controllers
{
    [Area("Authenticated")]
    [Authorize]
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
                return View(accountVM);
            }

            var user = await this._userService.GetByUsername(User.Identity.Name);
            accountVM.UserId = user.Id;

            await this._accountService.Create(accountVM);

            return RedirectToAction("All");
        }

        public async Task<IActionResult> All()
        {
            var user = await this._userService.GetByUsername(User.Identity.Name);
            var accounts = this._accountService.GetAll(user.Id);
            return View(accounts);
        }

        public IActionResult Details(string accountId)
        {
            ThrowIfNull(accountId);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(AccountVM accountVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return View(accountVM);
            }

            return RedirectToAction("All");
        }
    }
}