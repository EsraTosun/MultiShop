using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers
{
    //[Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> DiscountCouponList()
        {
            var values = await _discountService.GetAllDiscountCouponAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountCouponById(int id)
        {
            var value = await _discountService.GetByIdDiscountCouponAsync(id);
            if (value == null) return NotFound("Kupon bulunamadı");
            return Ok(value);
        }

        [HttpGet("GetCodeDetailByCodeAsync")]
        public async Task<IActionResult> GetCodeDetailByCodeAsync(string code)
        {
            var value = await _discountService.GetCodeDetailByCodeAsync(code);
            if (value == null) return NotFound("Kupon kodu bulunamadı");
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createCouponDto)
        {
            await _discountService.CreateDiscountCouponAsync(createCouponDto);
            return Ok("Kupon başarıyla oluşturuldu");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountCoupon(int id)
        {
            await _discountService.DeleteDiscountCouponAsync(id);
            return Ok("Kupon başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateCouponDto)
        {
            await _discountService.UpdateDiscountCouponAsync(updateCouponDto);
            return Ok("İndirim kuponu başarıyla güncellendi");
        }

        [HttpGet("GetDiscountCouponRateByCodeAsync")]
        public async Task<IActionResult> GetDiscountCouponRateByCodeAsync(string code)
        {
            var rate = await _discountService.GetDiscountCouponRateByCodeAsync(code);
            return Ok(rate);
        }

        [HttpGet("GetDiscountCouponCountAsync")]
        public async Task<IActionResult> GetDiscountCouponCountAsync()
        {
            var count = await _discountService.GetDiscountCouponCountAsync();
            return Ok(count);
        }
    }
}
