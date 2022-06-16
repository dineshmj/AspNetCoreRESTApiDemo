using Microsoft.IdentityModel.Tokens;

using RESTApisDemo.Authorization;
using RESTApisDemo.Miscellaneous;
using RESTApisDemo.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RESTApisDemo.Authentication
{
    public sealed class AuthenticationManager
        : IAuthenticationManager
    {
        private static readonly IList<Credential> userRepo = new List<Credential>
            {
                new Credential { FullName = "Admin User", UserId = "admin", Password = "123", Roles = new [] { Role.ADMIN_ROLE } },
                new Credential { FullName = "Regular User", UserId = "regular", Password = "123", Roles = new [] { Role.REGULAR_ROLE } },
            };

        public bool CredentialExists (string? userId, string? password, out Credential? matchingCredential)
        {
            matchingCredential = userRepo.FirstOrDefault (u => u.UserId == userId && u.Password == password);
            return matchingCredential != null;
        }

        public string GenerateEncodedJsonWebToken (Credential? signedInCredential)
        {
            if (null == signedInCredential)
            {
                throw new ArgumentNullException (nameof (signedInCredential));
            }

            var jwtIssuer = TokenParameter.JWT_ISSUER;
            var jwtAudience = TokenParameter.JWT_AUDIENCE;

            var securityKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (TokenParameter.JWT_ENCRYPTION_KEY));
            var signingCredentials = new SigningCredentials (securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new List<Claim>
            {
                new Claim (JwtRegisteredClaimNames.Sub, signedInCredential?.UserId ?? "(unknown user ID)"),
                new Claim (Constants.FULL_NAME_CLAIM, signedInCredential?.FullName ?? "(unknown user name)"),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ())
            };

            signedInCredential
                ?.Roles
                ?.ToList ()
                .ForEach (ur => userClaims.Add (new Claim (Constants.ROLE_NAME_CLAIM, ur)));

            var jsonWebToken = new JwtSecurityToken
                (
                    issuer: jwtIssuer,
                    audience: jwtAudience,
                    claims: userClaims,
                    expires: DateTime.Now.AddMinutes (30),
                    signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler ().WriteToken (jsonWebToken);
        }
    }
}