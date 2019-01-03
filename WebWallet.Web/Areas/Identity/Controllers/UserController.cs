using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.User;
using WebWallet.Web.Controllers;
using WebWallet.Web.Extensions.Alert;

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

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return this.View();
        }

        public IActionResult Register() => this.View();

        public IActionResult ForgotPassword() => this.View();

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._userService.Logout();
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { area = "" })
                .WithSuccess("Успешен изход.", "До скоро!");
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

            var message = Regex.Replace(
                registrationVM.ActivationEmailBody,
                @"https:\/\/localhost:5001\/Identity\/User\/EmailValidation",
                confirmationLink);

            await this._emailSender.SendEmailAsync(user.Email, registrationVM.ActivationEmailSubjec, message);

            return this.RedirectToAction("RegistrationSuccess", new { userEmail = user.Email })
                .WithSuccess("Регистрацията е създадена.", "Моля потвърдете имейла си преди за да се активира регистрацията.");
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
                confirmEmailVM.Message = "Имейлът беше потвърден!\nРегистрацията е активирана.";
                return View(confirmEmailVM);
            }

            confirmEmailVM.Result = "Грешка";
            confirmEmailVM.Message = "Не можем да потвърдим имейла!";

            return View(confirmEmailVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM, string returnUrl = null)
        {
            if (!(ModelState.IsValid &&
                    await this._userService
                        .Login(loginVM.UserName, loginVM.Password, loginVM.RememberMe))
               )
            {
                ModelState.AddModelError("", "Грешно име или парола.");
                return this.View(loginVM);
            }

            return returnUrl != null
                ? this.Redirect(returnUrl)
                : this.Redirect("/Authenticated/Report/IncomeAndSpendings");
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string username)
        {
            var user = await this._userService.GetByUsername(username);
            ThrowIfNull(user);

            var token = await this._userService.GetPasswordResetToken(user);
            var resetLink = Url.Action(
                "ResetPassword",
                "User",
                new { userId = user.Id, token = token },
                protocol: HttpContext.Request.Scheme);
            var message = $"Кликни на линка за да въведеш нова парола. <br/> <a href={resetLink}> Нова парола</a>";
            await this._emailSender.SendEmailAsync(user.Email, "Password Reset", message);

            return this.RedirectToAction("Index", "Home", new { area = "" })
                .WithSuccess("Успех!", "Изпратихме ви имейл, в който има инструкции за създаване на нова парола.");
        }

        public IActionResult ResetPassword(string token, string userId)
        {
            ViewData["passwordResetToken"] = token;
            ViewData["userId"] = userId;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return this.View(resetPasswordVM);
            }

            var passwordResetSucceded = await this._userService.
                ResetPasswordAndLogin(resetPasswordVM.UserId, resetPasswordVM.Token, resetPasswordVM.Password);
            if (!passwordResetSucceded)
            {
                return this.View(resetPasswordVM);
            }

            return this.RedirectToAction("IncomeAndSpendings", "Report", new { area = "Authenticated" })
                .WithSuccess("Успех!", "Вашата нова парола беше запазена.");
        }
    }
}