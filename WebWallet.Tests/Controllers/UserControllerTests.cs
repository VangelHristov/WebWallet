using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using WebWallet.Services.UserServices;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebWallet.Tests.Controllers
{
    public class UserControllerTests
    {
        private IUserService _userService;
        private IEmailSender _emailSender;

        public UserControllerTests()
        {
            _userService = new Mock<IUserService>().Object;
            _emailSender = new Mock<IEmailSender>().Object;
        }

        [Fact]
        public void adsf()
        {
        }
    }
}