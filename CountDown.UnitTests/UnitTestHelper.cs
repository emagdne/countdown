﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Moq;

namespace CountDown.UnitTests
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public static class UnitTestHelper
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

        /// <summary>
        /// <para>
        ///     Manually validates a model containing data annotations and returns all errors.
        /// </para>
        /// <para>
        ///     Taken from: http://stackoverflow.com/questions/2167811/unit-testing-asp-net-dataannotations-validation
        /// </para>
        /// </summary>
        public static List<string> GetValidationErrors(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults.Select(x => x.ErrorMessage).ToList();
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