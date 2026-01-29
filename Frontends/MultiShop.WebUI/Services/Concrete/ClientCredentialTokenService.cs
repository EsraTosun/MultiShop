using Microsoft.Extensions.Options;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;
using System.Text.Json;

namespace MultiShop.WebUI.Services.Concrete
{
    public sealed class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceApiSettings _apiSettings;
        private readonly ClientSettings _clientSettings;

        private string? _cachedToken;
        private DateTime _expiresAt;

        public ClientCredentialTokenService(
            HttpClient httpClient,
            IOptions<ServiceApiSettings> apiSettings,
            IOptions<ClientSettings> clientSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _clientSettings = clientSettings.Value;
        }

        public async Task<string> GetToken()
        {
            if (_cachedToken != null && DateTime.UtcNow < _expiresAt)
                return _cachedToken;

            var tokenEndpoint = $"{_apiSettings.IdentityServerUrl}/connect/token";

            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _clientSettings.MultiShopVisitorClient.ClientId,
                ["client_secret"] = _clientSettings.MultiShopVisitorClient.ClientSecret,
                ["scope"] = "Catalog.Read Image.Full Comment.Full Ocelot.Full"
            };

            var response = await _httpClient.PostAsync(
                tokenEndpoint,
                new FormUrlEncodedContent(form));

            if (!response.IsSuccessStatusCode)
                throw new Exception("IdentityServer token üretmedi");

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            _cachedToken = doc.RootElement.GetProperty("access_token").GetString();
            var expiresIn = doc.RootElement.GetProperty("expires_in").GetInt32();

            _expiresAt = DateTime.UtcNow.AddSeconds(expiresIn - 60);

            return _cachedToken!;
        }
    }
}
