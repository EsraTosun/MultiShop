using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.BasketServices;

namespace MultiShop.WebUI.ViewComponents.ShoppingCartViewComponents
{
    public class _ShoppingCartDiscountCouponComponentPartial : ViewComponent
    {
        private readonly IBasketService _basketService;

        public _ShoppingCartDiscountCouponComponentPartial(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basket = await _basketService.GetBasket();
            var total = basket.BasketItems?.Sum(x => x.Price * x.Quantity) ?? 0;
            var tax = total * 0.10M;
            var totalWithTax = total + tax;

            var model = new DiscountSummaryViewModel
            {
                SubTotal = total,
                Tax = tax,
                Total = totalWithTax
            };

            return View(model);
        }
    }
}
