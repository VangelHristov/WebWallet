using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.User;
using WebWallet.Web.Controllers;

namespace WebWallet.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;

        public UserController(IUserService userService, IEmailSender emailSender)
        {
            this._userService = userService;
            this._emailSender = emailSender;
        }

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
        public async Task<IActionResult> Register(RegistrationVM registrationVM)
        {
            if (!ModelState.IsValid)
            {
                return this.View(registrationVM);
            }

            var user = await this._userService.Register(registrationVM);
            ThrowIfNull(user);

            var confirmationToken = await this._userService.GetEmailConfirmationToken(user);

            string confirmationLink = Url.Action(
                "EmailValidation",
                "User",
                new { userId = user.Id, token = confirmationToken },
                protocol: HttpContext.Request.Scheme);

            var subject = "Web Wallet Email Validation";
            var message = "Благодарим ви че се регистрирахте в WebWallet. Моля кликнете потвърди имейл за да завършите вашата регистрация.\n";
            message += $"<a href='{confirmationLink}'> ПОТВЪРДИ ИМЕЙЛ</а> ";

            await this._emailSender.SendEmailAsync(user.Email, subject, message);

            return this.RedirectToAction("RegistrationSuccess", new { userEmail = user.Email });
        }

        public IActionResult RegistrationSuccess(string userEmail)
        {
            ViewData["userEmail"] = userEmail;
            return this.View();
        }

        public async Task<IActionResult> EmailValidation(string userId, string token)
        {
            var user = await this._userService.GetById(userId);

            var emailConfirmed = await this._userService.ConfirmEmail(user, token);
            var confirmEmailVM = new ConfirmEmailVM();
            if (emailConfirmed)
            {
                confirmEmailVM.Result = "Успех!";
                confirmEmailVM.Message = "Имейлът беше потвърден!";

                return View(confirmEmailVM);
            }

            confirmEmailVM.Result = "Грешка";
            confirmEmailVM.Message = "Не можем да потвърдим имейла!";

            return View(confirmEmailVM);
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