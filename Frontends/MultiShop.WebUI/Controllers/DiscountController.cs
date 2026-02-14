using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.DiscountServices;

namespace MultiShop.WebUI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IBasketService _basketService;
        public DiscountController(IDiscountService discountService, IBasketService basketService)
        {
            _discountService = discountService;
            _basketService = basketService;
        }

        [HttpGet]
        public PartialViewResult ConfirmDiscountCoupon()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDiscountCoupon(string? CouponCode)
        {
            if (string.IsNullOrWhiteSpace(CouponCode))
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            var discountRate = await _discountService.GetDiscountCouponCountRate(CouponCode);

            if (discountRate <= 0)
            {
                TempData["CouponError"] = "Kupon bulunamadı veya geçersiz.";
                return RedirectToAction("Index", "ShoppingCart");
            }

            var basket = await _basketService.GetBasket();
            var totalWithTax = basket.TotalPrice * 1.10m;
            var discountedTotal = totalWithTax - (totalWithTax * discountRate / 100);

            TempData["CouponSuccess"] = $"Kupon uygulandı! %{discountRate} indirim kazandınız 🎉";
            TempData["DiscountedTotal"] = discountedTotal;
            TempData["DiscountRate"] = discountRate;
            TempData["CouponCode"] = CouponCode;

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
