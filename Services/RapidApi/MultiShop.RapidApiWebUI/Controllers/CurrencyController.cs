using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApiWebUI.Services.FinanceServices;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly IFinanceService _financeService;

        public CurrencyController(IFinanceService financeService)
        {
            _financeService = financeService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _financeService.GetExchangeRateAsync("USD", "TRY");
            return View(model);
        }
    }
}
