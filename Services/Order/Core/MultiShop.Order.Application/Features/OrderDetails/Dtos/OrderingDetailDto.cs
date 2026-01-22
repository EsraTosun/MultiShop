
namespace MultiShop.Order.Application.Features.OrderDetails.Dtos
{
    public class OrderingDetailDto
    {
        public int OrderingId { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
