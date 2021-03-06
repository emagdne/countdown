﻿using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using CountDown.Models.Security;
using Moq;

namespace CountDown.IntegrationTests
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public static class IntegrationTestHelper
    {
        public static ControllerContext GetMockControllerContext(bool authenticated)
        {
            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.Setup(x => x.IsAuthenticated).Returns(authenticated);
            return CreateMockControllerContext(mockIdentity.Object);
        }

        public static ControllerContext GetMockControllerContextWithException()
        {
            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.Setup(x => x.IsAuthenticated).Throws(new Exception());
            return CreateMockControllerContext(mockIdentity.Object);
        }

        public static ControllerContext GetMockControllerContextWithCountDownIdentity(
            CountDownIdentity countDownIdentity,
            bool authenticated)
        {
            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.Setup(x => x.IsAuthenticated).Returns(authenticated);
            countDownIdentity.Identity = mockIdentity.Object;
            return CreateMockControllerContext(countDownIdentity);
        }

        private static ControllerContext CreateMockControllerContext(IIdentity identity)
        {
            // Lots of mocking is required to fake the ControllerContext.
            var mockPrincipal = new Mock<IPrincipal>();
            mockPrincipal.Setup(x => x.Identity).Returns(identity);

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockHttpRequest = new Mock<HttpRequestBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockHttpRequest.Object);
            mockHttpContext.Setup(x => x.User).Returns(mockPrincipal.Object);

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);
            return mockControllerContext.Object;
        }

        public static string GetStandardJsonStatus(JsonResult result)
        {
            var property = result.Data.GetType().GetProperty("Status");
            return (string) property.GetValue(result.Data);
        }

        public static string GetStandardJsonError(JsonResult result)
        {
            var property = result.Data.GetType().GetProperty("Error");
            return (string) property.GetValue(result.Data);
        }
    }
}