using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public interface IDiscountService
    {
        Task CreateDiscountCouponAsync(CreateDiscountCouponDto createCouponDto);
        Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateCouponDto);
        Task DeleteDiscountCouponAsync(int couponId);

        Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync();
        Task<ResultDiscountCouponDto?> GetByIdDiscountCouponAsync(int couponId);
        Task<ResultDiscountCouponDto?> GetCodeDetailByCodeAsync(string code);

        Task<int> GetDiscountCouponCountAsync();
        Task<int> GetDiscountCouponRateByCodeAsync(string code);
    }
}
