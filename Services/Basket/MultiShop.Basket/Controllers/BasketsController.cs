using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Basket.Dtos;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services;

namespace MultiShop.Basket.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetMyBasketDetail()
        {
            var basket = await _basketService.GetBasket(_loginService.GetUserId);

            if (basket == null)
                return NoContent();

            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMyBasket(BasketTotalDto basketTotalDto)
        {
            basketTotalDto.UserId = _loginService.GetUserId;
            await _basketService.SaveBasket(basketTotalDto);
            return Ok("Sepetteki değişiklikler kaydedili");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            var deleted = await _basketService.DeleteBasket(_loginService.GetUserId);

            if (!deleted)
                return NoContent();

            return Ok("Sepet başarıyla silindi");
        }

    }
}
