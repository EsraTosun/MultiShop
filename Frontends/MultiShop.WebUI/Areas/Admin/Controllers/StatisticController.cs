using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly ICatalogStatisticService _catalogStatisticService;
        private readonly IUserStatisticService _userStatisticService;
        private readonly ICommentService _commentService;
        private readonly IDiscountStatisticService _discountStatisticService;
        private readonly IMessageStatisticService _messageStatisticService;
        public StatisticController(ICatalogStatisticService catalogStatisticService, IUserStatisticService userStatisticService, ICommentService commentService, IDiscountStatisticService discountStatisticService, IMessageStatisticService messageStatisticService)
        {
            _catalogStatisticService = catalogStatisticService;
            _userStatisticService = userStatisticService;
            _commentService = commentService;
            _discountStatisticService = discountStatisticService;
            _messageStatisticService = messageStatisticService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new AdminStatisticViewModel
            {
                BrandCount = await _catalogStatisticService.GetBrandCount(),
                ProductCount = await _catalogStatisticService.GetProductCount(),
                CategoryCount = await _catalogStatisticService.GetCategoryCount(),

                MaxPriceProductName = await _catalogStatisticService.GetMaxPriceProductName(),
                MinPriceProductName = await _catalogStatisticService.GetMinPriceProductName(),

                UserCount = await _userStatisticService.GetUsercount(),

                TotalCommentCount = await _commentService.GetTotalCommentCount(),
                ActiveCommentCount = await _commentService.GetActiveCommentCount(),
                PassiveCommentCount = await _commentService.GetPAssiveCommentCount(),

                DiscountCouponCount = await _discountStatisticService.GetDiscountCouponCount(),
                MessageTotalCount = await _messageStatisticService.GetTotalMessageCount()
            };

            return View(model);
        }
    }
}
