using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebWallet.Services.TransactionServices;
using WebWallet.ViewModels.Transaction;
using WebWallet.Web.Controllers;
using WebWallet.Web.Extensions.Alert;

namespace WebWallet.Web.Areas.Authenticated.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this._transactionService = transactionService;
        }

        public IActionResult Create() => View();

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
                .WithSuccess("", "");
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
                .WithSuccess("", "");
        }

        public async Task<IActionResult> Delete(string transactionId)
        {
            ThrowIfNull(transactionId);

            await _transactionService.Delete(transactionId);
            return RedirectToAction(nameof(All), new { timestamp = DateTime.Now.Ticks.ToString() })
                .WithSuccess("", "");
        }
    }
}