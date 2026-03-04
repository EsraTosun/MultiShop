using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApiWebUI.Models;
using MultiShop.RapidApiWebUI.Services.ProductServices;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ProductSearchViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProductSearchViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Query))
            {
                model = await _productService.SearchProductsAsync(model);
            }

            return View(model);
        }
    }
}
