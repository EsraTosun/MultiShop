using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Handlers
{
    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext?
                .Request
                .Cookies["MultiShopJwt"];

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
            // Token yoksa, sadece istek göndermeyi dene
            // veya null/empty olarak bırak
            // API 401 dönerse controller tarafında handle edilir

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
