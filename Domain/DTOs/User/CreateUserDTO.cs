using Domain.DTOs.Address;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.User
{
    public class CreateUserDTO
    {        
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }      
        public string? Password { get; set; }       
        public string? Role { get; set; }
        public List<CreateAddressDTO>? Addresses { get; set; }
    }
}