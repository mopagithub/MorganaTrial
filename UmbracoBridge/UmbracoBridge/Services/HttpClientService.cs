
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http;
using UmbracoBridge.Models.Requests;
using UmbracoBridge.Models.Retrieves;

namespace UmbracoBridge.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private readonly string _clientID;
        private readonly string _clientSecret;
        private readonly string _host;

        public HttpClientService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _clientID = _configuration["Umbraco:ClientId"] ?? throw new ArgumentNullException(nameof(_clientID), "Check ClientId");
            _clientSecret = _configuration["Umbraco:ClientSecret"] ?? throw new ArgumentNullException(nameof(_clientSecret), "Check ClientSecret");
            _host = _configuration["Umbraco:Host"] ?? throw new ArgumentNullException(nameof(_host), "Check Host");
        }

        public async Task<T?> GetRequestAsync<T>(string url)
        {
            bool isAuthenticated = await AuthenticateToken();
            if (!isAuthenticated)
            {
                return default;
            }

            var apiResponse = await _httpClient.GetAsync($"{_host}{url}");
            if (!apiResponse.IsSuccessStatusCode)
            {
                return default;
            }

            var apiUserResponse = await apiResponse.Content.ReadFromJsonAsync<T>();
            if (apiUserResponse == null)
            {
                return default;
            }
            return apiUserResponse;
        }

        public async Task<PostResponse?> SendRequestAsync<T>(string url, DocumentTypeRequest documentTypeRequest)
        {
            bool isAuthenticated = await AuthenticateToken();
            if (!isAuthenticated)
            {
                return default;
            }

            var apiResponse = await _httpClient.PostAsJsonAsync($"{_host}{url}", documentTypeRequest);

            if (!apiResponse.IsSuccessStatusCode)
            {
                return default;
            }


            var status = apiResponse.StatusCode.ToString();
            string generatedId = "";

            if (apiResponse.Headers.Location != null)
            {
                generatedId = apiResponse.Headers.Location.Segments[6].ToString();
            }
            else
            {
                generatedId = "Id not generated";
            }

            PostResponse response = new PostResponse
            {
                status = status,
                idGenerated = generatedId
            };


            return response;
        }

        public async Task<HttpStatusCode> DeleteRequestAsync<T>(string url)
        {
            bool isAuthenticated = await AuthenticateToken();
            if (!isAuthenticated)
            {
                return default;
            }

            var apiResponse = await _httpClient.DeleteAsync($"{_host}{url}");

            if (!apiResponse.IsSuccessStatusCode)
            {
                return default;
            }

            return apiResponse.StatusCode;
        }


        private async Task<bool> AuthenticateToken()
        {
            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = $"{_host}/umbraco/management/api/v1/security/back-office/token",
                ClientId = _clientID,
                ClientSecret = _clientSecret
            });

            if (tokenResponse.IsError || tokenResponse.AccessToken is null)
            {
                return false;
            }

            _httpClient.SetBearerToken(tokenResponse.AccessToken);
            return true;
        }
    }
}
