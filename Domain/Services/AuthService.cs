using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        //private readonly IConfiguration _configuration;

        public AuthService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetAllQueryAsync(u => u.Email == email);

            // Verifique se o usuário existe e se a senha está correta
            // Supondo que você esteja usando um hash de senha e uma função para verificar isso
            if (user == null || !VerifyPasswordHash(password, user.Password))
            {
                // Usuário não encontrado ou senha incorreta
                return null;
            }

            // Se válido, gere o token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? "User") // Use um valor padrão se o Role for null
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // Implemente sua lógica de verificação de hash de senha aqui
            // Por exemplo, se você estiver usando o BCrypt:
            // return BCrypt.Net.BCrypt.Verify(password, storedHash);
            return true; // Substitua isso pela verificação real do hash da senha
        }
    }
}
