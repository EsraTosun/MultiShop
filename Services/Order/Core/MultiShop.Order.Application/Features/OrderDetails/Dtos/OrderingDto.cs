using MultiShop.Order.Application.Features.OrderDetails.Dtos;

namespace MultiShop.Order.Application.Features.Orderings.Dtos
{
    public class OrderingDto
    {
        public int OrderingId { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        // Eğer detayları da taşımak istersek:
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
