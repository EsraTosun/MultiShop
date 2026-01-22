using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Addresses.Commands.Update
{
    public class UpdateAddressCommandHandler
        : IRequestHandler<UpdateAddressCommand>
    {
        private readonly IRepository<Address> _repository;
        private readonly IMapper _mapper;

        public UpdateAddressCommandHandler(
            IRepository<Address> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(
            UpdateAddressCommand request,
            CancellationToken cancellationToken)
        {
            var address = await _repository.GetByIdAsync(request.AddressId);

            if (address == null)
                throw new Exception("Address not found");

            _mapper.Map(request, address);

            await _repository.UpdateAsync(address);
        }
    }
}
