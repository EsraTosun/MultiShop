using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Features.OrderDetails.Dtos;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.OrderDetails.Queries.GetAll
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, List<OrderDetailDto>>
    {
        private readonly IRepository<OrderDetail> _repository;
        private readonly IMapper _mapper;

        public GetOrderDetailsQueryHandler(IRepository<OrderDetail> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrderDetailDto>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var orderDetails = await _repository.GetAllAsync();

            var result = _mapper.Map<List<OrderDetailDto>>(orderDetails);

            return result;
        }
    }
}
