using MediatR;
using MultiShop.Order.Application.Features.OrderDetails.Dtos;

namespace MultiShop.Order.Application.Features.OrderDetails.Queries.GetById
{
    public class GetOrderDetailByIdQuery : IRequest<OrderDetailDto>
    {
        public int OrderDetailId { get; set; }

        public GetOrderDetailByIdQuery(int orderDetailId)
        {
            OrderDetailId = orderDetailId;
        }
    }
}
