using Domain.DTOs.Address;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.User
{
    public class CreateUserDTO
    {        
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 12, ErrorMessage = "A senha deve conter no mínimo 12 caracteres e no máximo 40.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$", ErrorMessage = "A senha deve conter pelo menos um número, um caractere especial, uma letra maiúscula e uma minúscula.")]
        public string? Password { get; set; }
        
        public string? Role { get; set; }

        public List<CreateAddressDTO>? Addresses { get; set; }

        public string? ObservedShares { get; set; }
    }
}