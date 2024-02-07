using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MercadoFacilAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;        

        public LoginController(IAuthService authService)
        {
            _authService = authService;            
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var token = await _authService.Authenticate(login.Email, login.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(new { Token = token });
        }
    }
}
