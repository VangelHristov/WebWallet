using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebWallet.Data.Contracts;
using WebWallet.Models.Contracts;
using WebWallet.Models.Entities;
using WebWallet.Web.Controllers;
using WebWallet.ViewModels.Account;
using WebWallet.Services.AccountServces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebWallet.Web.Areas.Authenticated.Controllers
{
    [Area("Authenticated")]
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
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
                return View(accountVM);
            }

            await this._accountService.Create(accountVM);
            return RedirectToAction("All");
        }

        public IActionResult All()
        {
            return View();
        }

        public IActionResult Details(string accountId)
        {
            if (accountId == null)
            {
                ModelState.AddModelError("", "Номер на сметка е задължителен.");
                return View();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(AccountVM accountVM)
        {
            if (!ModelState.IsValid)
            {
                return View(accountVM);
            }

            return RedirectToAction("All");
        }
    }
}