using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using WebWallet.Tests.Mocks;
using WebWallet.ViewModels.User;
using WebWallet.Web.Areas.Dashboard.Controllers;
using WebWallet.Web.Areas.Identity.Controllers;
using WebWallet.Web.Extensions.Alert;
using Xunit;

namespace WebWallet.Tests.Controllers
{
    public class UserControllerTests
    {
        private UserServiceMock _userService;
        private EmailSenderMock _emailSender;
        private UserController _controller;

        public UserControllerTests()
        {
            _userService = new UserServiceMock();
            _emailSender = new EmailSenderMock();
            _controller = new UserController(_userService.Object, _emailSender.Object)
            {
                Url = new UrlHelperMock("/RegistrationSuccess/").Object,
                ControllerContext = { HttpContext = new DefaultHttpContext() }
            };
        }

        [Fact]
        public void Login_Get_Returns_ViewResult_With_ReturnURL()
        {
            // Arrange
            var returnUrl = "/Authenticated/Report/Investment";

            // Act
            var result = _controller.Login(returnUrl);

            // Assert
            result
                .Should()
                .BeViewResult()
                .WithDefaultViewName()
                .WithViewData("returnUrl", returnUrl);
        }

        [Fact]
        public void Register_Get_Returns_ViewResult()
        {
            // Act
            var result = _controller.Register();

            // Assert
            result
                .Should()
                .BeViewResult()
                .WithDefaultViewName();
        }

        [Fact]
        public async Task Register_Post_Returns_RedirectToAction_RegistrationSuccess_With_SuccessAlert()
        {
            // Act
            var result = await _controller.Register(_userService.RegistrationVM);

            // Assert
            var viewResult = Assert.IsType<AlertDecoratorResult>(result);
            var routeValues = Assert.IsType<RedirectToActionResult>(viewResult.Result);
            Assert.Equal("RegistrationSuccess", routeValues.ActionName);
            Assert.Null(routeValues.ControllerName);
        }

        [Fact]
        public async Task Register_Post_Throws_When_Username_Is_Missing()
        {
            // Arrange
            var registrationVm = new RegistrationVM
            {
                UserName = "",
                Password = "some[assw0rd",
                ConfirmPassword = "some[assw0rd",
                Email = "some@email.com"
            };

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.Register(registrationVm));
        }

        [Fact]
        public void ForgotPassword_Get_Returns_ViewResult()
        {
            // Act
            var result = _controller.ForgotPassword();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void RegistrationSuccess_Get_Returns_ViewResult()
        {
            // Arrange
            var email = "vangel@mail.com";

            // Act
            var result = _controller.RegistrationSuccess(email);

            // Assert
            result
                .Should()
                .BeViewResult()
                .WithDefaultViewName()
                .WithViewData("userEmail", email);
        }

        [Fact]
        public async Task EmailValidation_Returns_ViewResult_With_Success()
        {
            // Act
            var result = await _controller.EmailValidation(_userService.User.Id, _userService.EmailConfirmationToken);

            // Assert

            result
                .Should()
                .BeViewResult()
                .WithDefaultViewName();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ConfirmEmailVM>(viewResult.Model);
            Assert.Equal("Успех!", model.Result);
        }

        [Fact]
        public async Task EmailValidation_Returns_ViewResult_With_Error()
        {
            // Act
            var result = await _controller.EmailValidation("notAValidId", _userService.EmailConfirmationToken);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ConfirmEmailVM>(viewResult.Model);
            Assert.Equal("Грешка", model.Result);
        }

        [Fact]
        public async Task Login_Returns_Redirect_To_ReturnUrl_When_Available()
        {
            // Arrange
            var returnUrl = "/SomeController/SomeAction/";
            // Act
            var result = await _controller.Login(_userService.LoginVM, returnUrl);

            // Assert
            result
                .Should()
                .BeRedirectResult()
                .WithUrl(returnUrl);
        }

        [Fact]
        public async Task Login_Returns_Redirect_To_Report_IncomeAndSpendings()
        {
            // Act
            var result = await _controller.Login(_userService.LoginVM);

            // Assert
            result
                .Should()
                .BeRedirectResult()
                .WithUrl("/Authenticated/Report/IncomeAndSpendings");
        }

        [Fact]
        public async Task Login_Returns_LoginView_With_Model_Error_When_Password_Is_Incorrect()
        {
            // Arrange
            var loginVm = new LoginVM
            {
                UserName = _userService.LoginVM.UserName,
                Password = _userService.LoginVM.Password + "a",
                RememberMe = true
            };

            // Act
            var result = (ViewResult)await _controller.Login(loginVm);

            // Assert
            result
                .Should()
                .BeViewResult()
                .WithDefaultViewName();

            Assert.True(_controller.ViewData.ModelState.ErrorCount == 1);
        }

        [Fact]
        public async Task ForgotPassword_Throws_When_NonExisting_Username_Is_Given()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.ForgotPassword(""));
        }

        [Fact]
        public async Task ForgetPassword_Returns_Redirect_To_Home_Index_With_SuccessAlert()
        {
            // Act
            var result = await _controller.ForgotPassword(_userService.User.UserName);

            // Assert
            var viewResult = Assert.IsType<AlertDecoratorResult>(result);
            var routeValues = Assert.IsType<RedirectToActionResult>(viewResult.Result);
            Assert.Equal("Index", routeValues.ActionName);
            Assert.Equal("Home", routeValues.ControllerName);
        }

        [Fact]
        public void ResetPassword_Returns_ViewResult_With_Correct_ViewData()
        {
            // Arrange
            var token = "token";
            var userId = "userId";

            // Act
            var result = _controller.ResetPassword(token, userId);

            // Assert
            result
                .Should()
                .BeViewResult()
                .WithDefaultViewName()
                .WithViewData("userId", userId)
                .WithViewData("passwordResetToken", token);
        }

        [Fact]
        public async Task ResetPassword_Post_Returns_ViewResult_With_SuccessAlert()
        {
            // Arrange
            var resetVm = new ResetPasswordVM
            {
                Password = "newPassword",
                ConfirmPassword = "newPassword",
                Token = _userService.PasswordResetToken,
                UserId = _userService.User.Id
            };

            // Act
            var result = await _controller.ResetPassword(resetVm);

            // Assert
            var viewResult = Assert.IsType<AlertDecoratorResult>(result);
            var routeValues = Assert.IsType<RedirectToActionResult>(viewResult.Result);
            Assert.Equal("IncomeAndSpendings", routeValues.ActionName);
            Assert.Equal("Report", routeValues.ControllerName);
        }

        [Fact]
        public async Task ResetPassword_Post_Returns_Default_View_When_Token_And_Id_Not_Match()
        {
            // Arrange
            var resetVm = new ResetPasswordVM
            {
                Password = "newPassword",
                ConfirmPassword = "newPassword",
                Token = _userService.PasswordResetToken + "asd",
                UserId = _userService.User.Id
            };

            // Act
            var result = await _controller.ResetPassword(resetVm);

            // Assert
            result
                .Should()
                .BeViewResult()
                .WithDefaultViewName();
        }

        [Fact]
        public async Task Logout_Returns_ViewResult_WithSuccess()
        {
            // Act
            var result = await _controller.Logout();

            // Assert
            var viewResult = Assert.IsType<AlertDecoratorResult>(result);
            var routeValues = Assert.IsType<RedirectToActionResult>(viewResult.Result);
            Assert.Equal("Index", routeValues.ActionName);
            Assert.Equal("Home", routeValues.ControllerName);
        }
    }
}