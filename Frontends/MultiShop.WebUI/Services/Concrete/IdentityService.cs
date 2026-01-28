using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;
using System.Security.Claims;
using System.Text.Json;

namespace MultiShop.WebUI.Services.Concrete
{
    public sealed class IdentityService : IIdentityService
    {
        private const string AccessTokenName = "access_token";
        private const string RefreshTokenName = "refresh_token";
        private const string ExpiresInName = "expires_in";

        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientSettings _clientSettings;
        private readonly ServiceApiSettings _apiSettings;

        public IdentityService(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            IOptions<ClientSettings> clientSettings,
            IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _clientSettings = clientSettings.Value;
            _apiSettings = serviceApiSettings.Value;
        }

        public async Task<bool> GetRefreshToken()
        {
            var refreshToken = await _httpContextAccessor.HttpContext!
                .GetTokenAsync(RefreshTokenName);

            if (string.IsNullOrEmpty(refreshToken))
                return false;

            var tokenEndpoint = $"{_apiSettings.IdentityServerUrl}/connect/token";

            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = _clientSettings.MultiShopManagerClient.ClientId,
                ["client_secret"] = _clientSettings.MultiShopManagerClient.ClientSecret,
                ["refresh_token"] = refreshToken
            };

            var response = await _httpClient.PostAsync(
                tokenEndpoint,
                new FormUrlEncodedContent(form));

            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var accessToken = doc.RootElement.GetProperty(AccessTokenName).GetString();
            var newRefreshToken = doc.RootElement.GetProperty(RefreshTokenName).GetString();
            var expiresIn = doc.RootElement.GetProperty(ExpiresInName).GetInt32();

            var authResult = await _httpContextAccessor.HttpContext.AuthenticateAsync();
            var props = authResult.Properties;

            props.StoreTokens(new[]
            {
                new AuthenticationToken { Name = AccessTokenName, Value = accessToken },
                new AuthenticationToken { Name = RefreshTokenName, Value = newRefreshToken },
                new AuthenticationToken
                {
                    Name = ExpiresInName,
                    Value = DateTime.UtcNow.AddSeconds(expiresIn).ToString("o")
                }
            });

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                authResult.Principal!,
                props);

            return true;
        }

        public async Task<bool> SignIn(SignInDto signInDto)
        {
            var tokenEndpoint = $"{_apiSettings.IdentityServerUrl}/connect/token";

            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["client_id"] = _clientSettings.MultiShopManagerClient.ClientId,
                ["client_secret"] = _clientSettings.MultiShopManagerClient.ClientSecret,
                ["username"] = signInDto.Username,
                ["password"] = signInDto.Password,
                ["scope"] = "openid profile email"
            };

            var response = await _httpClient.PostAsync(
                tokenEndpoint,
                new FormUrlEncodedContent(form));

            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var accessToken = doc.RootElement.GetProperty(AccessTokenName).GetString();
            var refreshToken = doc.RootElement.GetProperty(RefreshTokenName).GetString();
            var expiresIn = doc.RootElement.GetProperty(ExpiresInName).GetInt32();

            // USERINFO
            var userInfoRequest = new HttpRequestMessage(
                HttpMethod.Get,
                $"{_apiSettings.IdentityServerUrl}/connect/userinfo");

            userInfoRequest.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var userInfoResponse = await _httpClient.SendAsync(userInfoRequest);
            var userInfoJson = await userInfoResponse.Content.ReadAsStringAsync();

            using var userDoc = JsonDocument.Parse(userInfoJson);

            var claims = userDoc.RootElement
                .EnumerateObject()
                .Select(p => new Claim(p.Name, p.Value.ToString()))
                .ToList();

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                "name",
                "role");

            var principal = new ClaimsPrincipal(identity);

            var props = new AuthenticationProperties { IsPersistent = false };

            props.StoreTokens(new[]
            {
                new AuthenticationToken { Name = AccessTokenName, Value = accessToken },
                new AuthenticationToken { Name = RefreshTokenName, Value = refreshToken },
                new AuthenticationToken
                {
                    Name = ExpiresInName,
                    Value = DateTime.UtcNow.AddSeconds(expiresIn).ToString("o")
                }
            });

            await _httpContextAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                props);

            return true;
        }
    }
}
