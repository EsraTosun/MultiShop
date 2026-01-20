using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context;
        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto dto)
        {
            var query = "INSERT INTO Coupons (Code, Rate, IsActive, ValidDate) VALUES (@Code,@Rate,@IsActive,@ValidDate)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, dto);
        }

        public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto dto)
        {
            var query = @"UPDATE Coupons 
                          SET Code=@Code, Rate=@Rate, IsActive=@IsActive, ValidDate=@ValidDate 
                          WHERE CouponId=@CouponId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, dto);
        }

        public async Task DeleteDiscountCouponAsync(int id)
        {
            var query = "DELETE FROM Coupons WHERE CouponId=@CouponId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { CouponId = id });
        }

        public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync()
        {
            var query = "SELECT * FROM Coupons";
            using var connection = _context.CreateConnection();
            var values = await connection.QueryAsync<ResultDiscountCouponDto>(query);
            return values.ToList();
        }

        public async Task<ResultDiscountCouponDto?> GetByIdDiscountCouponAsync(int id)
        {
            var query = "SELECT * FROM Coupons WHERE CouponId=@CouponId";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ResultDiscountCouponDto>(query, new { CouponId = id });
        }

        public async Task<ResultDiscountCouponDto?> GetCodeDetailByCodeAsync(string code)
        {
            var query = "SELECT * FROM Coupons WHERE Code=@Code";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ResultDiscountCouponDto>(query, new { Code = code });
        }

        public async Task<int> GetDiscountCouponCountAsync()
        {
            var query = "SELECT COUNT(*) FROM Coupons";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstAsync<int>(query);
        }

        public async Task<int> GetDiscountCouponRateByCodeAsync(string code)
        {
            var query = "SELECT Rate FROM Coupons WHERE Code=@Code";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<int>(query, new { Code = code });
        }
    }
}
