using AutoMapper;
using Domain.DTOs.User;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MercadoFacilAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IUserAddressService _userAddressService;
        private readonly IMapper _mapper;        

        public UserController(
            IUserService userService, 
            IAddressService addressService, 
            IUserAddressService userAddressService, 
            IMapper mapper)
        {
            _userService = userService;
            _addressService = addressService;
            _userAddressService = userAddressService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet(Name = "GetAllUsers")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsers();
            if (users == null)
                return NotFound();
            return Ok(users);
        }

        [HttpPost(Name = "AddUser")]
        public async Task<IActionResult> Post([FromBody] CreateUserDTO userDto)
        {            

            if (userDto == null)
                return BadRequest();

            User user = new User();
            List<Address> lstAddress = new List<Address>();
            List<UserAddress> lstUserAddress = new List<UserAddress>();

            _mapper.Map(userDto, user);

            foreach (var item in userDto.Addresses)
            {
                Address addressItem = new Address();
                UserAddress userAddress = new UserAddress();

                addressItem.Id = Guid.NewGuid();
                addressItem.IsDeleted = false;
                addressItem.Active = true;
                addressItem.Number = item.Number;
                addressItem.Street = item.Street;
                addressItem.Complement = item.Complement;
                addressItem.Neighborhood = item.Neighborhood;
                addressItem.City = item.City;
                addressItem.State = item.State;
                addressItem.Country = item.Country;
                addressItem.ZipCode = item.ZipCode;
                addressItem.District = item.District;
                userAddress.Id = Guid.NewGuid();
                userAddress.UserId = user.Id;
                userAddress.AddressId = addressItem.Id;
                userAddress.Active = true;
                userAddress.IsDeleted = false;
                _addressService.AddAddress(addressItem);
                _userAddressService.AddUserAddress(userAddress);
            }

            user.UserAddresses = lstUserAddress;            

            await _userService.AddUser(user);
            foreach (var item in lstAddress)
            {
                await _addressService.AddAddress(item);
            }            

            return Ok(user);
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            await _userService.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete(Name = "DeleteUser")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            // await _userService.DeleteUser(user);
            return Ok(user);
        }
    }
}
