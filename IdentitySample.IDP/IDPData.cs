using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.IDP
{
    public static class IDPData
    {

        public static IEnumerable<ApiResource> GetApiResources =>
            new List<ApiResource> {
                new ApiResource("ApiOne"),
                 new ApiResource("ApiTwo")
            };

        public static IEnumerable<IdentityResource> GetIdentityResources => new List<IdentityResource> {
          new IdentityResources.OpenId(),
          new IdentityResources.Profile()

        };

        public static IEnumerable<Client> GetClients =>
                new List<Client> {
                    new Client
                    {
                        // this clients needs access to APIONE..which is a API REsource
                        //We create a client app with audience set to ClientApi and an API app with audience set to ApiOne
                         ClientId="client_id",
                         ClientSecrets= { new Secret("client_secret".ToSha256()) },
                         AllowedGrantTypes=  GrantTypes.ClientCredentials,
                         AllowedScopes= { "ApiOne" },
                         ClientName="ClientApi"
                    },

                    new Client
                    {
                        // this clients needs access to APIONE..which is a API REsource
                        //We create a client app with audience set to ClientApi and an API app with audience set to ApiOne
                         ClientId="client_id_mvc",
                         ClientSecrets= { new Secret("client_secret_mvc".ToSha256()) },
                         AllowedGrantTypes=  GrantTypes.Code,
                         AllowedScopes= {
                            "ApiOne",
                            "ApiTwo",
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile, 
                        },
                         ClientName="ClientApi2",
                         RedirectUris={
                                     "https://localhost:44316/signin-oidc"
                            },
                         PostLogoutRedirectUris={ "https://localhost:44316/signout-callback-oidc" },

                    }
            };
    }
}
