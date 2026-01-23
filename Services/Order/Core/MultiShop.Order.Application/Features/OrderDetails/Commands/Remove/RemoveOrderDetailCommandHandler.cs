using MediatR;
using MultiShop.Order.Application.Features.Addresses.Commands.Remove;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System.Net;


namespace MultiShop.Order.Application.Features.OrderDetails.Commands.Update
{
    public class RemoveOrderDetailCommandHandler
        : IRequestHandler<RemoveOrderDetailCommand>
    {
        private readonly IRepository<OrderDetail> _repository;
        public RemoveOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            RemoveOrderDetailCommand request,
            CancellationToken cancellationToken)
        {
            var orderDetail = await _repository.GetByIdAsync(request.Id);
            if (orderDetail == null)
                throw new Exception("OrderDetail not found");

            await _repository.DeleteAsync(orderDetail);
        }
    }
}