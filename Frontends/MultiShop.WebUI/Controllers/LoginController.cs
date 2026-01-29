using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.Interfaces;

namespace MultiShop.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IIdentityService _identityService;

        public LoginController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SignInDto signInDto)
        {
            if (!ModelState.IsValid)
                return View(signInDto);

            var result = await _identityService.SignIn(signInDto);

            if (!result)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                return View(signInDto);
            }

            return RedirectToAction("Index", "User");
        }
    }
}
