using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Moq;

namespace WebWallet.Tests.Mocks
{
    public class EmailSenderMock
    {
        public IEmailSender Object
        {
            get
            {
                var emailSenderMock = new Mock<IEmailSender>();

                emailSenderMock
                    .Setup(x => x.SendEmailAsync(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                    .Returns(Task.FromResult(""));

                return emailSenderMock.Object;
            }
        }

        public Mock<IEmailSender> Configurable { get; set; }
    }
}