using Microsoft.AspNetCore.Authorization;

namespace RESTApisDemo.Authorization
{
    public static class PolicyHub
    {
        public const string ADMIN_POLICY = "Admin Policy";
        public const string REGULAR_POLICY = "Regular Policy";

        public static AuthorizationPolicy GetAdminPolicy()
        {
            // "Admin" policy.
            return new AuthorizationPolicyBuilder ()
                .RequireAuthenticatedUser ()
                .RequireRole (Role.ADMIN_ROLE)
                .Build ();
        }

        public static AuthorizationPolicy GetRegularPolicy ()
        {
            // "Regular" policy.
            return new AuthorizationPolicyBuilder ()
                .RequireAuthenticatedUser ()
                .RequireRole (Role.REGULAR_ROLE)
                .Build ();
        }
    }
}