using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CommentServices;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureProductsDefaultComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ICommentService _commentService;

        public _FeatureProductsDefaultComponentPartial(
            IProductService productService,
            ICommentService commentService)
        {
            _productService = productService;
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetAllProductAsync();

            foreach (var item in products)
            {
                item.CommentCount =
                    await _commentService.GetCommentCountByProductId(item.ProductId);
            }

            return View(products);
        }
    }
}
