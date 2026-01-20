using Dapper;
using MultiShop.Discount.Context;

namespace MultiShop.Discount.Infrastructure
{
    public class DbInitializer
    {
        private readonly DapperContext _context;

        public DbInitializer(DapperContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            var sql = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Coupons')
                BEGIN
                    CREATE TABLE Coupons (
                        CouponId INT IDENTITY(1,1) PRIMARY KEY,
                        Code NVARCHAR(50) NOT NULL,
                        Rate INT NOT NULL,
                        IsActive BIT NOT NULL,
                        ValidDate DATETIME NOT NULL
                    )
                END";

            using var connection = _context.CreateConnection();
            connection.Execute(sql);
        }
    }
}
