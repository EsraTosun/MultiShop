using MediatR;
using MultiShop.Order.Application.Features.Addresses.Dtos;
using System.Collections.Generic;

namespace MultiShop.Order.Application.Features.Addresses.Queries.GetAll
{
    public class GetAddressesQuery : IRequest<List<AddressListDto>>
    {
    }
}
