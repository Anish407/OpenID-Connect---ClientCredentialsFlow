using IdentityModel;
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
            new List<ApiResource> { new ApiResource("ApiOne") };

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
                    }
            };
    }
}
