using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using RESTApisDemo.Models;

namespace RESTAPIsDemo.Test.Authentication
{
    [TestFixture]
    public sealed class LoginController_BadRequestTests
        : LoginControllerTestBase
    {
        [Test]
        public void Should_return_400_BadRequest_if_null_credentials_submitted ()
        {
            // Arrange
            Credential? nullCredential = null;

            // Act
            var apiResponse = base.loginController?.Login (nullCredential);

            // Assert for Login - Bad Request response.
            this.AssertLoginBadRequestExpectations (apiResponse);
        }

        [Test]
        public void Should_return_400_BadRequest_if_user_id_is_null ()
        {
            // Arrange
            var nullCredential
                = new Credential { UserId = null, Password = "password" };

            // Act
            var apiResponse = base.loginController?.Login (nullCredential);

            // Assert for Login - Bad Request response.
            this.AssertLoginBadRequestExpectations (apiResponse);
        }

        [Test]
        public void Should_return_400_BadRequest_if_user_id_is_empty ()
        {
            // Arrange
            var nullCredential
                = new Credential { UserId = string.Empty, Password = "password" };

            // Act
            var apiResponse = base.loginController?.Login (nullCredential);

            // Assert for Login - Bad Request response.
            this.AssertLoginBadRequestExpectations (apiResponse);
        }

        [Test]
        public void Should_return_400_BadRequest_if_password_is_null ()
        {
            // Arrange
            var nullCredential
                = new Credential { UserId = "userId", Password = null };

            // Act
            var apiResponse = base.loginController?.Login (nullCredential);

            // Assert for Login - Bad Request response.
            this.AssertLoginBadRequestExpectations (apiResponse);
        }

        [Test]
        public void Should_return_400_BadRequest_if_password_is_empty ()
        {
            // Arrange
            var nullCredential
                = new Credential { UserId = "userId", Password = string.Empty };

            // Act
            var apiResponse = base.loginController?.Login (nullCredential);

            // Assert for Login - Bad Request response.
            this.AssertLoginBadRequestExpectations (apiResponse);
        }

        private void AssertLoginBadRequestExpectations (IActionResult? apiResponse)
        {
            // API response
            apiResponse.Should ().NotBeNull ();
            apiResponse.Should ().BeOfType<BadRequestObjectResult> ();

            // API response body
            var responseBody = (apiResponse as BadRequestObjectResult)?.Value as string;

            responseBody.Should ().NotBeNull ();
            responseBody.Should ().Be ("Credentials must be specified.");
        }
    }
}