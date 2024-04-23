using IdentityServer4.Models;
using IdentityServer4.Test;

namespace MembersIdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new() {
                    ClientId = "memberClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "MemberAPI"}
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new("MemberAPI", "Member API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
           new ApiResource[]
           {
           };

        public static IEnumerable<IdentityResource> IdentityResources =>
           new IdentityResource[]
           {
           };

        public static List<TestUser> TestUsers =>
          new List<TestUser>
          {
          };
    }
}
