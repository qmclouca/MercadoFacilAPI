using Domain.DTOs.Address;

namespace Domain.DTOs.User
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public List<CreateAddressDTO>? Addresses { get; set; }
    }
}