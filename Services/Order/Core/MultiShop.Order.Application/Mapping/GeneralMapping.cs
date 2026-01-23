using AutoMapper;
using MultiShop.Order.Application.Features.Addresses.Commands.Create;
using MultiShop.Order.Application.Features.Addresses.Commands.Update;
using MultiShop.Order.Application.Features.Addresses.Dtos;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Features.OrderDetails.Commands.Create;
using MultiShop.Order.Application.Features.OrderDetails.Commands.Update;
using MultiShop.Order.Application.Features.OrderDetails.Dtos;
using MultiShop.Order.Application.Features.Orderings.Dtos;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // ====== OrderDetails Mapping ======
            CreateMap<OrderDetail, OrderDetailDto>()
                .ReverseMap(); // DTO → Entity dönüşümü için

            // Eğer nested Ordering DTO kullanıyorsan
            CreateMap<Ordering, OrderingDto>()
                .ReverseMap();

            // ====== Addresses Mapping ======
            CreateMap<Domain.Entities.Address, AddressDetailDto>()
                .ReverseMap();

            // ====== Command Mapping ======
            CreateMap<CreateOrderDetailCommand, OrderDetail>();
            CreateMap<UpdateOrderDetailCommand, OrderDetail>();

            CreateMap<CreateAddressCommand, Address>();
            CreateMap<UpdateAddressCommand, Address>();

            CreateMap<CreateOrderingCommand, Ordering>();
            CreateMap<UpdateOrderingCommand, Ordering>();

            CreateMap<Address, AddressListDto>().ReverseMap();
            CreateMap<Address, AddressDetailDto>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<Ordering, OrderingDto>().ReverseMap();
            CreateMap<Ordering, OrderingDetailDto>().ReverseMap();
        }
    }
}
