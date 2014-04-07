using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Moq;

namespace CountDownTests
{
    public static class UnitTestHelper
    {
        public static ControllerContext GetMockControllerContext(bool authenticated)
        {
            // Lots of mocking is required to fake the ControllerContext.
            // The goal here is to force User.Identity.IsAuthenticated to return true or false.
            var mockPrincipal = new Mock<IPrincipal>();
            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.Setup(x => x.IsAuthenticated).Returns(authenticated);
            mockPrincipal.Setup(x => x.Identity).Returns(mockIdentity.Object);

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockHttpRequest = new Mock<HttpRequestBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockHttpRequest.Object);
            mockHttpContext.Setup(x => x.User).Returns(mockPrincipal.Object);

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);
            return mockControllerContext.Object;
        }

        public static ControllerContext GetMockControllerContextWithException()
        {
            var mockPrincipal = new Mock<IPrincipal>();
            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.Setup(x => x.IsAuthenticated).Throws(new Exception());
            mockPrincipal.Setup(x => x.Identity).Returns(mockIdentity.Object);

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockHttpRequest = new Mock<HttpRequestBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockHttpRequest.Object);
            mockHttpContext.Setup(x => x.User).Returns(mockPrincipal.Object);

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);
            return mockControllerContext.Object;
        }
    }
}
