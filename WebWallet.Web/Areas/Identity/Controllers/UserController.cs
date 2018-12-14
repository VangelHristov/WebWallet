using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
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

        [Authorize]
        public IActionResult Logout() => this.RedirectToAction("Index", "Home");

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
                confirmEmailVM.Message = "Имейлът беше потвърден!\nРегистрацията е активирана.";

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
        public async Task<IActionResult> ForgotPassword(string username)
        {
            var user = await this._userService.GetByUsername(username);
            ThrowIfNull(user);
            var token = await this._userService.GetPasswordResetToken(user);
            var resetLink = Url.Action("ResetPassword", "User", new { userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);

            var message = $"Кликни на линка за да въведеш нова парола. <br/> <a href={resetLink}> Нова парола</a>";

            await this._emailSender.SendEmailAsync(user.Email, "Password Reset", message);

            return this.RedirectToAction("Index", "Home", new { area = "" });
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
            var passwordReset = await this._userService.ResetPassword(resetPasswordVM.UserId, resetPasswordVM.Token, resetPasswordVM.Password);

            if (!passwordReset)
            {
                return this.Redirect("/StatusCode/500");
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}