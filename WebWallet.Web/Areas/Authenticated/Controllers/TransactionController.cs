using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebWallet.Services.AccountServces;
using WebWallet.Services.TransactionServices;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.Transaction;
using WebWallet.Web.Controllers;
using WebWallet.Web.Extensions.Alert;

namespace WebWallet.Web.Areas.Authenticated.Controllers
{
    [Area("Authenticated")]
    [Authorize]
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public TransactionController(ITransactionService transactionService, IAccountService accountService, IUserService userService)
        {
            this._transactionService = transactionService;
            this._accountService = accountService;
            this._userService = userService;
        }

        public async Task<IActionResult> Create()
        {
            ViewData["UserAccounts"] = await GetAccountsSelectList();
            ViewData["TransactionCategories"] = TransactionCategories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionVM transactionVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return View(transactionVM);
            }

            await _transactionService.Create(transactionVM, User.Identity.Name);
            return RedirectToAction(nameof(All))
                .WithSuccess("Успех!", "Транзакцията е запазена.");
        }

        public async Task<IActionResult> All()
        {
            var transactionsVM = await _transactionService.GetAll(User.Identity.Name);
            return View(transactionsVM);
        }

        public async Task<IActionResult> Edit(string transactionId)
        {
            ThrowIfNull(transactionId);
            var transactionVM = await _transactionService.GetById(transactionId);
            ViewData["UserAccounts"] = await GetAccountsSelectList();
            ViewData["TransactionCategories"] = TransactionCategories;
            return View(transactionVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TransactionVM transactionVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return View(transactionVM);
            }

            await _transactionService.Update(transactionVM);
            return RedirectToAction(nameof(All))
                .WithSuccess("Успех!", "Променте са запазени.");
        }

        public async Task<IActionResult> Delete(string transactionId)
        {
            ThrowIfNull(transactionId);

            await _transactionService.Delete(transactionId);
            return RedirectToAction(nameof(All), new { timestamp = DateTime.Now.Ticks.ToString() })
                .WithSuccess("Успех!", "Транзакцията е изтрита.");
        }

        private async Task<List<SelectListItem>> GetAccountsSelectList()
        {
            var user = await _userService.GetByUsername(User.Identity.Name);
            var accounts = _accountService.GetAll(user.Id);
            var accountsSelectList = new List<SelectListItem>();

            foreach (var account in accounts)
            {
                accountsSelectList.Add(
                    new SelectListItem
                    {
                        Value = $"{account.Name};{account.Id}",
                        Text = account.Name
                    });
            }

            return accountsSelectList;
        }
    }
}