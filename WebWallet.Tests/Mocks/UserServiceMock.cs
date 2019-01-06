using System.Threading.Tasks;
using Moq;
using WebWallet.Models.Entities;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.User;

namespace WebWallet.Tests.Mocks
{
    public class UserServiceMock
    {
        public UserServiceMock()
        {
            User = new User
            {
                Id = "johnDoe1",
                UserName = "johnDoe",
                Email = "johnDoe@mail.bg"
            };

            RegistrationVM = new RegistrationVM
            {
                UserName = "johnDoe",
                Password = "TopSecret!2",
                ConfirmPassword = "TopSecret!2",
                Email = "johnDoe@mail.bg"
            };

            LoginVM = new LoginVM
            {
                UserName = "johnDoe",
                Password = "TopSecret!2",
                RememberMe = true
            };

            EmailConfirmationToken = "emailConfirmationToken";

            PasswordResetToken = "passwordResetToken";

            Configurable = new Mock<IUserService>();
        }

        public RegistrationVM RegistrationVM { get; set; }

        public LoginVM LoginVM { get; set; }

        public User User { get; set; }

        public string EmailConfirmationToken { get; set; }

        public string PasswordResetToken { get; set; }

        public Mock<IUserService> Configurable { get; set; }

        public IUserService Object
        {
            get
            {
                var userServiceMock = new Mock<IUserService>();

                userServiceMock
                    .Setup(x => x.Register(RegistrationVM))
                    .ReturnsAsync(User)
                    .Verifiable();

                userServiceMock
                    .Setup(x => x.GetEmailConfirmationToken(User))
                    .ReturnsAsync(EmailConfirmationToken)
                    .Verifiable();

                userServiceMock
                    .Setup(x => x.ConfirmEmail(User, EmailConfirmationToken))
                    .ReturnsAsync(true)
                    .Verifiable();

                userServiceMock
                    .Setup(x => x.GetById(User.Id))
                    .ReturnsAsync(User)
                    .Verifiable();

                userServiceMock
                    .Setup(x => x.Login(
                        LoginVM.UserName,
                        LoginVM.Password,
                        LoginVM.RememberMe))
                    .ReturnsAsync(true)
                    .Verifiable();

                userServiceMock
                    .Setup(x => x.GetByUsername(RegistrationVM.UserName))
                    .ReturnsAsync(User)
                    .Verifiable();

                userServiceMock
                    .Setup(x => x.GetPasswordResetToken(User))
                    .ReturnsAsync(PasswordResetToken)
                    .Verifiable();

                userServiceMock
                    .Setup(x => x.ResetPasswordAndLogin(
                        User.Id,
                        PasswordResetToken,
                        It.IsAny<string>()))
                    .ReturnsAsync(true)
                    .Verifiable();

                userServiceMock
                    .Setup(x => x.Logout())
                    .Returns(Task.FromResult(""));

                return userServiceMock.Object;
            }
        }
    }
}