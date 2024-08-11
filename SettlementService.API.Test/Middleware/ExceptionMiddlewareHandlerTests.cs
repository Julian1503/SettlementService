using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using SettlementService.API.Middleware;
using SettlementService.Domain.Abstractions;

namespace SettlementService.API.Test.Middleware
{
    [TestFixture]
    public class ExceptionMiddlewareHandlerTests
    {
        private Mock<RequestDelegate> _mockRequestDelegate;
        private Mock<ILogger<ExceptionMiddlewareHandler>> _mockLogger;
        private ExceptionMiddlewareHandler _middleware;

        [SetUp]
        public void SetUp()
        {
            _mockRequestDelegate = new Mock<RequestDelegate>();
            _mockLogger = new Mock<ILogger<ExceptionMiddlewareHandler>>();
            _middleware = new ExceptionMiddlewareHandler(_mockRequestDelegate.Object, _mockLogger.Object);
        }

        [Test]
        public async Task InvokeAsync_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            _mockRequestDelegate.Setup(x => x(It.IsAny<HttpContext>())).Throws(new Exception("Test exception"));
            var context = new DefaultHttpContext();
            var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // Act
            await _middleware.InvokeAsync(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = new StreamReader(context.Response.Body).ReadToEnd();
            var errorDetails = JsonSerializer.Create().Deserialize<Error>(new JsonTextReader(new StringReader(responseText)));

            // Assert
            Assert.That(actual: context.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
            Assert.That(actual: errorDetails.Detail, Is.EqualTo("Internal Server Error."));
        }
    }
}
