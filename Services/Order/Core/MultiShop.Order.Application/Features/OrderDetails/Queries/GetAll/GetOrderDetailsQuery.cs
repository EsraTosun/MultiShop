using MediatR;
using MultiShop.Order.Application.Features.OrderDetails.Dtos;
using System.Collections.Generic;

namespace MultiShop.Order.Application.Features.OrderDetails.Queries.GetAll
{
    public class GetOrderDetailsQuery : IRequest<List<OrderDetailDto>>
    {
        // İleride filtreleme parametreleri eklenebilir
    }
}
