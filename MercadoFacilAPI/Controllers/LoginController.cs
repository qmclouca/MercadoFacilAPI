using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MercadoFacilAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;        

        public LoginController(IAuthService authService)
        {
            _authService = authService;            
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
       {
            var token = await _authService.Authenticate(login.Email, login.Password);

            if (string.IsNullOrEmpty(token))
            {
                DateTime dateTime = DateTime.UtcNow;
                Log.Information($"Usuário: {login} tentou acessar com credenciais erradas em {dateTime}.");
                return Unauthorized();
            }
            Log.Information($"Usuário: {login} acessou com sucesso");
            return Ok(new { Token = token });
        }
    }
}
