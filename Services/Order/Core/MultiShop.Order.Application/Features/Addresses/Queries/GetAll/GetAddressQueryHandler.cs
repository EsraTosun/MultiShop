using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Features.Addresses.Dtos;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Addresses.Queries.GetAll
{
    public class GetAddressesQueryHandler : IRequestHandler<GetAddressesQuery, List<AddressListDto>>
    {
        private readonly IRepository<Address> _repository;
        private readonly IMapper _mapper;

        public GetAddressesQueryHandler(IRepository<Address> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AddressListDto>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _repository.GetAllAsync();

            var resultList = _mapper.Map<List<AddressListDto>>(addresses);

            return resultList;
        }
    }
}
