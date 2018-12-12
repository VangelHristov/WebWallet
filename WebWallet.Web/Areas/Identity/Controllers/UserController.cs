using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.ViewModels.User;

namespace WebWallet.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class UserController : Controller
    {
        public IActionResult Login() => this.View();

        public IActionResult Register() => this.View();

        public IActionResult ForgotPassword() => this.View();

        public IActionResult ResetPassword() => this.View();

        [Authorize]
        public IActionResult Logout()
        {
            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Register(RegistrationVM registrationVM)
        {
            if (!ModelState.IsValid)
            {
                return this.View(registrationVM);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return this.View(loginVM);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(email))
            {
                return this.View();
            }

            return this.RedirectToAction("ResetPassword");
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return this.View(resetPasswordVM);
            }
            // TODO: change the password and login user on success redirect to home
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}