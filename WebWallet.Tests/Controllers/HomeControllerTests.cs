using Microsoft.AspNetCore.Mvc;
using WebWallet.Web.Controllers;
using Xunit;

namespace WebWallet.Tests.Controllers
{
    public class HomeControllerTests
    {
        private HomeController _controller;

        public HomeControllerTests()
        {
            this._controller = new HomeController();
        }

        [Fact]
        public void Index_Action_Should_Return_View_Result()
        {
            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_Action_Should_Return_View_Result()
        {
            // Act
            var result = _controller.Privacy();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}