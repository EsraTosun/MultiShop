using MultiShop.Order.Application.Features.Orderings.Dtos;

namespace MultiShop.Order.Application.Features.OrderDetails.Dtos
{
    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductAmount { get; set; }
        public decimal ProductTotalPrice { get; set; }
        public int OrderingId { get; set; }

        // Eğer ordering bilgisi de gerekli ise nested DTO:
        public OrderingDto Ordering { get; set; }
    }
}
