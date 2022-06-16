using RESTApisDemo.Models;

namespace RESTApisDemo.Authentication
{
    public interface IAuthenticationManager
    {
        bool CredentialExists (string? userId, string? password, out Credential? matchingCredential);
        string GenerateEncodedJsonWebToken (Credential? signedInCredential);
    }
}