using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Backend.Controllers;
using System.Collections.Generic;
using Backend.Models;

namespace Backend.Tests
{
    public class AuthControllerTests
    {
        [Fact]
        public void Login_ReturnsUnauthorized_WhenUserNotFound()
        {
            // Arrange
            var controller = new AuthController("fake-connection-string");
            var loginRequest = new LoginRequest { Username = "notfound", Password = "wrong" };

            // Act
            var result = controller.Login(loginRequest);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        // Additional tests can be added for:
        // - Successful login
        // - Invalid password
        // - Inactive user
        // - Exception handling
    }
}
