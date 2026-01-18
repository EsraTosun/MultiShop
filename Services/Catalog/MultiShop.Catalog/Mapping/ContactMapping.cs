using AutoMapper;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Entites;

namespace MultiShop.Catalog.Mapping
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<Contact, ResultContactDto>();
            CreateMap<Contact, GetByIdContactDto>();

            CreateMap<CreateContactDto, Contact>();
            CreateMap<UpdateContactDto, Contact>();
        }
    }
}
