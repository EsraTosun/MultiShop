using MediatR;
using MultiShop.Order.Application.Features.Addresses.Dtos;

namespace MultiShop.Order.Application.Features.Addresses.Queries.GetById
{
    public class GetAddressByIdQuery : IRequest<AddressDetailDto>
    {
        public int Id { get; set; }

        public GetAddressByIdQuery(int id)
        {
            Id = id;
        }
    }
}
