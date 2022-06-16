using Microsoft.AspNetCore.Mvc;

using FizzWare.NBuilder;
using FluentAssertions;
using Moq;

using RESTApisDemo.Models;

namespace RESTAPIsDemo.Test.Authentication
{
    [TestFixture]
    public sealed class LoginController_UnauthorizedTest
        : LoginControllerTestBase
    {
        [Test]
        public void Should_return_401_Unauthorized_if_null_invalid_credentials_submitted ()
        {
            // Arrange
            var validCredential = Builder<Credential>.CreateNew ().Build ();

            base.authManagerMock
                ?.Setup (am => am.CredentialExists (It.IsAny<string> (), It.IsAny<string> (), out validCredential))
                ?.Returns (false);

            // Act
            var apiResponse = this.loginController?.Login (validCredential);

            // Assert expectations.
            apiResponse.Should ().NotBeNull ();
            apiResponse.Should ().BeOfType<UnauthorizedResult> ();
        }
    }
}