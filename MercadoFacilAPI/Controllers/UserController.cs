using AutoMapper;
using Domain.DTOs.Address;
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
           
            if (!IsUserDtoValid(userDto))
                return BadRequest();
            User user = await ConvertCreateUserDTOToUser(userDto);

            await _userService.AddUser(user);
            return Ok(ConvertUserToCreateUserDTO(user));
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
            bool addressListExclusionCheck = await _addressService.DeleteAddressByUserId(user.Id);
            if (!addressListExclusionCheck)
                return BadRequest("Um problema ocorreu ao excluir os endereços do usuário.");
            await _userService.DeleteUser(user);
            
            return Ok(user);
        }

        #region métodos auxiliares
        private async Task<User> ConvertCreateUserDTOToUser(CreateUserDTO createUserDTO)
        {
            User user = new User();
            List<Address> lstAddress = new List<Address>();
            List<UserAddress> lstUserAddress = new List<UserAddress>();
            
            user.Email = createUserDTO.Email;
            user.Name = createUserDTO.Name;
            user.Password = createUserDTO.Password;
            user.Role = createUserDTO.Role;

            if (createUserDTO.Addresses != null)
            {
                foreach (var item in createUserDTO.Addresses)
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
                    lstAddress.Add(addressItem);
                    lstUserAddress.Add(userAddress);
                    user.Addresses.Add(addressItem);
                }
            }
            await SaveAddressList(lstAddress);
            await SaveUserAddressList(lstUserAddress);
            return user;
        }

        private CreateUserDTO ConvertUserToCreateUserDTO(User user)
        {
            CreateUserDTO userDto = new CreateUserDTO();
            userDto.Name = user.Name;
            userDto.Email = user.Email;
            userDto.Password = user.Password;
            userDto.Role = user.Role;
            List<CreateAddressDTO> lstCreateAddressDTO = new List<CreateAddressDTO>();
            if (user.Addresses.Count > 0)
            {
                foreach (var item in user.Addresses)
                {
                    CreateAddressDTO createAddressDTO = new CreateAddressDTO();
                    createAddressDTO.Number = item.Number;
                    createAddressDTO.Street = item.Street;
                    createAddressDTO.Complement = item.Complement;
                    createAddressDTO.Neighborhood = item.Neighborhood;
                    createAddressDTO.City = item.City;
                    createAddressDTO.State = item.State;
                    createAddressDTO.Country = item.Country;
                    createAddressDTO.ZipCode = item.ZipCode;
                    createAddressDTO.District = item.District;
                    lstCreateAddressDTO.Add(createAddressDTO);
                }
            }
            userDto.Addresses = lstCreateAddressDTO;
            return userDto;
        }

        private async Task<bool> SaveAddressList(List<Address> lstAddresses)
        {
            try
            {
                foreach (var item in lstAddresses)
                {
                    await _addressService.AddAddress(item);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> SaveUserAddressList(List<UserAddress> lstUserAddresses)
        {
            try
            {
                foreach (var item in lstUserAddresses)
                {
                    await _userAddressService.AddUserAddress(item);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool IsUserDtoValid(CreateUserDTO userDto)
        {
            if (userDto == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(userDto.Name) ||
                string.IsNullOrEmpty(userDto.Email) ||
                string.IsNullOrEmpty(userDto.Password) ||
                string.IsNullOrEmpty(userDto.Role))
            {
                return false;
            }            
            if (userDto.Addresses == null || !userDto.Addresses.Any())
            {
                return false;
            }                        
            foreach (var address in userDto.Addresses)
            {
                if (string.IsNullOrEmpty(address.Street) ||
                    string.IsNullOrEmpty(address.City) ||
                    string.IsNullOrEmpty(address.ZipCode))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}