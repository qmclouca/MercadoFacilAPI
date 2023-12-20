using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MercadoFacilAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(Name = "GetUser")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        //[HttpGet(Name = "GetAllUsers")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var users = await _userService.GetAllUsers();
        //    if (users == null)
        //        return NotFound();
        //    return Ok(users);
        //}

        [HttpPost(Name = "AddUser")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            await _userService.AddUser(user);
            return Ok(user);
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            // await _userService.UpdateUser(user);
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
