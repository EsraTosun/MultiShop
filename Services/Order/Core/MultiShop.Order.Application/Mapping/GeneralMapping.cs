using AutoMapper;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;

namespace MultiShop.Order.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // ------------------
            // Address Mappings
            // ------------------

            // Create / Update Command <-> Entity
            CreateMap<Address, CreateAddressCommand>().ReverseMap();
            CreateMap<Address, UpdateAddressCommand>().ReverseMap()
                .ForMember(dest => dest.Detail1, opt => opt.MapFrom(src => src.Detail));
            // UpdateAddressCommand'taki "Detail" property, entity'deki Detail1 ile eşleşir

            // Entity -> Query Results
            CreateMap<Address, GetAddressByIdQueryResult>()
                .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src.Detail1));

            CreateMap<Address, GetAddressQueryResult>()
                .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src.Detail1));

            // ------------------
            // OrderDetail Mappings
            // ------------------

            // Create / Update Command <-> Entity
            CreateMap<OrderDetail, CreateOrderDetailCommand>().ReverseMap();
            CreateMap<OrderDetail, UpdateOrderDetailCommand>().ReverseMap();

            // Entity -> Query Results
            CreateMap<OrderDetail, GetOrderDetailByIdQueryResult>().ReverseMap();
            CreateMap<OrderDetail, GetOrderDetailQueryResult>().ReverseMap();
        }
    }
}
