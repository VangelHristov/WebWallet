using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebWallet.Services.PaymentServices;
using WebWallet.ViewModels.RecurringPayment;
using WebWallet.Web.Controllers;
using WebWallet.Web.Extensions.Alert;

namespace WebWallet.Web.Areas.Authenticated.Controllers
{
    [Area("Authenticated")]
    [Authorize]
    public class RecurringPaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public RecurringPaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RecurringPaymentVM paymentVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return View(paymentVM);
            }

            await _paymentService.Create(paymentVM, User.Identity.Name);
            return RedirectToAction(nameof(All))
                .WithSuccess("Инфо:", "Нова сметка/заем беше създадена.");
        }

        public async Task<IActionResult> All()
        {
            var username = User.Identity.Name;
            var paymentsVM = await _paymentService.GetAll(username);
            return View(paymentsVM);
        }

        public async Task<IActionResult> Edit(string paymentId)
        {
            var paymentVM = await _paymentService.GetById(paymentId);
            return View(paymentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RecurringPaymentVM paymentVM)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors(ModelState);
                return View(paymentVM);
            }

            await _paymentService.Update(paymentVM);
            return RedirectToAction(nameof(All))
                .WithSuccess("Инфо", "Промените са запазени.");
        }

        public async Task<IActionResult> Delete(string paymentId)
        {
            await _paymentService.Delete(paymentId);
            return RedirectToAction(nameof(All), new { timestamp = DateTime.Now.Ticks.ToString() })
                .WithSuccess("Инфо:", "Записът беше изтрит.");
        }
    }
}