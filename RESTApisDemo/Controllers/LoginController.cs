using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RESTApisDemo.Authentication;
using RESTApisDemo.Models;

namespace RESTApisDemo.Controllers
{
    [Route ("api/[controller]")]
    public sealed class LoginController
        : ControllerBase
    {
        private readonly IAuthenticationManager credentialsManager;

        public LoginController (IAuthenticationManager credentialsManager)
        {
            this.credentialsManager = credentialsManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Credential? credential)
        {
            if
                (
                    null == credential
                    || string.IsNullOrEmpty (credential?.UserId)
                    || string.IsNullOrEmpty (credential?.Password)
                )
            {
                return base.BadRequest ("Credentials must be specified.");
            }

            // Does user exist?
            if (this.credentialsManager.CredentialExists (credential?.UserId, credential?.Password, out Credential? matchingCredential) == false)
            {
                return base.Unauthorized ();        // 401 - Unauthorized.
            }

            // Yes. Generate Base 64 enocded JWT.
            var encodedJwt = this.credentialsManager.GenerateEncodedJsonWebToken (matchingCredential);

            return
                base.Ok
                (
                    new
                    {
                        encodedJwt,                         // JWT
                        userDetails = new
                        {
                            matchingCredential?.FullName,
                            matchingCredential?.UserId,
                            matchingCredential?.Roles       // List of roles.
                        }
                    }
                );
        }
    }
}