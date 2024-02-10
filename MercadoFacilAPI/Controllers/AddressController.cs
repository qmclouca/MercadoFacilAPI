using AutoMapper;
using Domain.DTOs.Address;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercadoFacilAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressController(IAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetAddress")]
        public async Task<IActionResult> Get(Guid id)
        {
            var address = await _addressService.GetAddressById(id);
            if (address == null)
                return NotFound();
            return Ok(address);
        }

        [HttpGet(Name = "GetAllAddresses")]
        public async Task<IActionResult> GetAll()
        {
            var addresses = await _addressService.GetAllAddresses();
            if (addresses == null)
                return NotFound();
            return Ok(addresses);
        }        

        [HttpPut("{id}", Name = "UpdateAddress")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateAddressDTO addressDto)
        {
            if (addressDto == null)
                return BadRequest();

            var address = await _addressService.GetAddressById(id);

            if (address == null)
                return NotFound();

            _mapper.Map(addressDto, address);

            await _addressService.UpdateAddress(address);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteAddress")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var address = await _addressService.GetAddressById(id);

            if (address == null)
                return NotFound();

            await _addressService.DeleteAddress(address);

            return NoContent();
        }   
    }
}