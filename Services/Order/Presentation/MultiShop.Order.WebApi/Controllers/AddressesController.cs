using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.Addresses.Commands.Create;
using MultiShop.Order.Application.Features.Addresses.Queries.GetById;
using MultiShop.Order.Application.Features.Addresses.Commands.Update;
using MultiShop.Order.Application.Features.Addresses.Commands.Remove;
using MultiShop.Order.Application.Features.Addresses.Queries.GetAll;

namespace MultiShop.Order.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> AddressList()
        {
            var values = await _mediator.Send(new GetAddressesQuery());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var values = await _mediator.Send(new GetAddressByIdQuery(id));
            if (values == null) return NotFound();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressCommand command)
        {
            await _mediator.Send(command);
            return Ok("Adres bilgisi başarıyla eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress(UpdateAddressCommand command)
        {
            await _mediator.Send(command);
            return Ok("Adres bilgisi başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAddress(int id)
        {
            await _mediator.Send(new RemoveAddressCommand(id));
            return Ok("Adres başarıyla silindi");
        }
    }
}
