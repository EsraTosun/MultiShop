using MultiShop.WebUI.Services.Interfaces;

namespace MultiShop.WebUI.Handlers
{
    public class RefreshTokenHandler : DelegatingHandler
    {
        private readonly IIdentityService _identityService;

        public RefreshTokenHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var refreshed = await _identityService.GetRefreshToken();
                if (refreshed)
                {
                    response.Dispose();
                    return await base.SendAsync(request, cancellationToken);
                }
            }

            return response;
        }
    }
}
