using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.OrderDetails.Commands.Update
{
    public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand>
    {
        private readonly IRepository<OrderDetail> _repository;
        private readonly IMapper _mapper;

        public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(
            UpdateOrderDetailCommand request,
            CancellationToken cancellationToken)
        {
            var orderDetail = await _repository.GetByIdAsync(request.OrderDetailId);

            if (orderDetail == null)
                throw new Exception("orderDetail not found");

            _mapper.Map(request, orderDetail);

            await _repository.UpdateAsync(orderDetail);
        }
    }
}
