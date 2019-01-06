using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;

namespace WebWallet.Tests.Mocks
{
    public class UrlHelperMock
    {
        public UrlHelperMock(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }

        public string ReturnUrl { get; set; }

        public IUrlHelper Object
        {
            get
            {
                var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
                mockUrlHelper
                    .Setup(
                        x => x.Action(
                            It.IsAny<UrlActionContext>()
                        )
                    )
                    .Returns(ReturnUrl)
                    .Verifiable();

                return mockUrlHelper.Object;
            }
        }
    }
}