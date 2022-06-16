using Microsoft.AspNetCore.Mvc;

using FizzWare.NBuilder;
using FluentAssertions;
using Moq;

using RESTApisDemo.Models;

namespace RESTAPIsDemo.Test.Authentication
{
    [TestFixture]
    public sealed class LoginController_OkTest
        : LoginControllerTestBase
    {
        [Test]
        public void Should_return_200_Ok_if_valid_credentials_submitted ()
        {
            // Arrange
            var validCredential = Builder<Credential>.CreateNew ().Build ();
            const string BASE64_ENCODE_JWT = "Base64EncodedJwt";
            const string PAYLOAD_EXPECTED
                = "{ encodedJwt = "
                    + BASE64_ENCODE_JWT
                    + ", userDetails = { FullName = FullName1, UserId = UserId1, Roles =  } }";

            base.authManagerMock
                ?.Setup (am => am.CredentialExists (It.IsAny<string> (), It.IsAny<string> (), out validCredential))
                ?.Returns (true);

            base.authManagerMock
                ?.Setup (am => am.GenerateEncodedJsonWebToken (It.IsAny<Credential> ()))
                ?.Returns (BASE64_ENCODE_JWT);

            // Act
            var apiResponse = this.loginController?.Login (validCredential);

            // Assert expectations.
            apiResponse.Should ().NotBeNull ();
            apiResponse.Should ().BeOfType<OkObjectResult> ();

            // API response body
            var responseBody = (apiResponse as OkObjectResult)?.Value?.ToString ();

            responseBody.Should ().NotBeNull ();
            responseBody.Should ().Be (PAYLOAD_EXPECTED);
        }
    }
}