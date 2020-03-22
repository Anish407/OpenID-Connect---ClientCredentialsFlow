using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Sample.ClientApi.Controllers
{
    public class SampleController : Controller
    {
        public SampleController(IHttpClientFactory httpClientFactory)
          => (_httpClientFactory) = (httpClientFactory);

        public IHttpClientFactory _httpClientFactory { get; }


        [Route("/")]
        public async Task<IActionResult> getSecret()
        {
            using var client = _httpClientFactory.CreateClient();

            // we call the discovery endpoint and get the url for the token endpoints
            // response contains the access token as we have used the client credentials flow
            var discoveryDoc = await client.GetDiscoveryDocumentAsync("https://localhost:44317/");

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = discoveryDoc.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "client_secret",
                Scope = "ApiOne"
            });


            // we set the token and call ApiOne
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var resp = await apiClient.GetAsync("https://localhost:44345/WeatherForecast/GetWeather");
            var result = await resp.Content.ReadAsStringAsync();
            return Ok(new
            {
                accessToken = tokenResponse.AccessToken,
                message = result
            });
        }
    }
}
