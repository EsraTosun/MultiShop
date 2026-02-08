using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Basket.Dtos;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services;

namespace MultiShop.Basket.Controllers
{
    [ApiController]
    [Route("api/baskets")]
    [Authorize]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ILoginService _loginService;

        public BasketsController(
            IBasketService basketService,
            ILoginService loginService)
        {
            _basketService = basketService;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _loginService.GetUserId;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var basket = await _basketService.GetBasket(userId);
            return basket == null ? NoContent() : Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> Save(BasketTotalDto dto)
        {
            var userId = _loginService.GetUserId;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            dto.UserId = userId;
            await _basketService.SaveBasket(dto);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var userId = _loginService.GetUserId;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var deleted = await _basketService.DeleteBasket(userId);
            return deleted ? Ok() : NoContent();
        }
    }
}
