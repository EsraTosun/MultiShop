using Microsoft.Extensions.Options;
using MultiShop.WebUI.Settings;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MultiShop.WebUI.Handlers
{
    public class ClientCredentialTokenHandler : DelegatingHandler
    {
        private readonly HttpClient _httpClient;
        private readonly ClientSettings _clientSettings;
        private readonly ServiceApiSettings _serviceApiSettings;

        public ClientCredentialTokenHandler(
            HttpClient httpClient,
            IOptions<ClientSettings> clientSettings,
            IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            _clientSettings = clientSettings.Value;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Burada hangi client kullanılacağını belirliyoruz
            var clientId = _clientSettings.MultiShopVisitorClient.ClientId;
            var clientSecret = _clientSettings.MultiShopVisitorClient.ClientSecret;

            // İleride farklı client için parametre geçilebilir
            var token = await GetTokenAsync(clientId, clientSecret);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> GetTokenAsync(string clientId, string clientSecret)
        {
            var response = await _httpClient.PostAsync(
                $"{_serviceApiSettings.IdentityServerUrl}/connect/token",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["client_id"] = clientId,
                    ["client_secret"] = clientSecret,
                    ["grant_type"] = "client_credentials"
                }));

            response.EnsureSuccessStatusCode(); // 401 ve 400 hatalarını yakalar

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);

            if (!doc.RootElement.TryGetProperty("access_token", out var tokenElement))
                throw new Exception("IdentityServer'dan access_token alınamadı");

            return tokenElement.GetString()!;
        }
    }
}
