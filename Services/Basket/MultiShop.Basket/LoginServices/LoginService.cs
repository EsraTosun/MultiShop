using System.Security.Claims;

namespace MultiShop.Basket.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _context;

        public LoginService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUserId
        {
            get
            {
                var user = _context.HttpContext?.User;

                if (user == null || !user.Identity!.IsAuthenticated)
                    return string.Empty;

                return user.Claims
                    .FirstOrDefault(c => c.Type == "sub")?.Value
                    ?? string.Empty;
            }
        }
    }
}
