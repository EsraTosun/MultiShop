using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.DiscountServices;

namespace MultiShop.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        public ShoppingCartController(IProductService productService, IBasketService basketService)
        {
            _productService = productService;
            _basketService = basketService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Sepetim";

            var basket = await _basketService.GetBasket();

            if (basket == null)
                return View(new List<BasketItemDto>());

            ViewBag.total = basket.TotalPrice;

            var tax = basket.TotalPrice * 0.10M;
            var totalPriceWithTax = basket.TotalPrice + tax;

            ViewBag.tax = tax;
            ViewBag.totalPriceWithTax = totalPriceWithTax;

            return View(basket.BasketItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBasketItem(string productId, int? quantity)
        {
            int finalQuantity = quantity ?? 1;

            var product = await _productService.GetByIdProductAsync(productId);

            if (product == null)
                return RedirectToAction("Index");

            var item = new BasketItemDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.ProductPrice,
                Quantity = finalQuantity,
                ProductImageUrl = product.ProductImageUrl,
            };

            var success = await _basketService.AddBasketItem(item);

            TempData["BasketSuccess"] = success
                ? "Ürün sepete eklendi."
                : "Ürün sepete eklenemedi.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity([FromBody] BasketUpdateDto model)
        {
            await _basketService.UpdateBasketItemQuantity(model.ProductId, model.Quantity);
            return Ok();
        }

        public async Task<IActionResult> RemoveBasketItem(string id)
        {
            await _basketService.RemoveBasketItem(id);
            return RedirectToAction("Index");
        }
    }
}
