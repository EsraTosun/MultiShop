using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Addresses.Commands.Create
{
    public class CreateAddressCommandHandler
        : IRequestHandler<CreateAddressCommand>
    {
        private readonly IRepository<Address> _repository;
        private readonly IMapper _mapper;

        public CreateAddressCommandHandler(
            IRepository<Address> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(
            CreateAddressCommand request,
            CancellationToken cancellationToken)
        {
            var address = _mapper.Map<Address>(request);
            await _repository.CreateAsync(address);
        }
    }
}
