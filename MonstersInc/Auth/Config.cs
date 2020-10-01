using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MonstersInc.Auth
{
    public class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
       new IdentityResource[]
       {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
       };

        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
            new ApiResource("api1", "My API")
            };

        //public static IEnumerable<Client> Clients =>
        //    new List<Client>
        //    {
        //    new Client {
        //        ClientId = "ropg_client",
        //        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
        //        ClientSecrets = {
        //            new Secret("secret".Sha256())
        //        },
        //        AllowedScopes = { "api1", StandardScopes.OpenId }
        //    },
        //    new Client
        //    {
        //        ClientId = "simple_client",
                
        //        // no interactive user
        //        // use the clientid/secret for authentication
        //        AllowedGrantTypes = GrantTypes.ClientCredentials,
                
        //        // secret for authentication
        //        ClientSecrets =
        //        {
        //            new Secret("secret".Sha256())
        //        },
                
        //        // scopes that client has access to
        //        AllowedScopes = { "api1" }
        //    }
        //    };

    }
}
